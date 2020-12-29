using DoubleListViewItemTemplates.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 

namespace DoubleListViewItemTemplates.SegmentViewModel
{
    public class VerticalSegmentViewModel : BaseSegementEntityViewModel
    {
        public VerticalSegmentViewModel(List<SmallItemViewModel> items)
        {
            ItemType = SegmentType.VerticalSegment;

            MyItemSource = new ObservableCollection<BaseItemEntityViewModel>();

            foreach (var item in items)
            {
                //transfom
                MyItemSource.Add(item);
            }
        }

        //other code for VerticalSegmentViewModel

    }
}
