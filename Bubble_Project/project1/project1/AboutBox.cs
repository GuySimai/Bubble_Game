//Guy Simai
using System;
using System.Drawing;
using System.Windows.Forms;

namespace project1
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.KeyDown += Form1_KeyDown;

            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            richTextBox1.Enabled = false;
        }

        public delegate void printToRichTextBox();


        public void Lotteryfunction(printToRichTextBox method)
        {
            method();
        }

        public void S1()
        {
            richTextBox1.Text = "MY HOME WORK";
            richTextBox1.Font = new Font("Times New Roman", 14, FontStyle.Bold | FontStyle.Italic);
            richTextBox1.ForeColor = Color.Red;
        }

        public void S2()
        {
            richTextBox1.Text = "HW2!!!";
            richTextBox1.Font = new Font("Arial", 16, FontStyle.Bold);
            richTextBox1.ForeColor = Color.Blue; 
        }
        public void S3()
        {
            richTextBox1.Text = "HW2!";
            richTextBox1.Font = new Font("Courier New", 12, FontStyle.Italic);
            richTextBox1.ForeColor = Color.Green; 
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // ENTER
            {
                button1_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
