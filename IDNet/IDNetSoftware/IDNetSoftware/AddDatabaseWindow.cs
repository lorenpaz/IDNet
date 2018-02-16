using System;
namespace IDNetSoftware
{
    public partial class AddDatabaseWindow : Gtk.Window
    {
        public AddDatabaseWindow() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }
    }
}
