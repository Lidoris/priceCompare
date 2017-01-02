class ShoppingCartController {
    public oneResult: ResultStorePrices ;

    public static $inject: string[] = ['$http', 'chainsService'];
    constructor(private $http: ng.IHttpService, private chainsService: ChainsService, $scope) {
      
    }

    public comparePrices() {
        var i = 0;
        this.chainsService.results = [];
        this.chainsService.selectedStores.forEach(store => {
            this.oneResult = { chain: null, store: null, prices: [], totalCostForStore: 0 };
            this.oneResult.store = store;
            this.oneResult.chain = this.chainsService.selectedChains[i++];
            this.chainsService.shoppingCart.forEach(item => {
                var price = store.prices.filter(price => item.item.itemID === price.itemID );
                if (price.length !== 0) {
                    price[0].item = item.item;
                    this.oneResult.prices.push(price[0]);
                    this.oneResult.totalCostForStore += (price[0].itemPrice * item.amount);
                } else {
                    this.oneResult.prices.push({ priceID: 0, itemID: item.item.itemID, storeID: store.storeID, itemPrice: undefined, store: store, item: item.item});
                }
            });
            this.chainsService.results.push(this.oneResult);

        });
    }

}