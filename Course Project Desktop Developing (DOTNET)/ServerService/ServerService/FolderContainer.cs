using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServerService
{
    [DataContract]
    public class FolderContainer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public DateTime CreationTime { get; set; }
        [DataMember]
        List<FileContainer> Files { get; set; }

        public FolderContainer() { }
    }
}
