﻿using AccountKeeper.Properties;
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
    public class AccountDataGridView : DataGridView
    {
        private DataWindow dw = null;
        private DataGridViewButtonColumn EditButtonColumn = null;

        public AccountDataGridView(DataWindow tempdw)
        {
            dw = tempdw;
            InitializeDataGridView();
            InitializeColumns();
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
                    string[] accountData = { this.Rows[e.RowIndex].Cells[0].Value.ToString(),
                                 this.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                 this.Rows[e.RowIndex].Cells[2].Value.ToString()};
                    DataGridViewRow row = this.Rows.SharedRow(e.RowIndex);

                    new EditWindow(accountData, this, row, dw).Show();
                }
            }
        }

        //Custom Methods
        public void DeleteRow(DataGridViewRow row)
        {
            this.Rows.Remove(row);
            dw.RemoveAccount(row.Index + 1);
        }

        public void UpdateCellColor()
        {
            foreach (DataGridViewRow row in this.Rows)
            {
                row.DefaultCellStyle.BackColor = Settings.Default.formBackColor;
                row.DefaultCellStyle.ForeColor = Settings.Default.foreColor;
                row.DefaultCellStyle.SelectionBackColor = Settings.Default.selectionBackColor;
                row.DefaultCellStyle.Font = new Font("Calibri", 12);
            }
        }
    }
}
