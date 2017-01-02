class ShoppingCartItemController {
    public static $inject: string[] = ['$http', 'chainsService'];
    public itemWithAmount: ItemWithAmount;

    constructor(private $http: ng.IHttpService, private chainsService: ChainsService) {
        }
       
    public removeItemFromShoppingCart() {
        this.chainsService.numOfAllItems -= this.itemWithAmount.amount
        this.chainsService.shoppingCart.splice(this.chainsService.shoppingCart.indexOf(this.itemWithAmount),1);
    }

    public updateAmount() {
      this.chainsService.numOfAllItems = 0;
      this.chainsService.shoppingCart.forEach(i=> this.chainsService.numOfAllItems += i.amount);
    }

    //public checkIfQuantityExists() {
    //    return !(this.itemWithAmount.item.unitQty=== "Unknown");
    //}
}
