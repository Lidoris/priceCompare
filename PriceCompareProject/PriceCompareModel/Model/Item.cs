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
    public class Item
    {
        [DataMember]
        public long ItemID { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string ManufacturerName { get; set; }
        [DataMember]
        public string Quantity { get; set; }
        [DataMember]
        public string UnitQty { get; set; }
        [DataMember]
        public virtual ICollection<Price> Prices { get; set; }

    }
}
