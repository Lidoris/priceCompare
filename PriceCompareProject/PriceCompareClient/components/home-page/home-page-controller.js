var HomePageController = (function () {
    function HomePageController($http, chainsService) {
        this.$http = $http;
        this.chainsService = chainsService;
    }
    HomePageController.$inject = ['$http', 'chainsService'];
    return HomePageController;
}());
