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
    public partial class DataWindow : Form
    {
        private MainMenuStrip menuStrip = null;
        private AccountListView listView = null;
        private List<ListViewItem> accounts = null;

        public DataWindow()
        {
            InitializeComponent();
            InitializeWindow();
            InitializeListView();
            InitializeMenuStrip();
        }

        private void InitializeWindow()
        {
            this.BackColor = Color.FromArgb(38, 38, 38);
        }

        private void InitializeMenuStrip()
        {
            menuStrip = new MainMenuStrip(this);
            this.Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
        }

        private void InitializeListView()
        {
            accounts = new List<ListViewItem>();
            listView = new AccountListView();

            listView.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 40);
            listView.Location = new Point(20, 40);

            this.Controls.Add(listView);
        }

        public void ListViewAddNewAccount(ListViewItem data)
        {
            accounts.Add(data);

            listView.Items.Clear();
            foreach (ListViewItem account in accounts)
            {
                listView.Items.Add(account);
            }
        }
    }
}
