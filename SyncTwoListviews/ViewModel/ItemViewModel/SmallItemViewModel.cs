using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GalaSoft.MvvmLight.Ioc;


namespace DoubleListViewItemTemplates.Model
{
    public class SmallItemViewModel : BaseItemEntityViewModel
    {
        public SmallItemViewModel(string title,
            ItemTemplateType itemTemplate)
        {
            Title = title;
            ItemTemplate = itemTemplate;
        }
    }
}