class ItemController {
    public item: Item ;
    public amount: number = 1;
    public itemWithAmount:ItemWithAmount = {item:null,  amount : 0} ;

    public static $inject: string[] = ['$http', 'chainsService'];

    constructor(private $http: ng.IHttpService, private chainsService: ChainsService) {
    }

    public addItemToShoppingCart() {
        if (this.chainsService.shoppingCart.some(x => x.item.itemID === this.item.itemID)) {
            this.chainsService.shoppingCart.filter(x => x.item.itemID === this.item.itemID)[0].amount += this.amount;
        } else {
             this.itemWithAmount.item = this.item;
             this.itemWithAmount.amount = this.amount;
             this.chainsService.shoppingCart.push(this.itemWithAmount);
        }
       
        this.chainsService.numOfAllItems += this.amount;
    }
    
}
