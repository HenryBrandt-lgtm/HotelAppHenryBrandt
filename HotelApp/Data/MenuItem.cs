using System;
using System.Collections.Generic;
using System.Text;

namespace HotelApp.Data
{
    public class MenuItem
    {
        public string Title { get; set; }
        public Action Action { get; set; }

        public MenuItem(string title, Action action)
        {
            Title = title;
            Action = action;
        }
    }
}
