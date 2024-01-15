using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace web_scraping
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AutoSize = true;
        }
        public void textl(string chara)
        {
            int size = 0;
         
            if (chara.Length <= 10)
            {
                 size= chara.Length * 10;
                this.Size = new Size(size, size);
            }
            else
            {
               
                this.Size= new Size(580, 98);
            
            }
           
          
            label1.Text = chara;
            colorDialog1.ShowDialog();
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
