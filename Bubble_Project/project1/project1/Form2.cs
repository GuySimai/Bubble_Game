//Guy Simai
using System;
using System.Windows.Forms;

namespace project1
{
    public partial class Form2 : Form
    {
        public string PlayerName { get; set; } = string.Empty; 

        public Form2()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show(this, 
                    "The player's name must contain at least one character.", 
                    "Warning", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Warning);
            }
            else
            {
                PlayerName = textBox1.Text;
                this.Close();   
            }
            
        }
    }
}
