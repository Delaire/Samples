using ReactiveUI_UWP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI_UWP.Services
{
    public class MyItemService
    {
        public IObservable<IEnumerable<MyItem>> GetUpcomingItems(int index)
        {
            //get your items here
            //do your http call here

            return null;
        }
    }
}
