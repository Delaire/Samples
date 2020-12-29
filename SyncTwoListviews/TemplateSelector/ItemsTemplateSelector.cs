using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DoubleListViewItemTemplates.Model;

namespace DoubleListViewItemTemplates.TemplateSelector
{
    public class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate KeyCircleUserControl = Application.Current.Resources["KeyCircleUserControl"] as DataTemplate;
        public DataTemplate KeySquareUserControl = Application.Current.Resources["KeySquareUserControl"] as DataTemplate;
        
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            var element = item as BaseItemEntityViewModel;
            
            if (element != null)
            {
                switch (element.ItemTemplate)
                {
                    case ItemTemplateType.CircleUserItem:
                        return KeyCircleUserControl;


                    default:
                        return KeySquareUserControl;
                }

            }
            return base.SelectTemplateCore(item, container);
        }
    }
}