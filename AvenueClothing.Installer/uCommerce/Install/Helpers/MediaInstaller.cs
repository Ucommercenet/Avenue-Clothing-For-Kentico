using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using CMS.DataEngine;
using CMS.Helpers;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using UCommerce.EntitiesV2;

namespace AvenueClothing.Installer.uCommerce.Install.Helpers
{
	public class MediaInstaller
	{
		private string productFolder = "Product";
		private string categoryFolder = "Category";
	    private string emsFolder = "EMS";

        public void Configure()
		{
			if (MediaLibraryInfoProvider.GetMediaLibraryInfo("AvenueClothing", SiteContext.CurrentSiteName) == null)
			{
				CMSApplication.Init();
				var libraryId = CreateMediaLibrary();

                CreateMediaLibraryFolders(libraryId);
                CreateAndUploadMediaFiles(libraryId, emsFolder);
			    CreateAndUploadMediaFiles(libraryId, productFolder);
			    CreateAndUploadMediaFiles(libraryId, categoryFolder);

            }

			AddUcommerceProductImages();
			AddUcommerceCategoryImages();
		}

		private void AddUcommerceProductImages()
		{
			var products = Product.All().ToList();
			var media = MediaFileInfoProvider.GetMediaFiles().ToList();

			foreach (var product in products)
			{
				if (product.IsVariant)
					continue;

				foreach (var medium in media)
				{
					if (medium.FileName == product.Sku)
					{
						var path = new StringBuilder(@"AvenueClothing\media\AvenueClothing\").Append(medium.FilePath).ToString();

						product.PrimaryImageMediaId = EncodePath(path);
						product.ThumbnailImageMediaId = EncodePath(path);
						product.Save();
						break;
					}
				}
			}
		}

		private void AddUcommerceCategoryImages()
		{
			var categories = Category.All().ToList();
			var media = MediaFileInfoProvider.GetMediaFiles().ToList();

			foreach (var category in categories)
			{
				foreach (var medium in media)
				{
					if (medium.FileName == category.Name)
					{
						var path = new StringBuilder(@"AvenueClothing\media\AvenueClothing\").Append(medium.FilePath).ToString();

						category.ImageMediaId = EncodePath(path);
						category.Save();
						break;
					}
				}
			}
		}

	    private void CreateAndUploadMediaFiles(int libraryId, string folderPath)
	    {
	        var pathWithSiteName = new StringBuilder("~/")
	            .Append(SiteContext.CurrentSiteName)
	            .Append("/media/AvenueClothing/")
	            .ToString();

            string filePath = HttpContext.Current.Server.MapPath(pathWithSiteName);

            var directoryInfo = new DirectoryInfo(filePath + folderPath);
	        UploadImages(directoryInfo);
	        CreateFilesAsMediaInfos(libraryId, directoryInfo, folderPath);
	    }

	    private void UploadImages(DirectoryInfo imagesDirectory)
		{
			DirectoryInfo fromDirectory;
			//for subapplication
			if (HttpRuntime.AppDomainAppVirtualPath != "/")
			{

				var fromParentDirectory = new DirectoryInfo(HttpContext.Current.Server.MapPath("/"));
				var fromPath = Path.Combine(fromParentDirectory.FullName,
					"AvenueClothing.Installer/uCommerce/Install/Files/", imagesDirectory.Name);
				fromDirectory = new DirectoryInfo(fromPath);
			}
			else
			{
				//no subapplication. Site points to CMS folder
				var physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
				var fullPathToImages = Path.Combine(physicalApplicationPath, "..", "AvenueClothing.Installer/uCommerce/Install/Files/", imagesDirectory.Name);
				fromDirectory = new DirectoryInfo(fullPathToImages);
			}

			UploadImagesToFileSystem(imagesDirectory, fromDirectory);

		}

		private void UploadImagesToFileSystem(DirectoryInfo imagesDirectory, DirectoryInfo fromDirectory)
		{
			foreach (var file in fromDirectory.GetFiles())
			{
				var path = Path.Combine(imagesDirectory.FullName, file.Name);

				if (File.Exists(path) == false)
				{
					file.CopyTo(path);
				}
			}
		}


		private static void CreateFilesAsMediaInfos(int libraryId, DirectoryInfo productImagesDirectory,
			string folderName)
		{
			foreach (var file in productImagesDirectory.GetFiles())
			{
				// Prepares a CMS.IO.FileInfo object representing the local file
				if (file != null)
				{
					// Creates a new media library file object
					MediaFileInfo mediaFile = new MediaFileInfo(file.FullName, libraryId)
					{
						FileName = file.Name.Replace(".jpg", ""),
						FileTitle = "File title",
						FileDescription = "This file was added through the API.",
						FilePath = folderName + "/" + file.Name, // Sets the path within the media library's folder structure
						FileExtension = file.Extension,
						FileMimeType = MimeTypeHelper.GetMimetype(file.Extension),
						FileSiteID = SiteContext.CurrentSiteID,
						FileLibraryID = libraryId,
						FileSize = file.Length
					};

					// Saves and imports the media library file
					MediaFileInfoProvider.ImportMediaFileInfo(mediaFile);
				}
			}
		}

		private void CreateMediaLibraryFolders(int libraryId)
		{
			MediaLibraryInfoProvider.CreateMediaLibraryFolder(SiteContext.CurrentSiteName, libraryId, productFolder);
			MediaLibraryInfoProvider.CreateMediaLibraryFolder(SiteContext.CurrentSiteName, libraryId, categoryFolder);
			MediaLibraryInfoProvider.CreateMediaLibraryFolder(SiteContext.CurrentSiteName, libraryId, emsFolder);
		}

		private int CreateMediaLibrary()
		{
			MediaLibraryInfo newLibrary = new MediaLibraryInfo
			{
				LibraryDisplayName = "Avenue Clothing",
				LibraryName = "AvenueClothing",
				LibraryDescription = "This media library was created for Avenue Clothing",
				LibraryFolder = "AvenueClothing",
				LibrarySiteID = SiteContext.CurrentSiteID
			};

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
