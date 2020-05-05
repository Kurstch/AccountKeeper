using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountKeeper
{
    class AccountDataGridView : DataGridView
    {
        private Color backColor = Color.FromArgb(38, 38, 38);
        private Color foreColor = Color.FromArgb(205, 205, 205);

        private DataWindow dw = null;
        private DataGridViewButtonColumn deleteButtonColumn = null;

        public AccountDataGridView(DataWindow tempdw)
        {
            dw = tempdw;
            InitializeDataGridView();
            InitializeColumns();
        }

        //Initializations
        private void InitializeDataGridView()
        {
            this.BackgroundColor = backColor;
            this.BorderStyle = BorderStyle.None;
            this.EnableHeadersVisualStyles = false;
            this.RowHeadersWidth = 10;
            this.RowHeadersDefaultCellStyle.Padding = new Padding(this.RowHeadersWidth);

            this.ColumnHeadersDefaultCellStyle.BackColor = backColor;
            this.ColumnHeadersDefaultCellStyle.ForeColor = foreColor;
            this.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 16);
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            this.RowHeadersDefaultCellStyle.BackColor = backColor;
            this.RowHeadersDefaultCellStyle.ForeColor = foreColor;
            this.RowHeadersDefaultCellStyle.Font = new Font("Calibri", 16);
            this.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            this.CellBorderStyle = DataGridViewCellBorderStyle.None;

            this.ReadOnly = true;

            UpdateCellColor();

            this.CellContentClick += new DataGridViewCellEventHandler(DataGridView_CellContentClick);
        }

        private void InitializeColumns()
        {
            this.Columns.Add("Websites", "Website");
            this.Columns.Add("E-mails", "E-mail");
            this.Columns.Add("Usernames", "Username");

            InitializeDeleteButtonColumn();
        }

        private void InitializeDeleteButtonColumn()
        {
            deleteButtonColumn = new DataGridViewButtonColumn();

            deleteButtonColumn.HeaderText = "";
            deleteButtonColumn.Text = "delete";
            deleteButtonColumn.Name = "deleteButtonColumn";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            deleteButtonColumn.FlatStyle = FlatStyle.Popup;

            this.Columns.Add(deleteButtonColumn);
        }

        public void UpdateCellColor()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                row.DefaultCellStyle.BackColor = backColor;
                row.DefaultCellStyle.ForeColor = foreColor;
                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(46, 46, 46);
                row.DefaultCellStyle.Font = new Font("Calibri", 12);
            }
        }

        //Event handlers
        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (this.Rows.Count > 1)
                {
                    DataGridViewRow row = this.Rows.SharedRow(e.RowIndex);
                    this.Rows.Remove(row);
                    dw.RemoveAccountFromList(e.RowIndex);
                }
            }
        }
    }
}
