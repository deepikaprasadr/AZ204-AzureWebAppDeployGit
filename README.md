# AZ204-AzureWebAppDeployGit
LearnModule26 WebApp4 -Stage a web app deployment for testing and rollback by using App Service deployment slots


68. StartCopyFromUri Method
Focus
Get the Access keys of the storage account
Create a console app in visual studio with .net v 5.0
Install nuget package for blob to be accessed in vs


Now of course we can also use Visual Studio
and other programmatic methods
to access the storage account using the SDK.


Create a console app in .Net5


And so it doesn't have to be a website,
it can just be a console application
or some type of Windows application.
Console app name: StorageCopy

And if you just pick the console application,
give it a name, call it StorageCopy, .NET 5.
There are basically extensions in NuGet.

Install a Nuget Pkg mgr Azure.Storage.Blobs:
So if you go to NuGet Package Manager
and you look at the NuGet packages for the solution,
if you enter in storage,
then you're going to see the official
Microsoft Azure.Storage.Blobs NuGet package.
V 12.12

We can attach it to this.
There are the ability to pick versions.
And so this, as of the time of recording, 12.12.
Say Install.
I do have to install dependencies, accept those.
So these are all the dependencies.
Azure.Core, Azure.Storage.Common, et cetera.
They'll also have to accept the licenses
'cause there's MIT licensed code here.
And what this will do is it will make
these Azure storage SDKs available to this project.
So if I go under a Solution Explorer
and I go under the Program again,
then I can start to say using Azure.Storage.Blobs.
And then I can start to use the methods
within this namespace.


Aim: copy Blob from container to another:
So we're going to try to create some code here
that will operate on our blob storage account,
and it's going to copy a blob
from one of the containers into another.\
using Azure.Storage.Blobs;

Storage Account-> Get Access Keys:-


And so we're gonna start with the connection string.
And the connection string is actually provided
by the account keys.
So we'll pop over to here, go into Access keys, Show keys,
copy the connection string.
All right, so this is the connection string to the account.
And we're going to create ourselves a client.

Connection string:
DefaultEndpointsProtocol=https;AccountName=storageacc1deepikajuly3;AccountKey=YCpRVWcc9KcikEVAVy77r2d+Df4RHTjb6Qm9k4SQLnGn4Xf9VDGJfwhw/iYJHZMsz/lzuTb9+GFM+AStJXRcHA==;EndpointSuffix=core.windows.net


In program.cs file this goes as the var connectionstring.
      var connectionstring = "DefaultEndpointsProtocol=https;AccountName=storageacc1deepikajuly3;AccountKey=YCpRVWcc9KcikEVAVy77r2d+Df4RHTjb6Qm9k4SQLnGn4Xf9VDGJfwhw/iYJHZMsz/lzuTb9+GFM+AStJXRcHA==;EndpointSuffix=core.windows.net";




Program.cs
using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;


namespace StorageCopy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connectionstring = "DefaultEndpointsProtocol=https;AccountName=storageacc1deepikajuly3;AccountKey=YCpRVWcc9KcikEVAVy77r2d+Df4RHTjb6Qm9k4SQLnGn4Xf9VDGJfwhw/iYJHZMsz/lzuTb9+GFM+AStJXRcHA==;EndpointSuffix=core.windows.net";
            Console.WriteLine("Hello World!");
            var blobclient = new BlobServiceClient(connectionstring);
            var sourcecontainer = "container1";
            var destinationcontainer = "container2";
            var sourcefile = "PRoverb.JPG";
            var destfile = "PRoverb-VisualStudio-copy.JPG";


            var sourceclient = new BlockBlobClient(connectionstring, sourcecontainer, sourcefile);
            var destclient = new BlockBlobClient(connectionstring, destinationcontainer,destfile);


            destclient.StartCopyFromUri(sourceclient.Uri);
        }


    }
}


Let’s bisect the code:
 var blobclient = new BlobServiceClient(connectionstring);
           
So let's call this blobclient.
And we're gonna use the new BlobServiceClient
using this connection string.
And so now this is, again, the client object
that's going to allow us to work on this account.
Oh, the connectionstring is small letters.


code:
   var sourcecontainer = "container1";
            var destinationcontainer = "container2";
            var sourcefile = "PRoverb.JPG";
            var destfile = "PRoverb-VisualStudio-copy.JPG";


And we're going to say
the sourcecontainer is the firstcontainer
and the destcontainer is the secondcontainer.
The sourcefile.
So Containers, firstcontainer.
So the file's gonna be this.
And the destination file,
since we've already done this copy using AzCopy,
I'm gonna append the letters COPY to this.


code:
var sourceclient = new BlockBlobClient(connectionstring, sourcecontainer, sourcefile);
var destclient = new BlockBlobClient(connectionstring, destinationcontainer,destfile);
destclient.StartCopyFromUri(sourceclient.Uri);


And so we're going to create
the sourceclient = new BlockBlobClient,
and it's going to be the connectionstring,
and forgive my typing here,
the sourcecontainer and the sourcefile.


var destclient = new BlockBlobClient(connectionstring, destinationcontainer,destfile);


