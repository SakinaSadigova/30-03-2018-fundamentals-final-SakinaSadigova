using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace GameWindow
{
    public partial class StartPage : Form
    {
        public static int EndScore=10;
        public static string Level = "Test";
        Button Easy, Medium, Hard;
        SoundPlayer player = new SoundPlayer(Properties.Resources.Click);
        GameWindow obj;

        //Creating level buttons
        void ToCreateLevelButtons()
        {
            //Start Page Panel Level buttons

            Easy = new Button();
            Medium = new Button();
            Hard = new Button();
            Controls.Add(Easy);
            Controls.Add(Medium);
            Controls.Add(Hard);
            // Clicking sound 
            int top = 200;
            foreach (var item in Controls)
            {
                if (item is Button)
                {
                    Button btn = item as Button;
                    btn.Size = new Size(250, 70);
                    btn.Location = new Point(this.Width / 2 - btn.Width / 2, top);
                    btn.BackColor = Color.FromArgb(54, 67, 2);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = Color.Yellow;
                    btn.Font = new Font(new FontFamily("Arial"), 20, FontStyle.Bold);
                }
                top += 100;
            }
            Easy.Text = "Easy";
            Easy.Click += delegate {
                EndScore = 10;
                Level = "Easy";
                DoCommonThings();
                

            };
            Medium.Text = "Medium";
            Medium.Click += delegate
            {
                EndScore = 20;
                Level = "Medium";
                DoCommonThings();

            };
            Hard.Text = "Hard";
            Hard.Click += delegate
            {
                EndScore = 30;
                Level = "Hard";
                DoCommonThings();
            };
            void DoCommonThings()
            {
                obj = new GameWindow();
                ToPlayPlayer();
                this.Hide();
                obj.Show();

            }
            void ToPlayPlayer()
            {
                player.Play();
            }
        }
        public StartPage()
        {
            InitializeComponent();
            this.Size = new Size(626, 704);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImage = Properties.Resources.GameBackground;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Text = "Dragon";
           
        }

        private void StartPage_Load(object sender, EventArgs e)
        {
            ToCreateLevelButtons();
            this.FormClosing += delegate
            {
                Application.Exit();
            };
        }
        
    }
}
