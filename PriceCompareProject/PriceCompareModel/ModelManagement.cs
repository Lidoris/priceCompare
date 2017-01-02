using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompareModel
{
    public class ModelManagement
    {
        public ShoppingCart _shoppingCart { get; private set; } = new ShoppingCart();
        public Dictionary<long, List<Price>> _minPricesForAllChains { get; private set; } = new Dictionary<long, List<Price>>();
        public Dictionary<long, double> _chainRank { get; private set; } = new Dictionary<long, double>();
        public DbManager _DbManager = new DbManager();

        public Price FindMinPriceForItemAndChain(Item item, Chain chain)
        {
        Price minPrice;

        if (chain != null)
        {
            minPrice = _minPricesForAllChains[chain.ChainID].Find(x => x.ItemID == item.ItemID);
        }
        else
        {
            minPrice = null;
        }

        return minPrice;
    }

    public List<Price> GetMinimumPricesForChian(Chain chain)
    {
        return _minPricesForAllChains[chain.ChainID];
    }

    public void FindTheMinPricesForAllChains()
    {
        List<Price> allPricesForCurChain;

        _minPricesForAllChains.Clear();
        _chainRank.Clear();

        foreach (var chain in _DbManager.GetChains())
        {
            _minPricesForAllChains.Add(chain.ChainID, new List<Price>());
            foreach (var item in _shoppingCart.selectedItems)
            {
                allPricesForCurChain = new List<Price>();
                foreach (var price in item.Prices)// adding all prices for the current chain in the list
                {
                    if (price.Store.ChainID == chain.ChainID)
                    {
                        allPricesForCurChain.Add(price);
                    }
                }

                if (allPricesForCurChain.Any()) //if there is at least one item
                {
                    _minPricesForAllChains[chain.ChainID].Add(allPricesForCurChain.Minimum()); // adding the minimun price to the final list
                }
            }
        }

        InitChainRank();
        foreach (var item in _shoppingCart.selectedItems)
        {
            UpdateChainRank(item);
        }

    }

    public void InitChainRank()
    {
        foreach (var chain in _DbManager.GetChains())
        {
            _chainRank.Add(chain.ChainID, 0);
        }
    }

    public void UpdateChainRank(Item item) // Chain Rank helps us decide which chain is the best
    {
        List<Price> pricesForItem = new List<Price>();

        foreach (var pair in _minPricesForAllChains)
        {
            Price price = pair.Value.Find(x => x.ItemID == item.ItemID);
            if (price != null)
            {
                pricesForItem.Add(price);
            }
        }

        Price maxPrice = pricesForItem.Maximum();

        foreach (var chain in _DbManager.GetChains())
        {
            Price curPrice = pricesForItem.Find(x => x.Store.ChainID == chain.ChainID);

            if (curPrice != null)
            {
                _chainRank[chain.ChainID] += (maxPrice.ItemPrice - pricesForItem.Find(x => x.Store.ChainID == chain.ChainID).ItemPrice);
            }
        }
    }

    public Chain FindBestRank()
    {
        long bestRankChain = 0;
        double bestRank = 0;
        foreach (var pair in _chainRank)
        {
            if (pair.Value > bestRank)
            {
                bestRank = pair.Value;
                bestRankChain = pair.Key;
            }
        }

        return _DbManager.GetChains().Find(c => c.ChainID == bestRankChain);
    }

    public List<string> FindMissingItemsInCart(Chain chain)
    {
        List<string> listOfMissingItems = new List<string>();
        foreach (var item in _shoppingCart.selectedItems)
        {
            if (_minPricesForAllChains[chain.ChainID].Find(x => x.ItemID == item.ItemID) == null)
            {
                if (!listOfMissingItems.Contains(item.ItemName))
                {
                    listOfMissingItems.Add(item.ItemName);
                }
            }
        }

        return listOfMissingItems;
    }

    public double TotalCartPrice(Chain chain)
    {
        double sum = 0;
        foreach (var price in _minPricesForAllChains[chain.ChainID])
        {
            sum += price.ItemPrice;
        }

        return sum;
    }
}
}

