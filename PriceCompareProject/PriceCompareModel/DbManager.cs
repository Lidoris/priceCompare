using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PriceCompareModel
{
    public class DbManager
    {
        private PriceCompareContext _context;
        
        public DbManager()
        {
            _context = new PriceCompareContext();
        }
        
        public List<Item> GetItems()
        {
            return _context.Items.Include("Prices").ToList();
        }

        public List<Chain> GetChains()
        {
             //return _context.Chains.ToList();
             return _context.Chains.Include("Stores.Prices").ToList();
        }

        //public List<Price> GetPricesByStore(Store store)
        //{
        //    return _context.Prices.Where(p => p.StoreID == store.StoreID).ToList();
        //}

        //public List<Price> GetPrices()
        //{
        //    return _context.Prices.Include("Store").ToList();
            
        //}

        //public List<Store> GetStores()
        //{
        //    return _context.Stores.Include("Prices").ToList();
        //}

        public List<Item> GetItemsByStore(Store store)
        {
           var items = _context.Prices.Where(p => p.StoreID == store.StoreID).Select(p => p.Item).Include("Prices");
           var items2 = items.ToList();
           return items2;
        }

        public void DecompressAllFiles()
        {
            DirectoryInfo allPrices = new DirectoryInfo(@"C:\Users\CodeValue\Lidor Ismach-moshe\prices\bin\chains");
            foreach (var subDirectories in allPrices.GetDirectories())
            {
                foreach (var file in subDirectories.GetFiles())
                {
                    if ((file.Name.StartsWith("PriceFull") || file.Name.StartsWith("PricesFull")) && (file.Extension == ".gz" || file.Extension == ".zip") && !subDirectories.GetFiles(file.Name.Remove(file.Name.Length - file.Extension.Length) + ".xml").Any())
                    {
                        if (file.Extension == ".gz")
                        {
                            DecompressGZ(file);
                        }
                        else if (file.Extension == ".zip")
                        {
                            DecompressZip(file);
                        }
                    }
                }
            }
        }

        public void DecompressZip(FileInfo fileToDecompress)
        {
            string zipPath = fileToDecompress.FullName;
            string extractPath = fileToDecompress.Name.Remove(fileToDecompress.Name.Length - fileToDecompress.Extension.Length);

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        entry.ExtractToFile(Path.Combine(extractPath, entry.FullName));
                    }
                }
            }
        }

        public void DecompressGZ(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);
                newFileName = newFileName + ".xml";
                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
        }

        public void PopulateDB()
        {
            Chain curChain;
            List<Store> listOfStores = new List<Store>();
            List<Item> listOfItems = new List<Item>();
            List<Price> listOfPrices = new List<Price>();
            DirectoryInfo allPrices = new DirectoryInfo(@"C:\Users\CodeValue\Lidor Ismach-moshe\prices\bin\chains");

            foreach (var subDirectory in allPrices.GetDirectories())
            {
                DecompressAllFiles();

                string storeFilePath = subDirectory.GetFiles().Where(t => t.Name.StartsWith("Stores")).Single().FullName;
                XDocument storesDoc = XDocument.Load(storeFilePath);
                curChain = ChainToDB(storesDoc);
                Chain existingChain = _context.Chains.FirstOrDefault(c => c.ChainID == curChain.ChainID);

                if (existingChain == null)
                {
                    _context.Chains.Add(curChain);
                    _context.SaveChanges();
                }

                listOfStores = StoresToDB(storesDoc, curChain);
                listOfStores.ForEach(s =>
                {
                    _context.Stores.Add(s);
                    _context.SaveChanges();
                });

                FileInfo[] priceFullXmlFiles = subDirectory.GetFiles("PriceFull*.xml");
                foreach (var file in priceFullXmlFiles)
                {
                    listOfItems = ItemsToDB(file);
                    foreach (Item Item in listOfItems)
                    {
                        var existingItem = _context.Items.FirstOrDefault(i => i.ItemID == Item.ItemID);
                        if (existingItem == null)
                        {
                            _context.Items.Add(Item);
                            _context.SaveChanges();
                        }
                    }
                }

                foreach (var file in priceFullXmlFiles)
                {
                    listOfPrices = PricesToDB(file);
                    foreach (Price Price in listOfPrices)
                    {
                        var existingPrice = _context.Prices.FirstOrDefault(p => p.ItemID == Price.ItemID && p.StoreID == Price.StoreID);
                        if (existingPrice == null)
                        {
                            _context.Prices.Add(Price);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        private Chain ChainToDB(XDocument storesDoc)
        {
            XElement root = storesDoc.Root;
            Chain chain = new Chain();
            long chainId;
            long.TryParse(root.Element("ChainId").Value, out chainId);

            chain.ChainID = chainId;
            chain.ChainName = root.Element("ChainName").Value;

            return chain;
        }

        private List<Store> StoresToDB(XDocument storesDoc, Chain chain)
        {
            List<Store> listOfStores = new List<Store>();
            XElement storesElm = storesDoc.Root.Element("SubChains").Element("SubChain").Element("Stores");

            foreach (var store in storesElm.Elements("Store"))
            {
                Store newStore = new Store();
                long storeCode;
                long.TryParse(store.Element("StoreId").Value, out storeCode);
                newStore.StoreCode = storeCode;
                newStore.ChainID = chain.ChainID;
                newStore.StoreName = store.Element("StoreName").Value;
                newStore.Address = store.Element("Address").Value;
                newStore.City = store.Element("City").Value;
                var existingStore = _context.Stores.FirstOrDefault(s => s.StoreID == newStore.StoreID && s.ChainID == newStore.ChainID);
                if (existingStore == null)
                {
                    listOfStores.Add(newStore);
                }
                else
                {
                    existingStore.ChainID = chain.ChainID;
                    existingStore.StoreName = store.Element("StoreName").Value;
                    existingStore.Address = store.Element("Address").Value;
                    existingStore.City = store.Element("City").Value;
                }
            }

            return listOfStores;
        }

        private List<Item> ItemsToDB(FileInfo xmlFile)
        {
            XDocument doc = XDocument.Load(xmlFile.FullName);
            List<Item> listOfItems = new List<Item>();

            foreach (XElement itemElement in doc.Root.Element("Items").Elements("Item"))
            {
                Item item = new Item();
                long itemId;
                long.TryParse(itemElement.Element("ItemCode").Value, out itemId);
                item.ItemID = itemId;
                item.ItemName = itemElement.Element("ItemName").Value;
                item.ManufacturerName = itemElement.Element("ManufacturerName").Value;
                item.Quantity = itemElement.Element("Quantity").Value;
                item.Description = itemElement.Element("ManufacturerItemDescription").Value;
                item.UnitQty = itemElement.Element("UnitQty").Value;
                var existingItem = _context.Items.FirstOrDefault(i => i.ItemID == item.ItemID);
                if (existingItem == null)
                {
                    listOfItems.Add(item);
                }
                else
                {
                    existingItem.ItemName = itemElement.Element("ItemName").Value;
                    existingItem.ManufacturerName = itemElement.Element("ManufacturerName").Value;
                    existingItem.Quantity = itemElement.Element("Quantity").Value;
                    existingItem.Description = itemElement.Element("ManufacturerItemDescription").Value;
                }
            }

            return listOfItems;
        }

        private List<Price> PricesToDB(FileInfo xmlFile)
        {
            XDocument doc = XDocument.Load(xmlFile.FullName);
            List<Price> listOfPrices = new List<Price>();
            long chainId;
            long itemId;
            long storeCode;

            foreach (XElement ItemElement in doc.Root.Element("Items").Elements("Item"))
            {
                long.TryParse(ItemElement.Element("ItemCode").Value, out itemId);
                long.TryParse(doc.Root.Element("ChainId").Value, out chainId);
                var existingItem = _context.Items.FirstOrDefault(i => i.ItemID == itemId);
                if (existingItem != null)
                {
                    Price price = new Price();
                    price.ItemID = existingItem.ItemID;
                    long.TryParse(doc.Root.Element("StoreId").Value, out storeCode);
                    var existingStore = _context.Stores.FirstOrDefault(s => s.StoreCode == storeCode && s.ChainID == chainId);
                    price.StoreID = existingStore.StoreID;
                    float itemPrice;
                    float.TryParse(ItemElement.Element("ItemPrice").Value, out itemPrice);
                    price.ItemPrice = itemPrice;
                    var existingPrice = _context.Prices.FirstOrDefault(p => p.ItemID == price.ItemID && p.StoreID == price.StoreID);
                    if (existingPrice == null)
                    {
                        listOfPrices.Add(price);
                    }
                }
            }

            return listOfPrices;
        }

    }
}
