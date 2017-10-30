using System;
namespace IDNetSoftware
{
    public partial class AddDatabaseDialog : Gtk.Dialog
    {
        public AddDatabaseDialog()
        {
            this.Build();
        }

        protected void OnButtonCancelClicked(object sender, EventArgs e)
        {
        }
    }
}
