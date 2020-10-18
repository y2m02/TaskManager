function dataBound() {
    this.expandRow(this.tbody.find("tr.k-master-row").first());

    var grid = window.$("#EarningReport").data("kendoGrid");
    var totalPending = 0;
    var total = 0;
    var totalPrince = 0;
    var rows = grid.items();

    window.$(rows).each(function () {
        var row = this;
        var dataItem = grid.dataItem(row);

        total += parseFloat(dataItem.Total);
        totalPending += parseFloat(dataItem.TotalPending);
        totalPrince += parseFloat(dataItem.Price);
    });

    var netEarnings = total > totalPrince
        ? total - totalPrince
        : 0;

    window.$("#lblTotal").html(convertToNumericFormat(total));
    window.$("#lblTotalEarning").html(convertToNumericFormat(netEarnings));
    window.$("#lblTotalPending").html(convertToNumericFormat(totalPending));
}