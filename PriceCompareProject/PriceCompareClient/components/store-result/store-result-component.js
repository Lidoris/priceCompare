app.component("storeResult", {
    templateUrl: "components/store-result/store-result-template.html",
    controller: StoreResultController,
    bindings: {
        resultStorePrices: "=item",
        index: "<"
    }
});
