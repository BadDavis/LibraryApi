using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public class PropertyMappingValue
    {
        public IEnumerable<string> DestinationProperties { get; set; }
        public bool Revert { get; set; }
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool rever = false)
        {
            DestinationProperties = destinationProperties;
            Revert = rever;
        }
    }
}
