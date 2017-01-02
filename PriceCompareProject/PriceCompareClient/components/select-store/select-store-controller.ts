class SelectStoreController {
  
    public static $inject: string[] = ['$http', 'chainsService'];
    constructor(private $http: ng.IHttpService, private chainsService: ChainsService) {
    }

    public addStore() {
        this.chainsService.numOfStoresToCompare++;
    }



    public updateItems() {
        this.chainsService.getItemsByStore(this.chainsService.selectedStores[0]);
    }


}
