using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountKeeper
{
    public partial class DataWindow : Form
    {
        MenuStrip menuStrip = null;

        public DataWindow()
        {
            InitializeComponent();
            InitializeUiBackground();
        }

        private void InitializeUiBackground()
        {
            this.BackColor = Color.FromArgb(38, 38, 38);

            menuStrip = new MenuStrip();
            menuStrip.Dock = DockStyle.Top;
            menuStrip.BackColor = Color.FromArgb(85, 85, 85);
            this.Controls.Add(menuStrip);
        }
    }
}
