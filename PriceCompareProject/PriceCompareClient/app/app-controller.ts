class AppController {
    public static $inject: string[] = ['$http'];
  
    constructor(private $http: ng.IHttpService) {

    }


}

app.controller("AppController", AppController); 