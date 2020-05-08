using AccountKeeper.Properties;
using System.Drawing;
using System.Windows.Forms;


namespace AccountKeeper
{
    public partial class DataWindow : Form
    {
        private MainToolStrip toolStrip = null;
        private MainStatusStrip statusStrip = null;
        private AccountDataGridView dataGridView = null;


        public DataWindow()
        {
            InitializeComponent();
            InitializeForm();
            InitializeMenuStrip();
            initializeStatusStrip();
            InitializeDataGridView();
        }

        //Initializations
        private void InitializeForm()
        {
            this.BackColor = Settings.Default.formBackColor;
            this.Size = new Size(600, 700);
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void InitializeMenuStrip()
        {
            toolStrip = new MainToolStrip(this);
            this.Controls.Add(toolStrip);
        }

        private void initializeStatusStrip()
        {
            statusStrip = new MainStatusStrip();
            this.Controls.Add(statusStrip);
        }

        private void InitializeDataGridView()
        {
            dataGridView = new AccountDataGridView(this);

            dataGridView.Size = new Size(ClientRectangle.Width, ClientRectangle.Height - 40);
            dataGridView.Location = new Point(10, 30);

            this.Controls.Add(dataGridView);
        }
    }
}
