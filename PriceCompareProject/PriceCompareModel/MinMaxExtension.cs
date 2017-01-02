using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceCompareModel
{
    static class MinMaxExtension
    {
        static public Price Minimum(this List<Price> listOfPrices)
        {
            Price minPrice = listOfPrices[0];
            foreach (var price in listOfPrices)
            {
                if (price != null)
                {
                    if (price.ItemPrice < minPrice.ItemPrice)
                    {
                        minPrice = price;
                    }
                }
            }

            return minPrice;
        }

        static public Price Maximum(this List<Price> listOfPrices)
        {
            double max = 0;
            Price maxPrice = null;
            foreach (var price in listOfPrices)
            {
                if (price != null)
                {
                    if (price.ItemPrice > max)
                    {
                        max = price.ItemPrice;
                        maxPrice = price;
                    }
                }
            }

            return maxPrice;
        }
    }
}
