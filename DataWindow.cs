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
        private AccountDataGridView dgv = null;
        private List<string[]> accounts = null;

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
            dgv = new AccountDataGridView();

            dgv.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 40);
            dgv.Location = new Point(10, 30);

            this.Controls.Add(dgv);

            LoadData();
        }

        public void AddNewListViewItem(string[] data)
        {
            accounts.Add(data);
            UpdateListView();
            SaveData();
        }

        private void SaveData()
        {
            Stream stream = File.Open(@"C:\Saves\AccountKeeper\save.xml", FileMode.Create);

            XElement xml = new XElement("Accounts", accounts.Select(account => new XElement("account",
                new XAttribute("website", account[0]),
                new XAttribute("e-mail", account[1]),
                new XAttribute("username", account[2]))));
            xml.Save(stream);

            stream.Close();
        }

        private void LoadData()
        {
            accounts = new List<string[]>();

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

                    foreach (XAttribute atribute in account.Attributes())
                    {
                        
                    }
                    //accounts.Add();
                }
                UpdateListView();
            }
            catch
            {

            }
        }

        public void UpdateListView()
        {
            foreach (string[] account in accounts)
            {
                dgv.Rows.Add(account);
            }
        }
    }
}
