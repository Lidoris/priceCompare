var ChainsService = (function () {
    function ChainsService($http) {
        var _this = this;
        this.$http = $http;
        this.chains = [];
        this.items = [];
        this.prices = [];
        this.shoppingCart = [];
        this.numOfAllItems = 0;
        this.selectedChains = [];
        this.selectedStores = [];
        this.numOfStoresToCompare = 2;
        this.results = [];
        this.getChains().then(function (data) { return _this.chains = data; });
    }
    ChainsService.prototype.getChains = function () {
        return this.$http.get("api/chains")
            .then(function (data) {
            return data.data;
        });
    };
    ChainsService.prototype.getItemsByStore = function (store) {
        var _this = this;
        return this.$http.post("api/items", store)
            .then(function (data) {
            _this.items = data.data;
        });
    };
    ChainsService.prototype.getItems = function () {
        return this.$http.get("api/items")
            .then(function (data) {
            return data.data;
        });
    };
    ChainsService.prototype.getPricesByStore = function (store) {
        var _this = this;
        return this.$http.post("api/prices", store)
            .then(function (data) {
            _this.prices = data.data;
        });
    };
    ChainsService.prototype.getPrices = function () {
        return this.$http.get("api/prices")
            .then(function (data) {
            return data.data;
        });
    };
    ChainsService.$inject = ['$http'];
    return ChainsService;
}());
app.service('chainsService', ChainsService);
