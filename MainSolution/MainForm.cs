using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PluginsLibrary;

namespace MainSolution
{
    public partial class MainForm : Form
    {
        private string salida;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PluginMongo mongo = new PluginMongo();
            mongo.CallScript();
            mostrarLabel.Text = mongo.Salida; 
        }
       // PluginMongo <-----------------> db

        private void Button2_Click(object sender, EventArgs e)
        {
            PluginMySQL mysql = new PluginMySQL();
            mysql.CallScript();
            mostrarLabel.Text = mysql.Salida;
        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }
    }
}
