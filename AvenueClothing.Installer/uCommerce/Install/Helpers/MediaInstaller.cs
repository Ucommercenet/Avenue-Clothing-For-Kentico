using System;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using System.Web;
using CMS.Helpers;
using UCommerce.EntitiesV2;
using System.Linq;
using System.Text;


namespace AvenueClothing.Installer.uCommerce.Install.Helpers
{
	public class MediaInstaller
	{
		private string productFolder = "Product";
		private string categoryFolder = "Category";

		public void Configure()
		{
			if (MediaLibraryInfoProvider.GetMediaLibraryInfo("AvenueClothing", SiteContext.CurrentSiteName) == null)
			{
				CMS.DataEngine.CMSApplication.Init();
				var libraryId = CreateMediaLibrary();
				CreateMediaLibraryFolders(libraryId);
				CreateAndUploadMediaFiles(libraryId);
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

		private void UploadImages(CMS.FileSystemStorage.DirectoryInfo imagesDirectory)
		{
			//Convert to System.IO.DirectoryInfo to get the parent (outside the CMS folder) without hardcoding.
			var fromParentDirectory = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath("/"));
			var fromPath = new StringBuilder().Append(fromParentDirectory.FullName)
				.Append("AvenueClothing.Installer/uCommerce/Install/Files/")
				.Append(imagesDirectory.Name)
				.Append("/")
				.ToString();
			//Create path to location in installer where the files will be copied from
			var fromDirectory = new System.IO.DirectoryInfo(fromPath);

			foreach (var file in fromDirectory.GetFiles())
			{
				var path = new StringBuilder(imagesDirectory.FullName)
					.Append("/")
					.Append(file.Name)
					.ToString();

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
