class ResultPageController {
    public static $inject: string[] = ['$http', 'chainsService'];
    constructor(private $http: ng.IHttpService, private chainsService: ChainsService) {
        this.chainsService.bestResultPrice = this.chainsService.results[0].totalCostForStore;
        this.chainsService.bestResultIndex = 0;
    }

    
}