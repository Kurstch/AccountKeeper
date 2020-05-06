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
    public partial class EditWindow : Form
    {
        private Color textBoxBackColor = Color.FromArgb(27, 27, 27);

        private Color backColor = Color.FromArgb(32, 32, 32);
        private Color foreColor = Color.FromArgb(205, 205, 205);

        private Label hLabel = null;
        private RichTextBox websiteTextBox = null;
        private RichTextBox emailTextBox = null;
        private RichTextBox usernameTextBox = null;
        private Button closeButton = null;
        private Button acceptButton = null;
        private Button deleteButton = null;
        private DataWindow dw = null;
        private AccountDataGridView dgv = null;
        private DataGridViewRow row = null;

        private bool dragging = false;
        private Point startPoint = Point.Empty;

        public EditWindow(string[] accountData, AccountDataGridView tempDgv, DataGridViewRow tempRow, DataWindow tempdw)
        {
            dw = tempdw;
            dgv = tempDgv;
            row = tempRow;

            InitializeComponent();
            InitializeWindow();
            InitializeHeaderLabel();
            InitializeWebsiteTextBox(accountData[0]);
            InitializeEmailTextBox(accountData[1]);
            InitializeUsernameTextBox(accountData[2]);
            InitializeAcceptButton();
            InitializeCloseButton();
            InitializeDeleteButton();
        }

        //Initializations
        private void InitializeWindow()
        {
            this.BackColor = backColor;
            this.Size = new Size(600, 400);
            this.FormBorderStyle = FormBorderStyle.None;

            this.MouseDown += new MouseEventHandler(EditWindow_MouseDown);
            this.MouseMove += new MouseEventHandler(EditWindow_MouseMove);
            this.MouseUp += new MouseEventHandler(EditWindow_MouseUp);
        }

        private void InitializeHeaderLabel()
        {
            hLabel = new Label();

            hLabel.BackColor = Color.Transparent;
            hLabel.ForeColor = Color.FromArgb(205, 205, 205);
            hLabel.Text = "Edit account";
            hLabel.Font = new Font("Calibri", 13);

            hLabel.Location = new Point(20, 30);
            hLabel.Size = new Size(200, 20);

            hLabel.BorderStyle = BorderStyle.None;

            this.Controls.Add(hLabel);
            this.ActiveControl = hLabel;
        }

        private void InitializeWebsiteTextBox(string website)
        {
            websiteTextBox = new RichTextBox();

            websiteTextBox.BackColor = textBoxBackColor;
            websiteTextBox.ForeColor = foreColor;
            websiteTextBox.Text = website;
            websiteTextBox.Font = new Font("Calibri", 14);

            websiteTextBox.Location = new Point(30, 90);
            websiteTextBox.Size = new Size(400, 30);

            websiteTextBox.BorderStyle = BorderStyle.None;
            websiteTextBox.Multiline = false;
            websiteTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(websiteTextBox);
        }

        private void InitializeEmailTextBox(string email)
        {
            emailTextBox = new RichTextBox();

            emailTextBox.BackColor = textBoxBackColor;
            emailTextBox.ForeColor = foreColor;
            emailTextBox.Text = email;
            emailTextBox.Font = new Font("Calibri", 14);

            emailTextBox.Location = new Point(30, 150);
            emailTextBox.Size = new Size(400, 30);

            emailTextBox.BorderStyle = BorderStyle.None;
            emailTextBox.Multiline = false;
            emailTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(emailTextBox);
        }

        private void InitializeUsernameTextBox(string username)
        {
            usernameTextBox = new RichTextBox();

            usernameTextBox.BackColor = textBoxBackColor;
            usernameTextBox.ForeColor = foreColor;
            usernameTextBox.Text = username;
            usernameTextBox.Font = new Font("Calibri", 14);

            usernameTextBox.Location = new Point(30, 210);
            usernameTextBox.Size = new Size(400, 30);

            usernameTextBox.BorderStyle = BorderStyle.None;
            usernameTextBox.Multiline = false;
            usernameTextBox.ScrollBars = RichTextBoxScrollBars.None;

            this.Controls.Add(usernameTextBox);
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

        private void InitializeDeleteButton()
        {
            deleteButton = new Button();

            deleteButton.BackColor = Color.FromArgb(38, 38, 38);
            deleteButton.ForeColor = Color.FromArgb(205, 205, 205);
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Font = new Font("Calibri", 12);
            deleteButton.Text = "delete";

            deleteButton.Size = new Size(100, 40);
            deleteButton.Location = new Point(10, ClientSize.Height - 50);

            this.Controls.Add(deleteButton);
            deleteButton.MouseDown += new MouseEventHandler(DeleteButton_MouseDown);
        }

        //Event handlers
        private void CloseButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void AcceptButton_MouseDown(object sender, MouseEventArgs e)
        {
            string[] accountData = { websiteTextBox.Text, emailTextBox.Text, usernameTextBox.Text };
            dw.EditAccount(accountData, row.Index);
            this.Close();
        }

        private void DeleteButton_MouseDown(object sender, MouseEventArgs e)
        {
            string question = "Are you sure you want to delete this?";
            ConfirmationBox cBox = new ConfirmationBox(question, this);
            cBox.Show();
        }

        private void EditWindow_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void EditWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void EditWindow_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        //Custom methods
        public void deleteAccount()
        {
            dgv.DeleteRow(row);
            this.Close();
        }
    }
}
