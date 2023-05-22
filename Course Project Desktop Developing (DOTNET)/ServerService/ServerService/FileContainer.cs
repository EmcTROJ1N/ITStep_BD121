using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServerService
{
    [DataContract]
    public class FileContainer
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsReadOnly { get; set; }
        [DataMember]
        public DateTime LastAccessTime { get; set; }
        [DataMember]
        public DateTime LastWriteTime { get; set; }
        [DataMember]
        public long Length { get; set; }
        [DataMember]
        public string FullName { get; set; }
        public FileContainer() { }
    }
}
