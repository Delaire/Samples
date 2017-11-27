using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SlidableItemSample.Model
{
    public class ItemsDataModel : INotifyPropertyChanged
    {

        public string Title { get; set; }



        public ICommand CallTopActionItemCommand
        {
            get
            {
                //do an action
                return null;
            }
        }

        public ICommand CallBottomItemCommand
        {
            get
            {
                //do an action
                return null;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


    }
}
