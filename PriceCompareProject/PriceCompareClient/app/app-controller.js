var AppController = (function () {
    function AppController($http) {
        this.$http = $http;
    }
    AppController.$inject = ['$http'];
    return AppController;
}());
app.controller("AppController", AppController);
