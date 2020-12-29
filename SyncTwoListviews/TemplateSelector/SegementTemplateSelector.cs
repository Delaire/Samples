using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using DoubleListViewItemTemplates.Model;

namespace DoubleListViewItemTemplates.TemplateSelector
{
    public class SegementTemplateSelector : DataTemplateSelector
    {
        //topic
        public DataTemplate KeyHorizontalControl = Application.Current.Resources["KeyHorizontalControl"] as DataTemplate;
        public DataTemplate KeyVerticalControl = Application.Current.Resources["KeyVerticalControl"] as DataTemplate;

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            var element = item as BaseSegementEntityViewModel;

            if (element != null)
            {
                if (element.ItemType == SegmentType.HorizontalSegment)
                    return KeyHorizontalControl;
                else
                    return KeyVerticalControl;
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}
