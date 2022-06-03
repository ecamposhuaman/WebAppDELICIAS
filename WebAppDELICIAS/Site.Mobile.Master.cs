using System;
using System.Web;

namespace WebAppDELICIAS
{
    public partial class Site_Mobile : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var AlternateView = "Desktop";
            var switchViewRouteName = "AspNet.FriendlyUrls.SwitchView";
            var url = GetRouteUrl(switchViewRouteName, new { view = AlternateView, __FriendlyUrls_SwitchViews = true });
            url += "?ReturnUrl=" + HttpUtility.UrlEncode(Request.RawUrl);
            Response.Redirect(url);
        }
    }
}