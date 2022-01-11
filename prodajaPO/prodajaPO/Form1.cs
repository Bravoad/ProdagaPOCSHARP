using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prodajaPO
{
    public partial class Favto : Form
    {
        public Favto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                     Fpost  frm = new Fpost();
                    frm.ShowDialog();   
             }
            if (comboBox1.SelectedIndex == 1)
            {
                Fzak frm = new Fzak();
                frm.ShowDialog();
            }
            if (comboBox1.SelectedIndex == 2)
            {
                otch frm = new otch();
               frm.ShowDialog();
            }

        }
    }
}
