using AccountKeeper.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace AccountKeeper
{
    public partial class ConfirmationBox : Form
    {        
        private Label questionLabel = null;
        private Button yesButton = null;
        private Button noButton = null;
        private EditWindow editWindow = null;

        private bool dragging = false;
        private Point startPoint = Point.Empty;

        public ConfirmationBox(string question, EditWindow tempEditWindow)
        {
            editWindow = tempEditWindow;

            InitializeComponent();
            InitializeForm();
            InitializeQuestionlabel(question);
            InitializeYesButton();
            InitializeNoButton();
        }

        //Initializations
        private void InitializeForm()
        {
            this.BackColor = Settings.Default.formBackColor;
            this.Size = new Size(340, 200);
            this.FormBorderStyle = FormBorderStyle.None;

            this.MouseDown += new MouseEventHandler(ConfirmationBox_MouseDown);
            this.MouseMove += new MouseEventHandler(ConfirmationBox_MouseMove);
            this.MouseUp += new MouseEventHandler(ConfirmationBox_MouseUp);
            this.Paint += new PaintEventHandler(ConfirmationBox_Paint);
        }

        private void InitializeQuestionlabel(string question)
        {
            questionLabel = new Label();

            questionLabel.BackColor = Color.Transparent;
            questionLabel.ForeColor = Settings.Default.foreColor;
            questionLabel.Text = question;
            questionLabel.Font = new Font("Calibri", 13);

            questionLabel.Size = new Size(this.Width - 40, 80);
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

            yesButton.BackColor = Color.Transparent;
            yesButton.ForeColor = Settings.Default.foreColor;
            yesButton.FlatAppearance.MouseOverBackColor = Settings.Default.selectionBackColor;
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

            noButton.BackColor = Color.Transparent;
            noButton.ForeColor = Settings.Default.foreColor;
            noButton.FlatAppearance.MouseOverBackColor = Settings.Default.selectionBackColor;
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
            editWindow.deleteAccount();
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

        private void ConfirmationBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 4),
                                     this.DisplayRectangle);
        }
    }
}
