class HomePageController {
  
    public static $inject: string[] = ['$http', 'chainsService'];
    constructor(private $http: ng.IHttpService, private chainsService: ChainsService) {
      
    }

   
}
