app.component("shoppingCartItem",
    {
        templateUrl: "components/shopping-cart-item/shopping-cart-item-template.html",
        controller: ShoppingCartItemController,
         bindings: {
             itemWithAmount: "="
        }

    });