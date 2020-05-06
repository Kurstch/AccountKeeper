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
    public partial class ConfirmationBox : Form
    {
        private Color backColor = Color.FromArgb(28, 28, 28);
        private Color foreColor = Color.FromArgb(205, 205, 205);

        
        private Label questionLabel = null;
        private Button yesButton = null;
        private Button noButton = null;
        private EditWindow edWindow = null;

        private bool dragging = false;
        private Point startPoint = Point.Empty;

        public ConfirmationBox(string question, EditWindow tempEdWindow)
        {
            edWindow = tempEdWindow;

            InitializeComponent();
            InitializeWindow();
            InitializeQuestionlabel(question);
            InitializeYesButton();
            InitializeNoButton();
        }

        //Initializations
        private void InitializeWindow()
        {
            this.BackColor = backColor;
            this.Size = new Size(300, 200);
            this.FormBorderStyle = FormBorderStyle.None;

            this.MouseDown += new MouseEventHandler(ConfirmationBox_MouseDown);
            this.MouseMove += new MouseEventHandler(ConfirmationBox_MouseMove);
            this.MouseUp += new MouseEventHandler(ConfirmationBox_MouseUp);
        }

        private void InitializeQuestionlabel(string question)
        {
            questionLabel = new Label();

            questionLabel.BackColor = backColor;
            questionLabel.ForeColor = foreColor;
            questionLabel.Text = question;
            questionLabel.Font = new Font("Calibri", 13);

            questionLabel.Size = new Size(260, 80);
            questionLabel.Location = new Point(20, 20);

            this.Controls.Add(questionLabel);
            this.ActiveControl = questionLabel;

            questionLabel.MouseDown += new MouseEventHandler(ConfirmationBox_MouseDown);
            questionLabel.MouseMove += new MouseEventHandler(ConfirmationBox_MouseMove);
            questionLabel.MouseUp += new MouseEventHandler(ConfirmationBox_MouseUp);
        }

        private void InitializeYesButton()
        {
            yesButton = new Button();

            yesButton.BackColor = backColor;
            yesButton.ForeColor = foreColor;
            yesButton.FlatAppearance.BorderSize = 0;
            yesButton.FlatStyle = FlatStyle.Flat;
            yesButton.Font = new Font("Calibri", 12);
            yesButton.Text = "Yes";

            yesButton.Size = new Size(60, 30);
            yesButton.Location = new Point(ClientSize.Width - 70, ClientSize.Height - 40);

            this.Controls.Add(yesButton);
            yesButton.MouseDown += new MouseEventHandler(YesButton_MouseDown);
        }

        private void InitializeNoButton()
        {
            noButton = new Button();

            noButton.BackColor = backColor;
            noButton.ForeColor = foreColor;
            noButton.FlatAppearance.BorderSize = 0;
            noButton.FlatStyle = FlatStyle.Flat;
            noButton.Font = new Font("Calibri", 12);
            noButton.Text = "No";

            noButton.Size = new Size(60, 30);
            noButton.Location = new Point(ClientSize.Width - 160, ClientSize.Height - 40);

            this.Controls.Add(noButton);
            noButton.MouseDown += new MouseEventHandler(NoButton_MouseDown);
        }

        //Event handlers
        private void YesButton_MouseDown(object sender, MouseEventArgs e)
        {
            edWindow.deleteAccount();
            this.Close();
        }

        private void NoButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ConfirmationBox_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void ConfirmationBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point p = PointToScreen(e.Location);
                this.Location = new Point(p.X - this.startPoint.X, p.Y - this.startPoint.Y);
            }
        }

        private void ConfirmationBox_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
