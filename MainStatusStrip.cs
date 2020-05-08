using AccountKeeper.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace AccountKeeper
{
    public class MainStatusStrip : StatusStrip
    {
        private ToolStripLabel itemCountLabel = null;

        public MainStatusStrip()
        {
            InitializeStatusStrip();
            InitializeItemCountLabel();
        }

        private void InitializeStatusStrip()
        {
            this.GripStyle = ToolStripGripStyle.Hidden;
            this.RenderMode = ToolStripRenderMode.System;

            this.BackColor = Settings.Default.stripBackColor;
            this.ForeColor = Settings.Default.foreColor;
        }

        private void InitializeItemCountLabel()
        {
            itemCountLabel = new ToolStripLabel();

            itemCountLabel.BackColor = Color.Transparent;
            itemCountLabel.ForeColor = Settings.Default.foreColor;
            itemCountLabel.Font = new Font("Calibri", 10);

            this.Items.Add(itemCountLabel);
        }

        public void UpdateItemCountLabel(int itemCount)
        {
            if (itemCount > 0)
                itemCountLabel.Text = itemCount.ToString() + " accounts";
            else
                itemCountLabel.Text = itemCount.ToString() + " account";
        }
    }
}
