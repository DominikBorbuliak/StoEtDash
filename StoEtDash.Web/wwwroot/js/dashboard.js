// _Asset.cshtml
const setDataInSellTransactionModal = (name, ticker) => {
	$("#sell-transaction-modal-name").val(name);
	$("#sell-transaction-modal-ticker").val(ticker);
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