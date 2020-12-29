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

            Title = RandomString(8);

            MyItemSource = new ObservableCollection<BaseItemEntityViewModel>();

            foreach (var item in items)
            {
                //transfom
                MyItemSource.Add(item);
            }
        }

        private static Random random = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
