class Store {
    public storeID: number;
    public storeCode: number;
    public storeName: string;
    public chainID: number;
    public address: string;
    public city: string;

    public chain: Chain = undefined;
    public prices: Price[];
}
