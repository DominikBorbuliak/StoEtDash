// Global
const clearErrorMessages = () => {
	document.querySelectorAll(".text-danger").forEach(element => element.textContent = '');
}

// _Asset.cshtml
const setDataInSellTransactionModal = (name, ticker) => {
	$("#sell-transaction-modal-name").val(name);
	$("#sell-transaction-modal-ticker").val(ticker);
	setMaximumSellableSharesForSellModal();
	clearErrorMessages();
}

const onAssetClick = (e, ticker) => {
	// Do not count as click on asset if button with sell was clicked
	if (e.target.tagName == 'BUTTON') {
		return;
	}

	$.ajax({
		type: "POST",
		url: "/Dashboard/ShowAssetTransactions?ticker=" + ticker,
		contentType: "application/json; charset=utf-8",
		dataType: "html",
		success: function (response) {
			$("#transaction-list-modal").find(".modal-body").html(response);
			$("#transaction-list-modal").modal('show');
		}
	});
}

// _Transaction.cshtml
const onEditTransactionClick = (transactionId) => {
	$.ajax({
		type: "POST",
		url: "/Dashboard/ShowEditTransactionModal?transactionId=" + transactionId,
		contentType: "application/json; charset=utf-8",
		dataType: "html",
		success: function (response) {
			$("#edit-transaction-modal").find(".modal-body").html(response);
			$("#edit-transaction-modal").modal('show');
		}
	});
}

// _SellTransactionModal.cshtml
const setMaximumSellableSharesForSellModal = () => {
	const ticker = document.getElementById("sell-transaction-modal-ticker").value;
	const time = document.getElementById("sell-transaction-modal-time").value;

	$.ajax({
		type: "GET",
		url: "/Dashboard/GetNumberOfSellableShare?ticker=" + ticker + "&dateTime=" + time + "&transactionId=",
		contentType: "application/json; charset=utf-8",
		dataType: "html",
		success: function (response) {
			document.getElementById("sell-transaction-modal-number-of-shares").max = response;
		}
	});
}

// _EditTransactionModalBody.cshtml
const setMaximumSellableSharesForEditModal = () => {
	const actionType = document.getElementById("edit-transaction-modal-action-type").value;

	if (actionType == 'Buy') {
		return;
	}

	const ticker = document.getElementById("edit-transaction-modal-ticker").value;
	const time = document.getElementById("edit-transaction-modal-time").value;
	const transactionId = document.getElementById("edit-transaction-modal-id").value;

	$.ajax({
		type: "GET",
		url: "/Dashboard/GetNumberOfSellableShare?ticker=" + ticker + "&dateTime=" + time + "&transactionId=" + transactionId,
		contentType: "application/json; charset=utf-8",
		dataType: "html",
		success: function (response) {
			document.getElementById("edit-transaction-modal-number-of-shares").max = response;
		}
	});
}