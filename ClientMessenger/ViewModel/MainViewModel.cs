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
        public ObservableCollection<Message> MessagesCollection { get; set; }
        public MainViewModel() 
        { 
            Users= new ObservableCollection<User>();
            MessagesCollection= new ObservableCollection<Message>();



            for (int i = 0; i < 5; i++)
            {
                MessagesCollection.Add(new Message()
                {
                    UserName = "Max",
                    Color = "#253645",
                    MessageData = $"MyMessage{i}",
                    Time = DateTime.Now
                });

            }

            for (int i = 0; i < 6; i++)
            {
                Users.Add(new User()
                {
                    Name = $"Friend {i}",
                    Messages = MessagesCollection
                });
            }
        }

    }
}
