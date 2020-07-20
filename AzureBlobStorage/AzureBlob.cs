using System;
using System.IO;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobStorage
{
 
/// <summary>
/// 
/// </summary>
    public static class AzureBlob
    {
        //Azure storage connection string
        //Read from configuration
        private static string _connectionString 
            = "DefaultEndpointsProtocol=https;AccountName=vpstorageaccount;AccountKey=MZBFlvxHd3wudESMO4oV9EQXEhiTD9I8p65U7Q7a2XnFvzGfB1MTE8drsgEpnipa2EH5b20Mf96iizK6vlpuAw==;EndpointSuffix=core.windows.net";
        
        private static BlobServiceClient _storageClient;
        private static BlobContainerClient _containerClient;

        /// <summary>
        /// Returns BlobServiceClient object
        /// </summary>
        internal static BlobServiceClient StorageClient => _storageClient ??= new BlobServiceClient(_connectionString);
        
        /// <summary>
        /// Returns BlobContainerClient object
        /// </summary>
        /// <param name="containerName">Blob storage container name</param>
        /// <returns>BlobContainerClient</returns>
        internal static BlobContainerClient GetContainerClient(string containerName=null)
        {
            containerName ??= Guid.NewGuid().ToString();
            return _containerClient ??= StorageClient.CreateBlobContainer(containerName);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public static Response<BlobContentInfo> Upload(Stream stream, string blobName)
        {
            BlobClient blobClient = GetContainerClient().GetBlobClient(blobName);
            var response= blobClient.Upload(stream, true);
            stream.Flush();
            stream.Close();
            return response;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="blobName"></param>
        /// <returns></returns>
        public static Response<BlobContentInfo> Upload(byte[] buffer, string blobName)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(buffer, 0, buffer.Length);
            memoryStream.Position = 0;
            return Upload(memoryStream, blobName);
        }

    }
}    