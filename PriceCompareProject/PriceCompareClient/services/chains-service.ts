class ChainsService {
    public static $inject: string[] = ['$http'];
    public chains: Chain[] = [];
    public items: Item[] = [];
    public prices: Price[] = [];
    public shoppingCart: ItemWithAmount[] = [];
    public numOfAllItems: number = 0;
    public selectedChains: Chain[] = [];
    public selectedStores: Store[] = [];
    public numOfStoresToCompare: number = 2;
    public results: ResultStorePrices[] = [];
    public bestResultIndex: number;
    public bestResultPrice: number ;



    public constructor(private $http: ng.IHttpService) {
        this.getChains().then(data => this.chains = data );
    }

    public getChains(): ng.IPromise<Chain[]> {
        return this.$http.get("api/chains")
            .then(data => {
                return data.data;
            });
    }

    public getItemsByStore(store: Store): ng.IPromise<void> {
        return this.$http.post<Item[]>("api/items" , store)
            .then(data => {
                this.items = data.data;
            });
    }

    public getItems(): ng.IPromise<Item[]> {
        return this.$http.get("api/items")
            .then(data => {
                return data.data;
            });
    }

    public getPricesByStore(store: Store): ng.IPromise<void> {
        return this.$http.post<Price[]>("api/prices", store)
            .then(data => {
                this.prices = data.data;
            });
    }
    public getPrices(): ng.IPromise<Price[]> {
        return this.$http.get("api/prices")
            .then(data => {
                return data.data;
            });
    }
}
app.service('chainsService', ChainsService);