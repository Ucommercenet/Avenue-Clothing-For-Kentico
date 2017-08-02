using System;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using System.Web;
using CMS.FileSystemStorage;
using CMS.Helpers;
using UCommerce.EntitiesV2;
using System.Linq;
using System.Text;
using System.IO;


namespace AvenueClothing.Installer.uCommerce.Install.Helpers
{
    public class MediaInstaller
    {
        private string productFolder = "Product";
        private string categoryFolder = "Category";

        public void Configure()
        {
            if(MediaLibraryInfoProvider.GetMediaLibraryInfo("AvenueClothing", SiteContext.CurrentSiteName) != null) {
                return;
            }
            
            CMS.DataEngine.CMSApplication.Init();
            var libraryId = CreateMediaLibrary();
            CreateMediaLibraryFolders(libraryId);
            CreateAndUploadMediaFiles(libraryId);
            //AddUcommerceProductImages(libraryId);
        }

		//TO DO
        private void AddUcommerceProductImages(int libraryId)
        {
            var products = Product.All().ToList();

            foreach(var product in products)
            {
                if (product.IsVariant == true)
                    continue;

                //if (!String.IsNullOrWhiteSpace(product.ThumbnailImageMediaId) && !String.IsNullOrWhiteSpace(product.ThumbnailImageMediaId))
                //    continue;

                //if (!String.IsNullOrWhiteSpace(product.VariantSku))
                //    continue;

                var media = MediaFileInfoProvider.GetMediaFiles().ToList();
                foreach(var medium in media)
                {
                    if(medium.FileName == product.Sku)
                    {
                        product.PrimaryImageMediaId = EncodePath(HttpContext.Current.Server.MapPath("~/AvenueClothingMVC/media/" + medium.FilePath));
                        product.ThumbnailImageMediaId = EncodePath(HttpContext.Current.Server.MapPath("~/AvenueClothingMVC/media/" + medium.FilePath));
                        product.Save();
                    }
                }
            }

        }

        private void CreateAndUploadMediaFiles(int libraryId)
        {
	        // Prepares a path to a local file
			var pathWithSiteName = "~/" + SiteContext.CurrentSiteName + "/media/AvenueClothing/";
			string filePath = HttpContext.Current.Server.MapPath(pathWithSiteName);
			
			var productImagesDirectory = new CMS.FileSystemStorage.DirectoryInfo(filePath + productFolder);
	        UploadImages(productImagesDirectory);
	        CreateFilesAsMediaInfos(libraryId, productImagesDirectory, productFolder);

			var categoryImagesDirectory = new CMS.FileSystemStorage.DirectoryInfo(filePath + categoryFolder);
			UploadImages(categoryImagesDirectory);
			CreateFilesAsMediaInfos(libraryId, categoryImagesDirectory, categoryFolder);

		}

		private void UploadImages(CMS.FileSystemStorage.DirectoryInfo imagesDirectory)
		{
			//Convert to System.IO.DirectoryInfo to get the parent (outside the CMS folder) without hardcoding.
			var fromParentDirectory = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath("../")).Parent;
			//Create path to location in installer where the files will be copied from
			var fromPath = new System.IO.DirectoryInfo(fromParentDirectory.FullName + "/AvenueClothing.Installer/uCommerce/Install/Files/" +
			               imagesDirectory.Name + "/");

			foreach (var file in fromPath.GetFiles())
			{
				file.CopyTo(imagesDirectory.FullName + "/" + file.Name);
			}
		}

		private static void CreateFilesAsMediaInfos(int libraryId, CMS.FileSystemStorage.DirectoryInfo productImagesDirectory, string folderName)
        {
            foreach (var file in productImagesDirectory.GetFiles())
            {
				// Prepares a CMS.IO.FileInfo object representing the local file
				if (file != null)
                {
                    // Creates a new media library file object
                    MediaFileInfo mediaFile = new MediaFileInfo(file.FullName, libraryId);

                    // Sets the media library file properties
                    mediaFile.FileName = file.Name.Replace(".jpg", "");
                    mediaFile.FileTitle = "File title";
                    mediaFile.FileDescription = "This file was added through the API.";
                    mediaFile.FilePath = folderName + "/" + file.Name; // Sets the path within the media library's folder structure
                    mediaFile.FileExtension = file.Extension;
                    mediaFile.FileMimeType = MimeTypeHelper.GetMimetype(file.Extension);
                    mediaFile.FileSiteID = SiteContext.CurrentSiteID;
                    mediaFile.FileLibraryID = libraryId;
                    mediaFile.FileSize = file.Length;

                    // Saves and imports the media library file
	                MediaFileInfoProvider.ImportMediaFileInfo(mediaFile);
				}
			}
        }

        private void CreateMediaLibraryFolders(int libraryId)
        {
            MediaLibraryInfoProvider.CreateMediaLibraryFolder(SiteContext.CurrentSiteName, libraryId, productFolder);
            MediaLibraryInfoProvider.CreateMediaLibraryFolder(SiteContext.CurrentSiteName, libraryId, categoryFolder);
        }

        private int CreateMediaLibrary()
        {
            MediaLibraryInfo newLibrary = new MediaLibraryInfo();

            // Sets the library properties
            newLibrary.LibraryDisplayName = "Avenue Clothing";
            newLibrary.LibraryName = "AvenueClothing";
            newLibrary.LibraryDescription = "This media library was created for Avenue Clothing";
            newLibrary.LibraryFolder = "AvenueClothing";
            newLibrary.LibrarySiteID = SiteContext.CurrentSiteID;

            // Saves the new media library to the database
            MediaLibraryInfoProvider.SetMediaLibraryInfo(newLibrary);

            return newLibrary.LibraryID;
        }

        private string EncodePath(string path)
        {
            var pathAsBytes = Encoding.UTF8.GetBytes(path);
            var encodedPath = Convert.ToBase64String(pathAsBytes);

            return encodedPath;
        }
    }
}
