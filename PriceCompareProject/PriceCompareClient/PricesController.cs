using PriceCompareModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PriceCompareClient
{
    [RoutePrefix("api")]
    public class PricesController : ApiController
    {
        private readonly DbManager _dbManager = new DbManager();

        [Route("chains")]
        [HttpGet]
        public Chain[] GetChains()
        {
            return _dbManager.GetChains().ToArray();
        }

        [Route("items")]
        [HttpGet]
        public Item[] GetItems()
        {

            return _dbManager.GetItems().ToArray();
        }

        [Route("items")]
        [HttpPost]
        public Item[] GetItemsByStore(Store store)
        {
            return _dbManager.GetItemsByStore(store).ToArray();
        }

        //[Route("prices")]
        //[HttpPost]
        //public Price[] GetPricesByStore(Store store)
        //{
        //    return _dbManager.GetPricesByStore(store).ToArray();
        //}

        //[Route("prices")]
        //[HttpGet]
        //public Price[] GetPrices()
        //{
        //    return _dbManager.GetPrices().ToArray();
        //}
    }
}