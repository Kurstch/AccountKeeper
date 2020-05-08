using AccountKeeper.Properties;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AccountKeeper
{
    public class AccountDataGridView : DataGridView
    {
        private DataWindow dataWindow = null;
        private MainStatusStrip statusStrip = null;
        private DataGridViewButtonColumn EditButtonColumn = null;
        private List<string[]> accounts = null;

        private string filePath = @"C:\Saves\AccountKeeper\save.xml";
        private string dirPath = @"C:\Saves\AccountKeeper";

        public AccountDataGridView(DataWindow tempdw)
        {
            dataWindow = tempdw;
            statusStrip = dataWindow.Controls[1] as MainStatusStrip;

            InitializeDataGridView();
            InitializeColumns();
            LoadData();
        }

        //Initializations
        private void InitializeDataGridView()
        {
            this.BackgroundColor = Settings.Default.formBackColor;
            this.BorderStyle = BorderStyle.None;
            this.EnableHeadersVisualStyles = false;
            this.RowHeadersWidth = 10;
            this.RowHeadersDefaultCellStyle.Padding = new Padding(this.RowHeadersWidth);

            this.ColumnHeadersDefaultCellStyle.BackColor = Settings.Default.formBackColor;
            this.ColumnHeadersDefaultCellStyle.ForeColor = Settings.Default.foreColor;
            this.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 16);
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            this.RowHeadersDefaultCellStyle.BackColor = Settings.Default.formBackColor;
            this.RowHeadersDefaultCellStyle.ForeColor = Settings.Default.foreColor;
            this.RowHeadersDefaultCellStyle.Font = new Font("Calibri", 16);
            this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            this.CellBorderStyle = DataGridViewCellBorderStyle.None;

            this.ReadOnly = true;

            this.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellContentClick);
        }

        private void InitializeColumns()
        {
            this.Columns.Add("Websites", "Website");
            this.Columns.Add("E-mails", "E-mail");
            this.Columns.Add("Usernames", "Username");

            InitializeEditButtonColumn();
        }

        private void InitializeEditButtonColumn()
        {
            EditButtonColumn = new DataGridViewButtonColumn();

            EditButtonColumn.HeaderText = "";
            EditButtonColumn.Text = "edit";
            EditButtonColumn.UseColumnTextForButtonValue = true;
            EditButtonColumn.FlatStyle = FlatStyle.Popup;

            this.Columns.Add(EditButtonColumn);
        }

        //Event handlers
        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (this.Rows.Count > 1)
                {
                    DataGridViewRow row = this.Rows.SharedRow(e.RowIndex);
                    new EditWindow(this, row).Show();
                }
            }
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
            statusStrip.UpdateItemCountLabel(this.RowCount - 1);
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
            statusStrip.UpdateItemCountLabel(this.RowCount - 1);
        }

        //Custom Methods
        public void UpdateCellStyle()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                row.DefaultCellStyle.BackColor = Settings.Default.formBackColor;
                row.DefaultCellStyle.ForeColor = Settings.Default.foreColor;
                row.DefaultCellStyle.SelectionBackColor = Settings.Default.selectionBackColor;
                row.DefaultCellStyle.Font = new Font("Calibri", 12);
                if (row.IsNewRow)
                    row.DefaultCellStyle.Padding = new Padding(20);
                else
                    row.DefaultCellStyle.Padding = new Padding(0);             
            }
        }

        public void AddNewAccount(string[] data)
        {
            accounts.Add(data);
            UpdateDataGridView();
        }

        public void RemoveAccount(DataGridViewRow row)
        {
            this.Rows.Remove(row);
            accounts.RemoveAt(row.Index + 1);
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
            this.Rows.Clear();
            foreach (string[] account in accounts)
            {
                this.Rows.Add(account);
            }
            UpdateCellStyle();
            SaveData();
        }
    }
}
