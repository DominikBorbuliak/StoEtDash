﻿const createChart = (type, data, chartContainerId, chartLegendContainerId) => {
	var context = document.getElementById(chartContainerId).getContext('2d');

	_ = new Chart(context, {
		type: type,
		data: data,
		options: {
			plugins: {
				htmlLegend: {
					containerID: chartLegendContainerId
				},
				legend: {
					display: false
				}
			}
		},
		plugins: [htmlLegendPlugin],
	});
};

// Code taken from chart.js documentation: https://www.chartjs.org/docs/latest/samples/legend/html.html
const getOrCreateLegendList = (chart, id) => {
	const legendContainer = document.getElementById(id);
	let listContainer = legendContainer.querySelector('ul');

	if (!listContainer) {
		listContainer = document.createElement('ul');
		listContainer.style.display = 'flex';
		listContainer.style.flexDirection = 'column';
		listContainer.style.margin = 0;
		listContainer.style.padding = 0;

		legendContainer.appendChild(listContainer);
	}

	return listContainer;
};

const htmlLegendPlugin = {
	id: 'htmlLegend',
	afterUpdate(chart, args, options) {
		const ul = getOrCreateLegendList(chart, options.containerID);

		// Remove old legend items
		while (ul.firstChild) {
			ul.firstChild.remove();
		}

		// Reuse the built-in legendItems generator
		const items = chart.options.plugins.legend.labels.generateLabels(chart);

		items.forEach(item => {
			const li = document.createElement('li');
			li.style.alignItems = 'center';
			li.style.cursor = 'pointer';
			li.style.display = 'flex';
			li.style.flexDirection = 'row';
			li.style.marginLeft = '10px';
			li.style.marginRight = 'auto';

			li.onclick = () => {
				const { type } = chart.config;
				if (type === 'pie' || type === 'doughnut') {
					// Pie and doughnut charts only have a single dataset and visibility is per item
					chart.toggleDataVisibility(item.index);
				} else {
					chart.setDatasetVisibility(item.datasetIndex, !chart.isDatasetVisible(item.datasetIndex));
				}
				chart.update();
			};

			// Color box
			const boxSpan = document.createElement('span');
			boxSpan.style.background = item.fillStyle;
			boxSpan.style.borderColor = item.strokeStyle;
			boxSpan.style.borderWidth = item.lineWidth + 'px';
			boxSpan.style.display = 'inline-block';
			boxSpan.style.minHeight = '20px';
			boxSpan.style.marginRight = '10px';
			boxSpan.style.minWidth = '20px';

			// Text
			const textContainer = document.createElement('p');
			textContainer.style.color = item.fontColor;
			textContainer.style.margin = 0;
			textContainer.style.padding = 0;
			textContainer.style.textDecoration = item.hidden ? 'line-through' : '';

			const text = document.createTextNode(item.text);
			textContainer.appendChild(text);

			li.appendChild(boxSpan);
			li.appendChild(textContainer);
			ul.appendChild(li);
		});
	}
};