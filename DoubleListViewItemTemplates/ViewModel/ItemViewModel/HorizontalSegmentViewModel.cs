using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoubleListViewItemTemplates.Model;
using Microsoft.VisualBasic;
 

namespace DoubleListViewItemTemplates.SegmentViewModel
{
   public class HorizontalSegmentViewModel : BaseSegementEntityViewModel
    {       
        public HorizontalSegmentViewModel(List<SmallItemViewModel> items)
        {
            ItemType = SegmentType.HorizontalSegment;

            MyItemSource = new ObservableCollection<BaseItemEntityViewModel>();

            foreach (var item in items)
            {
                //transfom
                MyItemSource.Add(item);
            }
        }

    }
}
