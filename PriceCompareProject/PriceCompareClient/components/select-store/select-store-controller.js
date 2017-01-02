var SelectStoreController = (function () {
    function SelectStoreController($http, chainsService) {
        this.$http = $http;
        this.chainsService = chainsService;
    }
    SelectStoreController.prototype.addStore = function () {
        this.chainsService.numOfStoresToCompare++;
    };
    SelectStoreController.prototype.updateItems = function () {
        this.chainsService.getItemsByStore(this.chainsService.selectedStores[0]);
    };
    SelectStoreController.$inject = ['$http', 'chainsService'];
    return SelectStoreController;
}());
