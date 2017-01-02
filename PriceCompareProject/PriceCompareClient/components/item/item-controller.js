var ItemController = (function () {
    function ItemController($http, chainsService) {
        this.$http = $http;
        this.chainsService = chainsService;
        this.amount = 1;
        this.itemWithAmount = { item: null, amount: 0 };
    }
    ItemController.prototype.addItemToShoppingCart = function () {
        var _this = this;
        if (this.chainsService.shoppingCart.some(function (x) { return x.item.itemID === _this.item.itemID; })) {
            this.chainsService.shoppingCart.filter(function (x) { return x.item.itemID === _this.item.itemID; })[0].amount += this.amount;
        }
        else {
            this.itemWithAmount.item = this.item;
            this.itemWithAmount.amount = this.amount;
            this.chainsService.shoppingCart.push(this.itemWithAmount);
        }
        this.chainsService.numOfAllItems += this.amount;
    };
    ItemController.$inject = ['$http', 'chainsService'];
    return ItemController;
}());
