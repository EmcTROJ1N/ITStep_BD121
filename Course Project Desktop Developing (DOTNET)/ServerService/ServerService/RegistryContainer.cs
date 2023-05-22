using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ServerService
{
    [DataContract(IsReference = true)]
    public class RegistryKeyContainer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string[] SubKeys { get; set; }
        [DataMember]
        List<ValueContainer> Values { get; set; }
        public RegistryKeyContainer() { }
    }

    [DataContract(IsReference = true)]
    public class ValueContainer
    {
        [DataMember]
        public RegistryKeyContainer Parent { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public RegistryValueKind Type { get; set; }
        [DataMember]
        public object Value { get; set; }
        public ValueContainer() { }
    }
}
