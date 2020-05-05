using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountKeeper
{
    class MainStatusStrip : StatusStrip
    {
        private ToolStripLabel itemCountLabel = null;

        private Color backColor = Color.FromArgb(85, 85, 85);
        private Color foreColor = Color.FromArgb(205, 205, 205);

        public MainStatusStrip()
        {
            InitializeStatusStrip();
            InitializeItemCountLabel();
        }

        private void InitializeStatusStrip()
        {
            this.GripStyle = ToolStripGripStyle.Hidden;
            this.RenderMode = ToolStripRenderMode.System;

            this.BackColor = backColor;
            this.ForeColor = foreColor;
        }

        private void InitializeItemCountLabel()
        {
            itemCountLabel = new ToolStripLabel();

            itemCountLabel.BackColor = backColor;
            itemCountLabel.ForeColor = foreColor;
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
