class StoreResultController {
    public static $inject: string[] = ['$http', 'chainsService'];
    public resultStorePrices: ResultStorePrices;
    public index: number;

    constructor(private $http: ng.IHttpService, private chainsService: ChainsService) {
        if (this.resultStorePrices.totalCostForStore < this.chainsService.bestResultPrice) {
            this.chainsService.bestResultPrice = this.resultStorePrices.totalCostForStore;
            this.chainsService.bestResultIndex = this.index;
        }

    }

}