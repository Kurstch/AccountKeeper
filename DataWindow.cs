using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace AccountKeeper
{
    public partial class DataWindow : Form
    {
        private MainMenuStrip menuStrip = null;
        private AccountDataGridView dgv = null;
        private List<string[]> accounts = null;

        private string filePath = @"C:\Saves\AccountKeeper\save.xml";
        private string dirPath = @"C:\Saves\AccountKeeper";

        public DataWindow()
        {
            InitializeComponent();
            InitializeWindow();
            InitializeListView();
            InitializeMenuStrip();
        }

        //Initializations
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
            dgv = new AccountDataGridView(this);

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
            Stream stream = File.Open(filePath, FileMode.Create);

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(dirPath);
                FileStream fs = File.Create(filePath);
                fs.Close();
            }

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

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(dirPath);
                FileStream fs = File.Create(filePath);
                fs.Close();
            }

            try
            {
                XDocument xmlDoc = XDocument.Load(filePath);
                XElement root = xmlDoc.Element("Accounts");

                foreach (XElement account in root.Elements())
                {
                    string[] accountData = new string[3];

                    accountData[0] = account.Attribute("website").Value;
                    accountData[1] = account.Attribute("e-mail").Value;
                    accountData[2] = account.Attribute("username").Value;

                    accounts.Add(accountData);
                }
                UpdateListView();
            }
            catch
            {
                UpdateListView();
            }
        }

        private void UpdateListView()
        {
            dgv.Rows.Clear();
            foreach (string[] account in accounts)
            {
                dgv.Rows.Add(account);
            }
            dgv.UpdateCellColor();
        }

        public void RemoveAccountFromList(int index)
        {
            accounts.RemoveAt(index);
            SaveData();
        }
    }
}
