using AccountKeeper.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace AccountKeeper
{
    class MainToolStrip : ToolStrip
    {
        private ToolStripDropDownButton fileMenu = null;
        private ToolStripDropDownButton accountsMenu = null;
        private ToolStripButton closeButton = null;
        private ToolStripButton maximizeButton = null;
        private ToolStripButton minimizeButton = null;
        private DataWindow dw = null;

        private bool dragging = false;
        private Point startPoint = Point.Empty;

        public MainToolStrip(DataWindow tempdw)
        {
            dw = tempdw;
            InitializeToolStrip();
            InitializeFileMenu();
            InitializeAccountsMenu();
            InitializeCloseButton();
            InitializeMaximizeButton();
            InitializeMinimizeButton();
        }

        //Initializations
        private void InitializeToolStrip()
        {
            this.GripStyle = ToolStripGripStyle.Hidden;
            this.RenderMode = ToolStripRenderMode.System;
            this.BackColor = Settings.Default.stripBackColor;
            this.ForeColor = Settings.Default.foreColor;

            this.MouseDown += new MouseEventHandler(MainMenuStrip_MouseDown);
            this.MouseMove += new MouseEventHandler(MainMenuStrip_MouseMove);
            this.MouseUp += new MouseEventHandler(MainMenuStrip_MouseUp);

        }

        private void InitializeFileMenu()
        {
            ToolStripMenuItem exitApp = null;

            fileMenu = new ToolStripDropDownButton();
            fileMenu.BackColor = Color.Transparent;
            fileMenu.ForeColor = Settings.Default.foreColor;
            fileMenu.ShowDropDownArrow = false;
            fileMenu.Text = "File";

            exitApp = new ToolStripMenuItem();
            exitApp.Text = "Exit";
            exitApp.MouseDown += new MouseEventHandler(ExitApp_MouseDown);

            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { exitApp }); 
            this.Items.Add(fileMenu);
        }

        private void InitializeAccountsMenu()
        {
            ToolStripMenuItem addAccount = null;

            accountsMenu = new ToolStripDropDownButton();
            accountsMenu.BackColor = Color.Transparent;
            accountsMenu.ForeColor = Settings.Default.foreColor;
            accountsMenu.ShowDropDownArrow = false;
            accountsMenu.Text = "Accounts";

            addAccount = new ToolStripMenuItem();
            addAccount.BackColor = Color.Transparent;
            addAccount.Text = "Add Account";
            addAccount.MouseDown += new MouseEventHandler(AddAccount_MouseDown);

            accountsMenu.DropDownItems.AddRange(new ToolStripMenuItem[] { addAccount});

            this.Items.Add(accountsMenu);
        }

        private void InitializeCloseButton()
        {
            closeButton = new ToolStripButton();

            closeButton.BackColor = Color.Transparent;
            closeButton.ForeColor = Settings.Default.foreColor;
            closeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            closeButton.Text = "🗙";

            closeButton.Alignment = ToolStripItemAlignment.Right;

            this.Items.Add(closeButton);
            closeButton.MouseDown += new MouseEventHandler(CloseButton_MouseDown);
        }

        private void InitializeMaximizeButton()
        {
            maximizeButton = new ToolStripButton();

            maximizeButton.BackColor = Color.Transparent;
            maximizeButton.ForeColor = Settings.Default.foreColor;
            maximizeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            maximizeButton.Text = "🗖";

            maximizeButton.Alignment = ToolStripItemAlignment.Right;

            this.Items.Add(maximizeButton);
            maximizeButton.MouseDown += new MouseEventHandler(MaximizeButton_MouseDown);
        }

        private void InitializeMinimizeButton()
        {
            minimizeButton = new ToolStripButton();

            minimizeButton.BackColor = Color.Transparent;
            minimizeButton.ForeColor = Settings.Default.foreColor;
            minimizeButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            minimizeButton.Text = "🗕";

            minimizeButton.Alignment = ToolStripItemAlignment.Right;

            this.Items.Add(minimizeButton);
            minimizeButton.MouseDown += new MouseEventHandler(MinimizeButton_MouseDown);
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

        private void CloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            dw.Dispose();
        }
        
        private void MaximizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (dw.WindowState == FormWindowState.Normal)
                dw.WindowState = FormWindowState.Maximized;
            else
                dw.WindowState = FormWindowState.Normal;
        }
        
        private void MinimizeButton_MouseDown(object sender, MouseEventArgs e)
        {
            dw.WindowState = FormWindowState.Minimized;
        }
    }
}
