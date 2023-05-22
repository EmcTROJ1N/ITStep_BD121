using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServerService
{
    [DataContract]
    public class ProcessContainer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int BasePriority { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string WindowTitle { get; set; }
        [DataMember]
        public long _PagedMemory { get; set; }
        [DataMember]
        public bool Responding { get; set; }

        public ProcessContainer() { }
    }
}
