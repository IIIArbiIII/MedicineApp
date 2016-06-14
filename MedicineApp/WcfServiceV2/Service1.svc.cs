using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceV2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public byte[] RetrieveFile(string path1)
        {
            try
            {
            string path = @"C:\Users\Arbi\Desktop\SQLITEV5.sqlite";

            if (WebOperationContext.Current == null) throw new Exception("WebOperationContext not set");

            // As the current service is being used by a windows client, there is no browser interactivity.  
            // In case you are using the code Web, please use the appropriate content type.  
            var fileName = Path.GetFileName(path);
           // WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
           // WebOperationContext.Current.OutgoingResponse.Headers.Add("content-disposition", "inline; filename=" + fileName);
            var ss = File.ReadAllBytes(path);
            return ss;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public void UploadFile(Stream stream)
        {
            //TODO: SPREMENI PATH
            string path = @"C:\Users\Arbi\Desktop\SQLITEV5.sqlite";
 
            CreateDirectoryIfNotExists(path);
            using (var file = File.Create(path))
            {
                stream.CopyTo(file);
            }
        }

        private void CreateDirectoryIfNotExists(string filePath)
        {
            var directory = new FileInfo(filePath).Directory;
            if (directory == null) throw new Exception("Directory could not be determined for the filePath");

            Directory.CreateDirectory(directory.FullName);
        }
    }
}
