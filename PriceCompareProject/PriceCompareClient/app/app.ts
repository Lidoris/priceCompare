const app = angular.module("app", ["ui.bootstrap", "ngRoute"])
    .config(($routeProvider: ng.route.IRouteProvider) => {
        $routeProvider.when('/homePage',
            {
                template: '<home-page></home-page>'
            });

        $routeProvider.when('/resultPage',
            {
                template: '<result-page></result-page>'
            });

        $routeProvider.otherwise({ redirectTo: '/homePage' });
    });


