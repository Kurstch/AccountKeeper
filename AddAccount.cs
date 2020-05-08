using AccountKeeper.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AccountKeeper
{
    public partial class AddAccount : Form
    {
        private Label hLabel = null;
        private RichTextBox websiteTextBox = null;
        private RichTextBox emailTextBox = null;
        private RichTextBox usernameTextBox = null;
        private Button closeButton = null;
        private Button acceptButton = null;
        private DataWindow dataWindow = null;
        private AccountDataGridView dataGridView = null;

        private bool dragging = false;
        private Point startPoint = Point.Empty;

        public AddAccount(DataWindow tempDataWindow)
        {
            dataWindow = tempDataWindow;
            dataGridView = dataWindow.Controls[2] as AccountDataGridView;

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
            this.BackColor = Settings.Default.formBackColor;
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
            hLabel.ForeColor = Settings.Default.foreColor;
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

            websiteTextBox.BackColor = Settings.Default.textBoxBackColor;
            websiteTextBox.ForeColor = Settings.Default.grayForeColor;
            websiteTextBox.Text = "Website";
            websiteTextBox.Tag = "Website";
            websiteTextBox.Font = new Font("Calibri", 14);

            websiteTextBox.Location = new Point(30, 90);
            websiteTextBox.Size = new Size(400, 30);

            websiteTextBox.BorderStyle = BorderStyle.None;
            websiteTextBox.Multiline = false;
            websiteTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(websiteTextBox);
            websiteTextBox.Enter += new EventHandler(TextBox_Enter);
            websiteTextBox.Leave += new EventHandler(TextBox_Leave);
        }

        private void InitializeEmailTextBox()
        {
            emailTextBox = new RichTextBox();

            emailTextBox.BackColor = Settings.Default.textBoxBackColor;
            emailTextBox.ForeColor = Settings.Default.grayForeColor;
            emailTextBox.Text = "E-mail";
            emailTextBox.Tag = "E-mail";
            emailTextBox.Font = new Font("Calibri", 14);

            emailTextBox.Location = new Point(30, 150);
            emailTextBox.Size = new Size(400, 30);

            emailTextBox.BorderStyle = BorderStyle.None;
            emailTextBox.Multiline = false;
            emailTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(emailTextBox);
            emailTextBox.Enter += new EventHandler(TextBox_Enter);
            emailTextBox.Leave += new EventHandler(TextBox_Leave);
        }

        private void InitializeUsernameTextBox()
        {
            usernameTextBox = new RichTextBox();

            usernameTextBox.BackColor = Settings.Default.textBoxBackColor;
            usernameTextBox.ForeColor = Settings.Default.grayForeColor;
            usernameTextBox.Text = "Username";
            usernameTextBox.Tag = "Username";
            usernameTextBox.Font = new Font("Calibri", 14);

            usernameTextBox.Location = new Point(30, 210);
            usernameTextBox.Size = new Size(400, 30);

            usernameTextBox.BorderStyle = BorderStyle.None;
            usernameTextBox.Multiline = false;
            usernameTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(usernameTextBox);
            usernameTextBox.Enter += new EventHandler(TextBox_Enter);
            usernameTextBox.Leave += new EventHandler(TextBox_Leave);
        }

        private void InitializeAcceptButton()
        {
            acceptButton = new Button();

            acceptButton.BackColor = Color.Transparent;
            acceptButton.ForeColor = Settings.Default.foreColor;
            acceptButton.FlatAppearance.MouseOverBackColor = Settings.Default.selectionBackColor;
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

            closeButton.BackColor = Color.Transparent;
            closeButton.ForeColor = Settings.Default.foreColor;
            closeButton.FlatAppearance.MouseOverBackColor = Settings.Default.selectionBackColor;
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
            dataGridView.AddNewAccount(accountData);
            this.Close();
        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            RichTextBox field = (RichTextBox)sender;
            if (field.Text == (string)field.Tag)
            {
                field.Text = string.Empty;
                field.ForeColor = Settings.Default.foreColor;
            }
        }

        private void TextBox_Leave(object sender, EventArgs e)
        {
            RichTextBox field = (RichTextBox)sender;
            if (field.Text == string.Empty)
            {
                field.Text = (string)field.Tag;
                field.ForeColor = Settings.Default.grayForeColor;
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

        //Custom methods
        private bool CheckForFalseValue()
        {
            string[] s = {websiteTextBox.Text, "Website",
                               emailTextBox.Text, "E-mail",
                               usernameTextBox.Text, "Username"};

            if (s[0] == s[1] || s[2] == s[3] || s[4] == s[5])
            {
                if (s[0] == s[1])
                    websiteTextBox.ForeColor = Settings.Default.redForeColor;
                if (s[2] == s[3])
                    emailTextBox.ForeColor = Settings.Default.redForeColor;
                if (s[4] == s[5])
                    usernameTextBox.ForeColor = Settings.Default.redForeColor;
                return true;
            }
            return false;
        }
    }
}
