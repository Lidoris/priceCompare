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
    public class Price
    {
        [DataMember]
        public long PriceID { get; set; }
        [DataMember]
        public long ItemID { get; set; }
        [DataMember]
        public long StoreID { get; set; }
        [DataMember]
        public double ItemPrice { get; set; }
        [DataMember]
        public virtual Store Store { get; set; }
        [DataMember]
        public virtual Item Item { get; set; }
    }


}
