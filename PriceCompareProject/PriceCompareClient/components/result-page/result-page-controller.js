var ResultPageController = (function () {
    function ResultPageController($http, chainsService) {
        this.$http = $http;
        this.chainsService = chainsService;
        this.chainsService.bestResultPrice = this.chainsService.results[0].totalCostForStore;
        this.chainsService.bestResultIndex = 0;
    }
    ResultPageController.$inject = ['$http', 'chainsService'];
    return ResultPageController;
}());
