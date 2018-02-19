using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WebViewInvokeScriptApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            string json = '{ "props":{ "param":{ "action":{ "gesture":"click","id":"63825b13-dff5-4f45-81e9-a9dbe2aca39b"},"screen":{ "id":"662dfa95-6ad2-43c1-b403-4e9a4118be4c"},"section":{ "id":"2bcd2be7-9c10-49e9-b71e-b77dee17c7fb","index":1} } }';

            CallPlayerMethod("load", "xcv3df", json);
        }


        public async void CallPlayerMethod(string method, string param, string dataJson)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("myJSObject.");
            builder.Append(method);
            builder.Append(string.Format("('{0}'", param)) ;
            builder.Append(string.Format(",JSON.parse('{0}'))", dataJson));           

            //call webview
            CallEvalWebviewMethod(builder.ToString());
        }

        private async void CallEvalWebviewMethod(string callMethod)
        {
            List<string> callingJsMethod = new List<string>();
            callingJsMethod.Add(callMethod);

            try
            {
                await MyWebView?.InvokeScriptAsync("eval", callingJsMethod);
            }
            catch (Exception e)
            {
                //silent fail
            }
        }
    }
}
