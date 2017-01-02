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
    public class Chain
    {
        [DataMember]
        public long ChainID { get; set; }
        [DataMember]
        public string ChainName { get; set; }
        [DataMember]
        public virtual ICollection<Store> Stores { get; set; }
        
    }
}
