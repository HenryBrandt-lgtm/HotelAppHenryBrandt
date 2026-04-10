namespace HotelApp.MenuData
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
