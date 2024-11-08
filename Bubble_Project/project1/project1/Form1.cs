//Guy Simai
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics; 


namespace project1
{

    public partial class Form1 : Form
    {
        // Variables
        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\\MyDB.mdf';Integrated Security=True";
        private List<Ball> balls = new List<Ball>();
        private Timer timer;
        private Stopwatch stopwatch = new Stopwatch();
        private bool playerNameEntered = false;
        private string playerName = string.Empty;

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // It is not possible to change the size of the form.

            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;

            // Create timer
            timer = new Timer();
            timer.Interval = 16;
            timer.Tick += Timer_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true; // To reduce the flickering
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Drawing all the balls
            foreach (var ball in balls)
            {
                ball.Draw(e.Graphics);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            foreach (var ball in balls)
            {
                ball.Update(this.ClientSize, toolStrip1.Height);  // Updating the position of the balls
            }

            this.Invalidate();  // Screen refresh
        }

        // +
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (!playerNameEntered)
            {
                Form2 f = new Form2();
                f.ShowDialog(this);

                if (!string.IsNullOrWhiteSpace(f.PlayerName))
                {
                    playerNameEntered = true;
                    playerName = f.PlayerName;

                    stopwatch.Start();  // Counting the seconds
                    timer.Start();      // Starting the animation timer
                }
            }
            else
            {
                AddNewBall();
            }
        }

        private void AddNewBall()
        {
            Random random = new Random();
            float size = (float)(random.NextDouble() * 30 + 10); // 10- 40
            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            float dx = (float)(random.NextDouble() * 4 - 2); // -2 - 2
            float dy = (float)(random.NextDouble() * 4 - 2); // -2 - 2

            balls.Add(new Ball(new PointF(this.ClientSize.Width / 2, this.ClientSize.Height / 2), size, color, dx, dy));
            this.Invalidate(); // Screen refresh
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)  // ENTER == +
            {
                toolStripButton1_Click(sender, e);
            }
        }

        // -
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            removeLastBall();
        }

        private void removeLastBall()
        {
            if (balls.Count > 0)
            {
                balls.RemoveAt(balls.Count - 1);  // Removes the last ball in the list
            }
            this.Invalidate();  // Screen refresh
        }

        // S
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (balls.Count > 0)
            {
                balls[balls.Count - 1].stopLastBall();  // Stop the last ball
            }
        }

        // A
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            
            Random rand = new Random();
            int number = rand.Next(1, 4);  // 1 - 3

            AboutBox.printToRichTextBox selectedMethod = null;

            switch (number)
            {
                case 1:
                    selectedMethod = ab.S1;
                    break;
                case 2:
                    selectedMethod = ab.S2;
                    break;
                case 3:
                    selectedMethod = ab.S3;
                    break;
            }

            ab.Lotteryfunction(selectedMethod);
            ab.ShowDialog(this);
        }

        //End Game 
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            balls.Clear();
            SavePlayerTimeToDB();
            playerNameEntered = false;
        }

        // DB
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog(this);
        }

        private void SavePlayerTimeToDB()
        {
            if(!playerNameEntered) {
                return;
            }
            stopwatch.Stop();  // Stop Stopwatch

            
            long elapsedSeconds = stopwatch.ElapsedMilliseconds / 1000;  // Convert milliseconds to seconds

            string queryString = "INSERT INTO dbo.TblProducts (Name, Length) VALUES ('" + playerName + "', " + elapsedSeconds + ")";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }

            stopwatch.Reset();  // // Reset the clock-Stopwatch
        }

        // E
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(this,
                    "ARE YOU SURE?",
                    "DO U REALLY WANT TO EXIT?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
            
            if (result == DialogResult.Yes)
            {
                SavePlayerTimeToDB();
                this.Close();
            }
        }
    }
}
