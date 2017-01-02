app.component("item",
    {
        templateUrl: "components/item/item-template.html",
        controller: ItemController,
        bindings: {
             item: "="

        }
    });