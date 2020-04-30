﻿using System;
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

namespace AccountKeeper
{
    class MainMenuStrip : MenuStrip
    {
        private ToolStripMenuItem fileMenu = null;
        private ToolStripMenuItem accountsMenu = null;
        private DataWindow dw = null;

        private bool dragging = false;
        private Point startPoint = Point.Empty;

        private Color backColor = Color.FromArgb(85, 85, 85);
        private Color foreColor = Color.FromArgb(205, 205, 205);

        public MainMenuStrip(DataWindow tempdw)
        {
            dw = tempdw;
            InitializeMenuStrip();
            InitializeFileMenu();
            InitializeAccountsMenu();
        }

        //Initializations
        private void InitializeMenuStrip()
        {
            this.Dock = DockStyle.Top;
            this.BackColor = Color.FromArgb(85, 85, 85);
            this.ForeColor = Color.FromArgb(205, 205, 205);

            this.MouseDown += new MouseEventHandler(MainMenuStrip_MouseDown);
            this.MouseMove += new MouseEventHandler(MainMenuStrip_MouseMove);
            this.MouseUp += new MouseEventHandler(MainMenuStrip_MouseUp);

        }

        private void InitializeFileMenu()
        {
            ToolStripMenuItem exitApp = null;

            fileMenu = new ToolStripMenuItem();
            fileMenu.BackColor = backColor;
            fileMenu.ForeColor = foreColor;
            fileMenu.Text = "File";

            exitApp = new ToolStripMenuItem();
            exitApp.BackColor = backColor;
            exitApp.ForeColor = foreColor;
            exitApp.Text = "Exit";
            exitApp.MouseDown += new MouseEventHandler(ExitApp_MouseDown);

            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { exitApp });

            this.Items.Add(fileMenu);
        }

        private void InitializeAccountsMenu()
        {
            ToolStripMenuItem addAccount = null;

            accountsMenu = new ToolStripMenuItem();
            accountsMenu.BackColor = backColor;
            accountsMenu.ForeColor = foreColor;
            accountsMenu.Text = "Accounts";

            addAccount = new ToolStripMenuItem();
            addAccount.BackColor = backColor;
            addAccount.ForeColor = foreColor;
            addAccount.Text = "Add Account";
            addAccount.MouseDown += new MouseEventHandler(AddAccount_MouseDown);

            accountsMenu.DropDownItems.AddRange(new ToolStripMenuItem[] { addAccount});

            this.Items.Add(accountsMenu);
        }

        //Event handlers
        private void AddAccount_MouseDown(object sender, MouseEventArgs e)
        {
            new AddAccount(dw).Show();
        }

        private void ExitApp_MouseDown(object sender, MouseEventArgs e)
        {
            dw.Close();
        }

        private void MainMenuStrip_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void MainMenuStrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                dw.Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void MainMenuStrip_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}