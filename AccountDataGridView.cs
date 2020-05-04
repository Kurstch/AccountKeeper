using System;
using System.Collections.Generic;
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

        public AccountDataGridView()
        {
            InitializeDataGridView();
            InitializeColumns();
        }

        private void InitializeDataGridView()
        {
            this.BackgroundColor = backColor;
            this.BorderStyle = BorderStyle.None;
            this.EnableHeadersVisualStyles = false;

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

            UpdateCellColor();
        }

        private void InitializeColumns()
        {
            this.Columns.Add("Websites", "Website");
            this.Columns.Add("E-mails", "E-mail");
            this.Columns.Add("Usernames", "Username");           
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
    }
}
