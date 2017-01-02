using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace PriceCompareModel
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class Store
    {
        [DataMember]
        public long StoreID { get; set; }
        [DataMember]
        public long StoreCode { get; set; }
        [DataMember]
        public string StoreName { get; set; }
        [DataMember]
        public long ChainID { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public virtual Chain Chain { get; set; }
        [DataMember]
        public virtual ICollection<Price> Prices { get; set; }
      
    }
}
