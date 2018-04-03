using System;
namespace IDNetSoftware
{
    public partial class AcercadeDialog : Gtk.Dialog
    {
        public AcercadeDialog()
        {
            this.Build();
        }

        protected void OnButtonOkClicked(object sender, EventArgs e)
        {
            this.Destroy();
        }
    }
}
