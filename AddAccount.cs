using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountKeeper
{
    public partial class AddAccount : Form
    {
        private Color textBoxBackColor = Color.FromArgb(30, 30, 30);
        private Color textBoxForeColorHint = Color.FromArgb(124, 124, 124);
        private Color textBoxForeColorWrite = Color.FromArgb(205, 205, 205);
        private Color textBoxForeColorFalseValue = Color.FromArgb(130, 36, 36);

        private Label hLabel = null;
        private RichTextBox websiteTextBox = null;
        private RichTextBox emailTextBox = null;
        private RichTextBox usernameTextBox = null;
        private Button closeButton = null;
        private Button acceptButton = null;
        private DataWindow dw = null;

        private bool dragging = false;
        private Point startPoint = Point.Empty;

        public AddAccount(DataWindow tempdw)
        {
            dw = tempdw;
            InitializeComponent();
            InitializeHeaderLabel();
            InitializeWindow();
            InitializeWebsiteTextBox();
            InitializeEmailTextBox();
            InitializeUsernameTextBox();
            InitializeAcceptButton();
            InitializeCloseButton();
        }

        //Initializtions
        private void InitializeWindow()
        {
            this.BackColor = Color.FromArgb(38, 38, 38);
            this.Size = new Size(600, 400);
            this.FormBorderStyle = FormBorderStyle.None;

            this.MouseDown += new MouseEventHandler(AddAccount_MouseDown);
            this.MouseMove += new MouseEventHandler(AddAccount_MouseMove);
            this.MouseUp += new MouseEventHandler(AddAccount_MouseUp);
            this.Paint += new PaintEventHandler(AddAccount_Paint);
        }

        private void InitializeHeaderLabel()
        {
            hLabel = new Label();

            hLabel.BackColor = Color.Transparent;
            hLabel.ForeColor = Color.FromArgb(205, 205, 205);
            hLabel.Text = "Add new account";
            hLabel.Font = new Font("Calibri", 13);

            hLabel.Location = new Point(20, 30);
            hLabel.Size = new Size(200, 20);

            hLabel.BorderStyle = BorderStyle.None;

            this.Controls.Add(hLabel);
            this.ActiveControl = hLabel;
        }

        private void InitializeWebsiteTextBox()
        {
            websiteTextBox = new RichTextBox();

            websiteTextBox.BackColor = textBoxBackColor;
            websiteTextBox.ForeColor = textBoxForeColorHint;
            websiteTextBox.Text = "Website";
            websiteTextBox.Font = new Font("Calibri", 14);

            websiteTextBox.Location = new Point(30, 90);
            websiteTextBox.Size = new Size(400, 30);

            websiteTextBox.BorderStyle = BorderStyle.None;
            websiteTextBox.Multiline = false;
            websiteTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(websiteTextBox);
            websiteTextBox.GotFocus += new EventHandler(WebsiteTextBox_GotFocus);
            websiteTextBox.LostFocus += new EventHandler(WebsiteTextBox_LostFocus);
        }

        private void InitializeEmailTextBox()
        {
            emailTextBox = new RichTextBox();

            emailTextBox.BackColor = textBoxBackColor;
            emailTextBox.ForeColor = textBoxForeColorHint;
            emailTextBox.Text = "E-mail";
            emailTextBox.Font = new Font("Calibri", 14);

            emailTextBox.Location = new Point(30, 150);
            emailTextBox.Size = new Size(400, 30);

            emailTextBox.BorderStyle = BorderStyle.None;
            emailTextBox.Multiline = false;
            emailTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(emailTextBox);
            emailTextBox.GotFocus += new EventHandler(EmailTextBox_GotFocus);
            emailTextBox.LostFocus += new EventHandler(EmailTextBox_LostFocus);
        }
        
        private void InitializeUsernameTextBox()
        {
            usernameTextBox = new RichTextBox();

            usernameTextBox.BackColor = textBoxBackColor;
            usernameTextBox.ForeColor = textBoxForeColorHint;
            usernameTextBox.Text = "Username";
            usernameTextBox.Font = new Font("Calibri", 14);

            usernameTextBox.Location = new Point(30, 210);
            usernameTextBox.Size = new Size(400, 30);

            usernameTextBox.BorderStyle = BorderStyle.None;
            usernameTextBox.Multiline = false;
            usernameTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(usernameTextBox);
            usernameTextBox.GotFocus += new EventHandler(UsernameTextBox_GotFocus);
            usernameTextBox.LostFocus += new EventHandler(UsernameTextBox_LostFocus);
        }

        private void InitializeAcceptButton()
        {
            acceptButton = new Button();

            acceptButton.BackColor = Color.FromArgb(38, 38, 38);
            acceptButton.ForeColor = Color.FromArgb(205, 205, 205);
            acceptButton.FlatAppearance.BorderSize = 0;
            acceptButton.FlatStyle = FlatStyle.Flat;
            acceptButton.Font = new Font("Calibri", 12);
            acceptButton.Text = "ok";

            acceptButton.Size = new Size(40, 40);
            acceptButton.Location = new Point(ClientSize.Width - 100, ClientSize.Height - 50);

            this.Controls.Add(acceptButton);
            acceptButton.MouseDown += new MouseEventHandler(AcceptButton_MouseDown);
        }

        private void InitializeCloseButton()
        {
            closeButton = new Button();

            closeButton.BackColor = Color.FromArgb(38, 38, 38);
            closeButton.ForeColor = Color.FromArgb(205, 205, 205);
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Calibri", 16);
            closeButton.Text = "x";

            closeButton.Size = new Size(40, 40);
            closeButton.Location = new Point(ClientSize.Width - 50, ClientSize.Height - 50);

            this.Controls.Add(closeButton);
            closeButton.MouseDown += new MouseEventHandler(CloseButton_MouseDown);
        }

        //Event handlers
        private void CloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void AcceptButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (CheckForFalseValue())
                return;

            string[] accountData = { websiteTextBox.Text, emailTextBox.Text, usernameTextBox.Text };
            dw.AddNewAccount(accountData);
            this.Close();
        }

        private bool CheckForFalseValue()
        {
            string[] s = {websiteTextBox.Text, "Website",
                               emailTextBox.Text, "E-mail",
                               usernameTextBox.Text, "Username"};

            if (s[0] == s[1] || s[2] == s[3] || s[4] == s[5])
            {
                if(s[0] == s[1])
                    websiteTextBox.ForeColor = textBoxForeColorFalseValue;
                if(s[2] == s[3])
                    emailTextBox.ForeColor = textBoxForeColorFalseValue;
                if(s[4] == s[5])
                    usernameTextBox.ForeColor = textBoxForeColorFalseValue;
                return true;
            }
            return false;
        }

        private void WebsiteTextBox_GotFocus(object sender, EventArgs e)
        {
            if (websiteTextBox.Text == "Website")
            {
                websiteTextBox.Text = "";
                websiteTextBox.ForeColor = textBoxForeColorWrite;
            }

        }

        private void WebsiteTextBox_LostFocus(object sender, EventArgs e)
        {
            if (websiteTextBox.Text == "")
            {
                websiteTextBox.ForeColor = textBoxForeColorHint;
                websiteTextBox.Text = "Website";
            }
        }
        
        private void EmailTextBox_GotFocus(object sender, EventArgs e)
        {
            if (emailTextBox.Text == "E-mail")
            {
                emailTextBox.Text = "";
                emailTextBox.ForeColor = textBoxForeColorWrite;
            }
        }

        private void EmailTextBox_LostFocus(object sender, EventArgs e)
        {
            if (emailTextBox.Text == "")
            {
                emailTextBox.ForeColor = textBoxForeColorHint;
                emailTextBox.Text = "E-mail";
            }
        }

        private void UsernameTextBox_GotFocus(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "Username")
            {
                usernameTextBox.Text = "";
                usernameTextBox.ForeColor = textBoxForeColorWrite;
            }
        }

        private void UsernameTextBox_LostFocus(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "")
            {
                usernameTextBox.ForeColor = textBoxForeColorHint;
                usernameTextBox.Text = "Username";
            }
        }

        private void AddAccount_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void AddAccount_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void AddAccount_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void AddAccount_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 4),
                                     this.DisplayRectangle);
        }
    }
}
