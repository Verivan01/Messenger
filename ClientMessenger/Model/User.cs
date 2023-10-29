using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMessenger.Model
{
    internal class User
    {
        public string Name { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
    }
}
