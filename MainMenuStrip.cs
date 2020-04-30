using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountKeeper
{
    class MainMenuStrip : MenuStrip
    {
        private ToolStripMenuItem accountsMenu = null;
        private DataWindow dw = null;

        public MainMenuStrip(DataWindow tempdw)
        {
            dw = tempdw;
            InitializeMenuStrip();
            InitializeAccountsMenu();
        }

        private void InitializeMenuStrip()
        {
            this.Dock = DockStyle.Top;
            this.BackColor = Color.FromArgb(85, 85, 85);
            this.ForeColor = Color.FromArgb(205, 205, 205);
        }

        private void InitializeAccountsMenu()
        {
            ToolStripMenuItem addAccount = null;

            accountsMenu = new ToolStripMenuItem();
            accountsMenu.BackColor = Color.FromArgb(85, 85, 85);
            accountsMenu.ForeColor = Color.FromArgb(205, 205, 205);
            accountsMenu.Text = "Accounts";

            addAccount = new ToolStripMenuItem();
            addAccount.BackColor = Color.FromArgb(85, 85, 85);
            addAccount.ForeColor = Color.FromArgb(205, 205, 205);
            addAccount.Text = "Add Account";
            addAccount.MouseDown += new MouseEventHandler(AddAccount_Click);

            accountsMenu.DropDownItems.AddRange(new ToolStripMenuItem[] { addAccount});

            this.Items.Add(accountsMenu);
        }

        private void AddAccount_Click(object sender, MouseEventArgs e)
        {
            new AddAccount(dw).Show();
        }
    }
}