Now you'll see here I haven't yet included the client.
So I'm gonna do Control + Period,
and it'll show me the using statement
that I need to use in order to get this,
so the specialized namespace.
And I'm gonna copy this and create the destination client.
And we're going to use the destination container
and the destination file for that.


destclient.StartCopyFromUri(sourceclient.Uri);




So now we have objects that we can use to copy this, right?
So this StartCopyFromUri will actually grab
the source file by URI and copy this to our destination.


We're not using the SAS token here.
We are providing the account key
as part of the connection string.
And that will give us authorization to perform this task.


Now that's if you're using the same storage account
from the source and the destination.
If you are using two different storage accounts,
then you are going to have to generate the SAS token.
And that will be a slightly different method for that.
So if I run this code, I don't think I even need this,
it's an unused variable, if I run this code,


Code execution and results expectations:-


I am expecting this file
to have now two copies in the second container,
second one having the word COPY in it.
So let's hit a five and build the thing, let it run.
There is no output and then there's no return,
so it's just gonna end with this exit code 0.


Now if we go back to the account,
we go to the secondcontainer, we have two copies,
one that we did with AzCopy and one that we did in code.
 


Before execution of copy 





Success:
After the code execution: the file was copied



69. SetMetadata Method


Focus:
1.GetProperties & Retrieving methods using code 
2.Quick Watch on BlobProperties:-
 Image of blobProperties
3.Code to update metadata dictionary & write it on Azure Portal:
4. Result executed successfully (in visual studio)
Result on Azure Portal(check the metadata for the blob file uploaded):-
6.Portal Azure-> storage acc->container->blob file-> properties->metadata


1.GetProperties & Retrieving methods using code 
So let's talk about
getting and retrieving properties using this code.
I've commented out the copy from your ICommand
and I've placed that with this GetProperties method.
So I can go to the source or to any
file that I've properly set up using the BlockBlobClient
and I can say GetProperties.
Code:
//connectionstring is from storage’s access key
var sourceclient = new BlockBlobClient(connectionstring, sourcecontainer, sourcefile);
BlobProperties properties=sourceclient.GetProperties();


I'm gonna run this code and I put a debug point
on the next line
and we can actually see how this plays out.


Quick Watch on BlobProperties:-
 Image of blobProperties

Now first of all,
the GetProperties returns this BlobProperties object.
And so if I said a Quick Watch on BlobProperties,
I can see that the various details of this file.
Various properties of blob from image are as follows:-
So it's currently set into the cool tier.


Name
Value
Type


AccessTier
"Cool"
string



It is...
it's not been specifically said it's an inferred tier.




Name
Value
Type


ContentType
"image/jpeg"
string



We can see that it is a  Proverb.jpeg  file.
We can see the size of it.
we can see various dates the data was created on,
hasn't been accessed, last modified date.


Now there is this LeaseState
which means that if we wanted to do some type of action
on this file,
we could of course grab the lease for it
which will lock it and make it unavailable
for other people to modify
so there's a locking mechanism built into here.


Metadata:
Initially in metadata the dictionaries are empty.





If I had any metadata on it,
I could grab the metadata from here.
Why don't we set some metadata onto this file?
Okay I'm going to close Quick Watch.
I did put th
3.Code to update metadata dictionary & write it on Azure Portal:
 var sourceclient = new BlockBlobClient(connectionstring, sourcecontainer, sourcefile);
//adding metadata to the blob container
   IDictionary<string, string> metadata = new Dictionary<string, string>();
   metadata.Add("CreatedBy", "Deepika Prasad");
   metadata["environment"] = "development";
   sourceclient.SetMetadata(metadata);


So in order to set metadata,
we need to create a new dictionary object
and I'm calling it metadata.
And then we can basically add pairs of variables
with the metadata name and the metadata value.
And I can use the metadata.add method
or I can just use it as a dictionary object
way that I pass the key and pass it a value.
And so my expectation is that after running this code,
the source container that contains the file
from the source client
is gonna have these two new metadata values added to it.


I set a break point at the very end of the code
so I'm gonna say debug
and we've reached the debug point.


4. Result executed successfully (in visual studio) 


Result: executed successfully



So it successfully ran by adding metadata
and hopefully adding the metadata to the source file.
Now there are a couple of ways that I can check this, right?
Result on Azure Portal(check the metadata for the blob file uploaded):-
 Portal Azure-> storage acc->container->blob file-> properties->metadata
 
Portal Azure-> storage acc-> container1-> PRoverb.jpg(file)-> properties->Metadata -> key values are added 
Created By: Deepika Prasad

I can go in into the portal,
I can go into the first container
and I can go to the file, this source file,
to the three dots, say properties
and I can see the metadata value
that has been successfully added based on my code.
The other way to do this, of course, is to rerun this code.
I'm going to set a breakpoint.
I'm gonna set a breakpoint at this Console.WriteLine again
and I'm going to run the debug.
We are now broken
and if I was to do a Quick Watch on the properties,
scroll down to the metadata,
I can see there are two properties
and the other properties that I created.
So we can see
that the SetMetadata command
was successfully able to add this dictionary of metadata
to to the source file
and we are able to verify that a couple of different ways.


