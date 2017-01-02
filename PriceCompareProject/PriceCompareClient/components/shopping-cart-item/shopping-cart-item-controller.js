var ShoppingCartItemController = (function () {
    function ShoppingCartItemController($http, chainsService) {
        this.$http = $http;
        this.chainsService = chainsService;
    }
    ShoppingCartItemController.prototype.removeItemFromShoppingCart = function () {
        this.chainsService.numOfAllItems -= this.itemWithAmount.amount;
        this.chainsService.shoppingCart.splice(this.chainsService.shoppingCart.indexOf(this.itemWithAmount), 1);
    };
    ShoppingCartItemController.prototype.updateAmount = function () {
        var _this = this;
        this.chainsService.numOfAllItems = 0;
        this.chainsService.shoppingCart.forEach(function (i) { return _this.chainsService.numOfAllItems += i.amount; });
    };
    ShoppingCartItemController.$inject = ['$http', 'chainsService'];
    return ShoppingCartItemController;
}());
