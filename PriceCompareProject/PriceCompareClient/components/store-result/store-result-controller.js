var StoreResultController = (function () {
    function StoreResultController($http, chainsService) {
        this.$http = $http;
        this.chainsService = chainsService;
        if (this.resultStorePrices.totalCostForStore < this.chainsService.bestResultPrice) {
            this.chainsService.bestResultPrice = this.resultStorePrices.totalCostForStore;
            this.chainsService.bestResultIndex = this.index;
        }
    }
    StoreResultController.$inject = ['$http', 'chainsService'];
    return StoreResultController;
}());
