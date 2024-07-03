using System;
using System.Collections;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System.Collections.Generic;

namespace StorageCopy
{
    internal class Program
    {
        static void Main(string[] args)
        {
          var connectionstring = "DefaultEndpointsProtocol=https;AccountName=storageacc1deepikajuly3;AccountKey=***;EndpointSuffix=core.windows.net";

            Console.WriteLine("Hello World!");
            var blobclient = new BlobServiceClient(connectionstring);
            var sourcecontainer = "container1";
            var destinationcontainer = "container2";
            var sourcefile = "PRoverb.JPG";
            var destfile = "PRoverb-VisualStudio-copy.JPG";

            var sourceclient = new BlockBlobClient(connectionstring, sourcecontainer, sourcefile);
            var destclient = new BlockBlobClient(connectionstring, destinationcontainer,destfile);
            destclient.StartCopyFromUri(sourceclient.Uri);
            BlobProperties properties=sourceclient.GetProperties();
         //adding metadata to the blob container
            IDictionary<string, string> metadata = new Dictionary<string, string>();
            metadata.Add("CreatedBy", "Deepika Prasad");
            metadata["environment"] = "development";
            sourceclient.SetMetadata(metadata);
            //git create and push done// added to the branch
        }

    }
}
