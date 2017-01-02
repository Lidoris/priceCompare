var ShoppingCartController = (function () {
    function ShoppingCartController($http, chainsService, $scope) {
        this.$http = $http;
        this.chainsService = chainsService;
    }
    ShoppingCartController.prototype.comparePrices = function () {
        var _this = this;
        var i = 0;
        this.chainsService.results = [];
        this.chainsService.selectedStores.forEach(function (store) {
            _this.oneResult = { chain: null, store: null, prices: [], totalCostForStore: 0 };
            _this.oneResult.store = store;
            _this.oneResult.chain = _this.chainsService.selectedChains[i++];
            _this.chainsService.shoppingCart.forEach(function (item) {
                var price = store.prices.filter(function (price) { return item.item.itemID === price.itemID; });
                if (price.length !== 0) {
                    price[0].item = item.item;
                    _this.oneResult.prices.push(price[0]);
                    _this.oneResult.totalCostForStore += (price[0].itemPrice * item.amount);
                }
                else {
                    _this.oneResult.prices.push({ priceID: 0, itemID: item.item.itemID, storeID: store.storeID, itemPrice: undefined, store: store, item: item.item });
                }
            });
            _this.chainsService.results.push(_this.oneResult);
        });
    };
    ShoppingCartController.$inject = ['$http', 'chainsService'];
    return ShoppingCartController;
}());
