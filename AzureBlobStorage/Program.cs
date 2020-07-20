using System;
using System.IO;
using System.Text;

namespace AzureBlobStorage
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string filepath = @"C:\Users\Administrator\Desktop\go.txt";
                string fileName = string.Empty;

                UploadByteArray(filepath);
                
                UploadFileStream(filepath);
                
            }
            catch (Exception ex)
            {
                Console.Write("ERROR: " +ex.Message);                
            }

            void UploadByteArray(string filepath)
            {
                string fileName;
                //Get byte array
                byte[] bytes = File.ReadAllBytes(filepath);

     
                //Upload byte array
                fileName = "blobname" + Guid.NewGuid();
                AzureBlob.Upload(bytes, fileName);
                Console.WriteLine("Upload byte array successful");
            }

            void UploadFileStream(string filepath)
            {
                string fileName;
                //Get file stream
                using FileStream uploadFileStream = File.OpenRead(filepath);
                //Upload file stream
                fileName = "blobname" + Guid.NewGuid();
                AzureBlob.Upload(uploadFileStream, fileName);
                Console.WriteLine("Upload file stream successful");
            }
        }

    }
}