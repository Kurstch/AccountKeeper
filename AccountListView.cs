﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountKeeper
{
    class AccountListView : ListView
    {
        private Color backColor = Color.FromArgb(38, 38, 38);
        private Color foreColor = Color.FromArgb(205, 205, 205);

        public AccountListView()
        {
            InitializeListView();
            InitializeListViewItems();
            colorListViewHeader();
        }

        private void InitializeListView()
        {
            this.BackColor = backColor;
            this.ForeColor = foreColor;
            this.BorderStyle = BorderStyle.None;
            this.Font = new Font("Calibri", 14);
            this.View = View.Details;
            this.Scrollable = false;
        }

        private void InitializeListViewItems()
        {
            this.Columns.Add("", -2);
            this.Columns.Add("Website", 160, HorizontalAlignment.Left);
            this.Columns.Add("E-mail", 200,HorizontalAlignment.Left);
            this.Columns.Add("Username", -2, HorizontalAlignment.Left);
        }

        #region Set color

        public void colorListViewHeader()
        {
            this.OwnerDraw = true;
            this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler((sender, e) => headerDraw(sender, e));
            this.DrawItem += new DrawListViewItemEventHandler(bodyDraw);
        }

        private void headerDraw(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush backBrush = new SolidBrush(backColor))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            {
                e.Graphics.DrawString(e.Header.Text, e.Font, foreBrush, e.Bounds);
            }
        }

        private void bodyDraw(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        #endregion
    }
}
