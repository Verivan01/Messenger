using ClientMessenger.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMessenger.ViewModel
{
    internal class MainViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        
        public MainViewModel() 
        { 
            Users= new ObservableCollection<User>();
           

            for (int i = 0; i < 6; i++)
            {
                Users.Add(new User()
                {
                    Name = $"Friend {i}",
                    
                });
            }
        }

    }
}
