using System;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using System.Web;
using CMS.Helpers;
using UCommerce.EntitiesV2;
using System.Linq;
using System.Text;
using CMS.FileSystemStorage;


namespace AvenueClothing.Installer.uCommerce.Install.Helpers
{
	public class MediaInstaller
	{
		private string productFolder = "Product";
		private string categoryFolder = "Category";

		public void Configure()
		{
			//int libraryId = MediaLibraryInfoProvider.GetMediaLibraryInfo("AvenueClothing", SiteContext.CurrentSiteName).LibraryID;
			if (MediaLibraryInfoProvider.GetMediaLibraryInfo("AvenueClothing", SiteContext.CurrentSiteName) == null)
			{
				CMS.DataEngine.CMSApplication.Init();
				var libraryId = CreateMediaLibrary();
				CreateMediaLibraryFolders(libraryId);
				CreateAndUploadMediaFiles(libraryId);

			}
			//var libraryId = MediaLibraryInfoProvider.GetMediaLibraryInfo("AvenueClothing", SiteContext.CurrentSiteName).LibraryID;

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
						var path = new StringBuilder("~/AvenueClothing/media/AvenueClothing/").Append(medium.FilePath).ToString();

						category.ImageMediaId = EncodePath(path);
						category.Save();
						break;
					}
				}
			}
		}

		private void CreateAndUploadMediaFiles(int libraryId)
		{
			// Prepares a path to a local file
			var pathWithSiteName = new StringBuilder("~/")
				.Append(SiteContext.CurrentSiteName)
				.Append("/media/AvenueClothing/")
				.ToString();

			string filePath = HttpContext.Current.Server.MapPath(pathWithSiteName);

			var productImagesDirectory = new CMS.FileSystemStorage.DirectoryInfo(filePath + productFolder);
			UploadImages(productImagesDirectory);
			CreateFilesAsMediaInfos(libraryId, productImagesDirectory, productFolder);

			var categoryImagesDirectory = new CMS.FileSystemStorage.DirectoryInfo(filePath + categoryFolder);
			UploadImages(categoryImagesDirectory);
			CreateFilesAsMediaInfos(libraryId, categoryImagesDirectory, categoryFolder);
		}


		//private void UploadImages(CMS.FileSystemStorage.DirectoryInfo imagesDirectory)
		//{
		//	var physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
		//	var fullPathToImages = System.IO.Path.Combine(physicalApplicationPath, "..", "AvenueClothing.Installer/uCommerce/Install/Files/", imagesDirectory.Name);

		//	//Create path to location in installer where the files will be copied from
		//	var fromDirectory = new System.IO.DirectoryInfo(fullPathToImages);

		//	foreach (var file in fromDirectory.GetFiles())
		//	{
		//		var path = System.IO.Path.Combine(imagesDirectory.FullName, file.Name);

		//		if (System.IO.File.Exists(path) == false)
		//		{
		//			file.CopyTo(path);
		//		}
		//	}
		//}

		private void UploadImages(CMS.FileSystemStorage.DirectoryInfo imagesDirectory)
		{
			System.IO.DirectoryInfo fromDirectory;
			//for subapplication
			if (HttpRuntime.AppDomainAppVirtualPath != "/")
			{

				var fromParentDirectory = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath("/"));
				var fromPath = System.IO.Path.Combine(fromParentDirectory.FullName,
					"AvenueClothing.Installer/uCommerce/Install/Files/", imagesDirectory.Name);
				fromDirectory = new System.IO.DirectoryInfo(fromPath);
			}
			else
			{
				//no subapplication. Site points to CMS folder
				var physicalApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
				var fullPathToImages = System.IO.Path.Combine(physicalApplicationPath, "..", "AvenueClothing.Installer/uCommerce/Install/Files/", imagesDirectory.Name);
				fromDirectory = new System.IO.DirectoryInfo(fullPathToImages);
			}

			UploadImagesToFileSystem(imagesDirectory, fromDirectory);

		}

		private void UploadImagesToFileSystem(DirectoryInfo imagesDirectory, System.IO.DirectoryInfo fromDirectory)
		{
			foreach (var file in fromDirectory.GetFiles())
			{
				var path = System.IO.Path.Combine(imagesDirectory.FullName, file.Name);

				if (System.IO.File.Exists(path) == false)
				{
					file.CopyTo(path);
				}
			}
		}


		private static void CreateFilesAsMediaInfos(int libraryId, CMS.FileSystemStorage.DirectoryInfo productImagesDirectory,
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
