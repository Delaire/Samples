using System;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;


namespace MainApp.Connector
{
    [AllowForWeb]
    public sealed class HtmlCommunicator
    {
        public void getHtmlSpecificEvent()
        {
          // do something else
        }

        public string getAppVersion()
        {
            PackageVersion version = Package.Current.Id.Version;
            return String.Format("{0}.{1}.{2}.{3}",
                                 version.Major, version.Minor, version.Build, version.Revision);
        }
    }
}
