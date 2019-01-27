using System.Runtime.Serialization;

namespace WcfHostService
{
    [DataContract]
    public class UploadedFile
    {

        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public int FileLength
        {
            get; set;
        }
        [DataMember]
        public string FileName { get; set; }

    }
}
