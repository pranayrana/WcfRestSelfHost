using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfHostService
{
    [ServiceContract]
    interface IFileUploadService
    {
        [OperationContract]
        UploadedFile UploadFile(Stream stream);

        [OperationContract]
        string TestData();
    }
}
