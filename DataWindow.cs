﻿using AccountKeeper.Properties;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AccountKeeper
{
    public partial class DataWindow : Form
    {
        private MainToolStrip toolStrip = null;
        private MainStatusStrip statusStrip = null;
        private AccountDataGridView dgv = null;
        private List<string[]> accounts = null;

        private string filePath = @"C:\Saves\AccountKeeper\save.xml";
        private string dirPath = @"C:\Saves\AccountKeeper";

        public DataWindow()
        {
            InitializeComponent();
            InitializeForm();
            initializeStatusStrip();
            InitializeMenuStrip();
            InitializeDataGridView();
        }

        //Initializations
        private void InitializeForm()
        {
            this.BackColor = Settings.Default.formBackColor;
            this.Size = new Size(600, 700);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void InitializeMenuStrip()
        {
            toolStrip = new MainToolStrip(this);
            this.Controls.Add(toolStrip);
        }

        private void initializeStatusStrip()
        {
            statusStrip = new MainStatusStrip();
            this.Controls.Add(statusStrip);
        }

        private void InitializeDataGridView()
        {
            dgv = new AccountDataGridView(this);

            dgv.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 40);
            dgv.Location = new Point(10, 30);

            this.Controls.Add(dgv);

            LoadData();
        }

        //Save, load data
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
            statusStrip.UpdateItemCountLabel(dgv.RowCount - 1);
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
            }
            catch
            {
                
            }
            UpdateDataGridView();
            statusStrip.UpdateItemCountLabel(dgv.RowCount - 1);
        }

        //DataGridView management
        public void AddNewAccount(string[] data)
        {
            accounts.Add(data);
            UpdateDataGridView();
        }

        public void RemoveAccount(int index)
        {
            accounts.RemoveAt(index);
            SaveData();
        }

        public void EditAccount(string[] accountData, int index)
        {
            string[] account = accounts[index];
            account[0] = accountData[0];
            account[1] = accountData[1];
            account[2] = accountData[2];

            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            dgv.Rows.Clear();
            foreach (string[] account in accounts)
            {
                dgv.Rows.Add(account);
            }
            dgv.UpdateCellColor();
            SaveData();
        }
    }
}
