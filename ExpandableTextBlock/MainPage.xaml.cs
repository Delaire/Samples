using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ExpandableTextBlock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public string Description { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis lobortis libero vel justo sollicitudin auctor. Nulla blandit pulvinar augue ac feugiat. Ut eros diam, lacinia mollis laoreet at, aliquam et metus. Vivamus ac dictum urna. In gravida dapibus lacus, ut ullamcorper risus congue ac. Cras ac volutpat lorem, eu imperdiet urna. Etiam volutpat venenatis magna. Vestibulum a tellus lobortis, varius nibh ut, molestie metus. Nunc tincidunt quis urna vel feugiat. Aenean placerat ut augue pharetra pharetra. Nam metus eros, pulvinar quis egestas ut, varius ac dui. Pellentesque eros arcu, tristique non sem quis, sodales congue magna. Cras eleifend porttitor lacus, et blandit lacus imperdiet id. Fusce id eros sed nulla consequat tempus.Ut commodo, ligula sed pretium mollis, sapien purus pulvinar purus, vel viverra sem sem sed tortor.Sed sagittis rhoncus diam. Suspendisse eget suscipit magna, nec auctor tortor. Nunc blandit congue tellus quis feugiat. Sed dapibus semper auctor. Nam pretium tempus mi quis posuere. Curabitur felis eros, pellentesque vel aliquam eget, hendrerit in orci.Morbi ut rhoncus massa. Curabitur fringilla orci id est pharetra vestibulum.Vestibulum iaculis, eros et sodales scelerisque, ante dui interdum nisl, consectetur laoreet diam urna sit amet dolor. Suspendisse mattis dolor eu nulla volutpat, vitae accumsan sapien commodo.Vivamus id turpis sem.";
        }
    }
}
