using CMS.MediaLibrary;
using CMS.SiteProvider;

namespace AvenueClothing.Installer.uCommerce.Install.Helpers
{
	public class MediaInstallerEms : MediaInstaller
	{
		private string emsFolder = "EMS";

		public override void CreateAndUploadAllMediaFiles(int libraryId)
		{
			base.CreateAndUploadAllMediaFiles(libraryId);
			CreateAndUploadMediaInFolder(libraryId, emsFolder);

		}

		public override void CreateMediaLibraryFolders(int libraryId)
		{
			base.CreateMediaLibraryFolders(libraryId);
			MediaLibraryInfoProvider.CreateMediaLibraryFolder(SiteContext.CurrentSiteName, libraryId, emsFolder);

		}

	}
}
