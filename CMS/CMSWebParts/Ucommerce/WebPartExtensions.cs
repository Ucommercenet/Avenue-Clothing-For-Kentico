using CMS.PortalEngine.Web.UI;
using System.Linq;
using System.Web.UI;

public static class WebPartExtensions
{
    public static T FindWebPart<T>(this Page page) where T : CMSAbstractWebPart
    {
        return FindWebPart<T>(page.Controls);
    }
    public static T FindWebPart<T>(this Control control) where T : CMSAbstractWebPart
    {
        return FindWebPart<T>(control.Controls);
    }

    private static T FindWebPart<T>(ControlCollection childControls)
    {
        // first level
        var webParts = childControls.OfType<T>();
        if (webParts.Any())
        {
            return webParts.First();
        }

        foreach (Control control in childControls)
        {
            var webPart = FindWebPart<T>(control.Controls);
            if (webPart != null)
                return webPart;
        }

        return default(T);
    }
}