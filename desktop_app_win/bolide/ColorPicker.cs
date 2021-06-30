using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace bolide
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class ColorPicker : UserControl
    {
        public Color selectedColor
        {
            get
            {
                return button.BackColor;
            }
            set 
            {
                button.BackColor = value;
            }
        }
        public ColorPicker()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = button.BackColor;
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                button.BackColor = colorDialog.Color;
            }
        }
    }
}
