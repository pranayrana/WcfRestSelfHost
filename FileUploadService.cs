using AntsCode.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace WcfHostService
{
    public class FileUploadService : IFileUploadService
    {
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "TestData")]
        public string TestData()
        {
            return "Test";
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UploadFile")]
        public UploadedFile UploadFile(Stream stream)
        {
            //http://multipartparser.codeplex.com/
            MultipartParser parser = new MultipartParser(stream);

            if (parser.Success)
            {
                UploadedFile upload = new UploadedFile
                {
                    FilePath = Path.Combine(Path.GetTempPath(), parser.Filename)
                };


                int length = 0;
                using (FileStream writer = new FileStream(upload.FilePath, FileMode.Create))
                {
                    var buffer = parser.FileContents;// new byte[8192];
                    writer.Write(parser.FileContents, 0, parser.FileContents.Length);
                }

                upload.FileLength = length;
                upload.FileName = parser.Filename;
                return upload;
            }

            else

            {
                throw (new Exception("Error in uploading file"));
            }
        }
    }
}
