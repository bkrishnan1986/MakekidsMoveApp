using System;
using System.Collections.Generic;
using System.Text;

namespace MakeKidsMoveApp.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        SignUp
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
