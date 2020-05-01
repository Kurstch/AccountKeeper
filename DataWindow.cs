using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
            this.Size = new Size(600, 700);
        }

        private void InitializeMenuStrip()
        {
            menuStrip = new MainMenuStrip(this);
            this.Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void InitializeListView()
        {
            listView = new AccountListView();

            listView.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 40);
            listView.Location = new Point(10, 30);

            this.Controls.Add(listView);

            LoadData();
        }

        public void AddNewListViewItem(ListViewItem data)
        {
            accounts.Add(data);
            UpdateListView();
            SaveData();
        }

        private void SaveData()
        {
            Stream stream = File.Open(@"C:\Saves\AccountKeeper\save.xml", FileMode.Create);

            XElement xml = new XElement("Accounts", accounts.Select(account => new XElement("account",
                new XAttribute("website", account.SubItems[1]),
                new XAttribute("e-mail", account.SubItems[2]),
                new XAttribute("username", account.SubItems[3]))));
            xml.Save(stream);

            stream.Close();
        }

        private void LoadData()
        {
            accounts = new List<ListViewItem>();

            string filePath = @"C:\Saves\AccountKeeper\save.xml";

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(@"C:\Saves\AccountKeeper");
                FileStream fs = File.Create(filePath);
                fs.Close();
            }

            try
            {
                XDocument xmlDoc = XDocument.Load(filePath);
                XElement ac = xmlDoc.Element("Accounts");

                foreach (XElement account in ac.Elements())
                {
                    ListViewItem listViewItem = new ListViewItem();
                    foreach (XAttribute atribute in account.Attributes())
                    {
                        listViewItem.SubItems.Add(atribute.Value.Split('{', '}')[1]);
                    }
                    accounts.Add(listViewItem);
                }
                UpdateListView();
            }
            catch
            {

            }
        }

        public void UpdateListView()
        {
            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (ListViewItem account in accounts)
            {
                listView.Items.Add(account);
            }
            listView.EndUpdate();
        }
    }
}
