using System;
using System.Collections.Generic;

namespace Swk5.GeoCaching.DomainModel {
    public class StatisticDataset {
        public List<StatisticData> Data { get; set; }
        public String Caption { get; set; }       
        public String Column2Caption { get; set; }
        public String Column3Caption { get; set; }        
    }
}