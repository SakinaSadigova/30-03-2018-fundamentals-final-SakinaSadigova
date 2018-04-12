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
    public partial class GameWindow : Form
    {
        StartPage obj = new StartPage();
        SoundPlayer player;
        public int LeftPos, TopPos, Score = 0, newLeft = 0, newTop = 0, step = 30, counter = 30;
        public string Level = "Test";

        public Panel gamePanel, StartPagePanel,WinnerPanel,GameOverPanel;
        public Button Snake, playAgain,YesBtn,NoBtn;
        public PictureBox Feed;
        Label scoreLbl, timerLbl, GameOverLbl, level,winnerLbl,continueLbl;
        Timer timer;
        List<Button> SnakeBodyList = new List<Button>();
        bool check;
        void ToCheckEatingOwnself()
        {
            check = true;
            for (int i = 1; i < SnakeBodyList.Count; i++)
            {
                if (SnakeBodyList[0].Location == SnakeBodyList[i].Location)
                {
                    check = false;
                    break;
                }
            }
        }
        void CreateSnake()
        {
            Snake = new Button();
            gamePanel.Controls.Add(Snake);
            Snake.FlatStyle = FlatStyle.Flat;
            Snake.FlatAppearance.BorderSize = 1;
            Snake.FlatAppearance.BorderColor = Color.FromArgb(130, 195, 95);
            Snake.Size = new Size(30, 30);
            SnakeBodyList.Add(Snake);
        }
        void ToCreateSnakeWhileEating()
        {
            if (SnakeBodyList[0].Location.ToString() == Feed.Location.ToString())
            {
                player = new SoundPlayer(Properties.Resources.snakehit);
                player.Play();
                CreateSnake();
                int count = SnakeBodyList.Count - 1;
                SnakeBodyList[count].BackColor = Color.FromArgb(130, 195, 95);
                SnakeBodyList[count].Location = new Point(newLeft, newTop);
                DefinePositionOfFeed();
                Score++;
                scoreLbl.Text = "Your Score :" + Score.ToString();
                //timerLbl.Text = timer.ToString();
            }
            else
            {
                newLeft = SnakeBodyList[SnakeBodyList.Count - 1].Left;
                newTop = SnakeBodyList[SnakeBodyList.Count - 1].Top;
            }
        }
        void ToGetTheLocationOfPreviousButton()
        {
            for (int i = SnakeBodyList.Count - 1; i >= 1; i--)
            {
                SnakeBodyList[i].Location = SnakeBodyList[i - 1].Location;
            }
        }
        void DefinePositionOfSnake()
        {
            LeftPos = 0;
            TopPos = 0;
            SnakeBodyList[0].Location = new Point(0, 0);
        }
        void DefinePositionOfFeed()
        {
            Random random = new Random();
            int x = random.Next(1, 16) * 30, y = random.Next(1, 16) * 30;
            bool hasThePos = false;
            foreach (var item in SnakeBodyList)
            {
                if (item.Location == new Point(x, y))
                {
                    hasThePos = true;
                    break;
                }
            }
            if (hasThePos)
            {
                DefinePositionOfFeed();
            }
            else
            {
                Feed.Location = new Point(x, y);
            }
        }


        void ToDefineTimer()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += TimerTick;
        }
        void TimerTick(object sender, EventArgs e)
        {
            timerLbl.Text = "Time : " + (counter / 60).ToString() + ":" + ((counter % 60) >= 10 ? (counter % 60).ToString() : ("0" + (counter % 60).ToString()));
            if (counter >= 0 && Score == StartPage.EndScore)
            {
                timer.Stop();
                WinnerPanel.Show();
                toHideSnakeAndFeed();
            }
            else if (counter == 0 && Score < StartPage.EndScore)
            {
                timer.Stop();
                GameOverPanel.Show();
                toHideSnakeAndFeed();
            }
            else
            {
                counter--;
            }
        }
        void toHideSnakeAndFeed()
        {
            Feed.Hide();
            foreach (var item in SnakeBodyList)
            {
                item.Hide();
            }
        }
        void toShowSnakeAndFeed()
        {
            Feed.Show();
            foreach (var item in SnakeBodyList)
            {
                item.Show();
            }
        }
        void Click_Up(Object sender, KeyEventArgs e)
        {
            SnakeBodyList[0].Image = Properties.Resources.SnakeHeadUp;

            TopPos = gamePanel.Height - SnakeBodyList[0].Height;
            if (SnakeBodyList.Count >= 2)
            {
                if (SnakeBodyList[0].Top <= SnakeBodyList[1].Top || SnakeBodyList[1].Top == 0)
                {
                    ToCheckEatingOwnself();
                    if (check)
                    {
                        if (SnakeBodyList[0].Top <= 0)
                        {
                            ToGetTheLocationOfPreviousButton();
                            SnakeBodyList[0].Top = TopPos;
                        }
                        else
                        {
                            ToGetTheLocationOfPreviousButton();
                            SnakeBodyList[0].Top -= step;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Died");
                    }

                }
            }
            else
            {
                if (SnakeBodyList[0].Top <= 0)
                {
                    SnakeBodyList[0].Top = TopPos;
                }
                else
                {
                    SnakeBodyList[0].Top -= step;
                }
            }
            ToCreateSnakeWhileEating();
        }
        void Click_Down(Object sender, KeyEventArgs e)
        {
            SnakeBodyList[0].Image = Properties.Resources.SnakeHeadDown;

            TopPos = 0;
            if (SnakeBodyList.Count >= 2)
            {
                if (SnakeBodyList[0].Top >= SnakeBodyList[1].Top || SnakeBodyList[0].Top == 0)
                {
                    ToCheckEatingOwnself();
                    if (check)
                    {
                        if (SnakeBodyList[0].Top >= gamePanel.Height - SnakeBodyList[0].Height)
                        {
                            ToGetTheLocationOfPreviousButton();
                            SnakeBodyList[0].Top = TopPos;
                        }
                        else
                        {
                            ToGetTheLocationOfPreviousButton();
                            SnakeBodyList[0].Top += step;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Died");
                    }
                }
            }
            else
            {
                if (SnakeBodyList[0].Top >= gamePanel.Height - SnakeBodyList[0].Height)
                {
                    SnakeBodyList[0].Top = TopPos;
                }
                else
                {
                    SnakeBodyList[0].Top += step;
                }
            }
            ToCreateSnakeWhileEating();
        }
        void Click_Left(Object sender, KeyEventArgs e)
        {
            SnakeBodyList[0].Image = Properties.Resources.snakeHeadLeft;

            LeftPos = gamePanel.Width - SnakeBodyList[0].Width;
            if (SnakeBodyList.Count >= 2)
            {
                if (SnakeBodyList[0].Left <= SnakeBodyList[1].Left || SnakeBodyList[1].Left == 0)
                {
                    ToCheckEatingOwnself();
                    if (check)
                    {
                        if (SnakeBodyList[0].Left <= 0)
                        {
                            ToGetTheLocationOfPreviousButton();
                            SnakeBodyList[0].Left = LeftPos;
                        }
                        else
                        {
                            ToGetTheLocationOfPreviousButton();
                            SnakeBodyList[0].Left -= step;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Died");
                    }
                }
            }
            else
            {
                if (SnakeBodyList[0].Left <= 0)
                {
                    SnakeBodyList[0].Left = LeftPos;
                }
                else
                {
                    SnakeBodyList[0].Left -= step;
                }
            }
            ToCreateSnakeWhileEating();

        }
        void Click_Right(Object sender, KeyEventArgs e)
        {
            SnakeBodyList[0].Image = Properties.Resources.SnakeHeadRight;

            LeftPos = 0;
            if (SnakeBodyList.Count >= 2)
            {
                if (SnakeBodyList[0].Left >= SnakeBodyList[1].Left || SnakeBodyList[0].Left == 0)
                {
                    ToCheckEatingOwnself();
                    if (check)
                    {
                        if (SnakeBodyList[0].Left >= gamePanel.Width - SnakeBodyList[0].Width)
                        {
                            ToGetTheLocationOfPreviousButton();
                            SnakeBodyList[0].Left = LeftPos;
                        }
                        else
                        {
                            ToGetTheLocationOfPreviousButton();
                            SnakeBodyList[0].Left += step;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Died");
                    }
                }

            }
            else
            {
                if (SnakeBodyList[0].Left >= gamePanel.Width - SnakeBodyList[0].Width)
                {
                    SnakeBodyList[0].Left = LeftPos;
                }
                else
                {

                    SnakeBodyList[0].Left += step;
                }
            }
            ToCreateSnakeWhileEating();

        }

        void Clicked_Key(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Up)
            {
                Click_Up(this, e);
            }
            else if (e.KeyData == Keys.Down)
            {
                Click_Down(this, e);
            }
            else if (e.KeyData == Keys.Left)
            {
                Click_Left(this, e);
            }
            else if (e.KeyData == Keys.Right)
            {
                Click_Right(this, e);
            }
        }



        
        private void ToCreateWinnerAndLoserPanels()
        {
            //Winner and Loser views Creation When you are winning or losing the Game

            WinnerPanel = new Panel();
            gamePanel.Controls.Add(WinnerPanel);

            // Create Yes Button
            YesBtn = new Button();
            WinnerPanel.Controls.Add(YesBtn);
            YesBtn.Text = "YES";
            YesBtn.Click += delegate
            {
                WinnerPanel.Hide();
                toShowSnakeAndFeed();
                this.KeyPreview = true;
            };

            // Create No Button
            NoBtn = new Button();
            WinnerPanel.Controls.Add(NoBtn);
            NoBtn.Text = "No";
            NoBtn.Click += delegate
            {
                this.Hide();
                obj.Show();
            };

            //Winner label
            winnerLbl = new Label();
            WinnerPanel.Controls.Add(winnerLbl);
            winnerLbl.Text = "YOU ARE A WINNER";

            //Continue label
            continueLbl = new Label();
            WinnerPanel.Controls.Add(continueLbl);
            continueLbl.Text = "Continue?";

            GameOverPanel = new Panel();
            gamePanel.Controls.Add(GameOverPanel);

            //Game Over Label while showing you died
            GameOverLbl = new Label();
            GameOverPanel.Controls.Add(GameOverLbl);
            GameOverLbl.Text = "GAME OVER";


            //Play again Button
            playAgain = new Button();
            GameOverPanel.Controls.Add(playAgain);
            playAgain.Text = "Play Again";
            playAgain.Click += delegate
            {
                this.Hide();
                obj.Show();
            };

            foreach (var item in gamePanel.Controls)
            {
                if (item is Panel)
                {
                    var pnl = item as Panel;
                    pnl.BackColor = Color.FromArgb(54, 67, 2);
                    pnl.Size = new Size(500, 300);
                    pnl.Location = new Point(gamePanel.Width / 2 - pnl.Width / 2, gamePanel.Height / 2 - pnl.Height / 2);
                    pnl.Hide();
                }
                
            }
            foreach (var item in gamePanel.Controls)
            {
                if (item is Panel)
                {
                    var pnl = item as Panel;
                    int top = 50;
                    foreach (var lbl in pnl.Controls)
                    {
                        if (lbl is Label)
                        {
                            var newLbl = lbl as Label;
                            newLbl.Font = new Font(new FontFamily("Arial"), 35, FontStyle.Bold);
                            newLbl.ForeColor = Color.Red;
                            newLbl.AutoSize = true;
                            newLbl.Location = new Point(pnl.Width / 2 - newLbl.Width / 2, top);
                            top += 50;
                        }
                        
                    }
                    foreach (var btn in pnl.Controls)
                    {
                        if (btn is Button)
                        {
                            var newBtn = btn as Button;
                            newBtn.Size = new Size(250, 50);
                            newBtn.Location = new Point(pnl.Width/2-newBtn.Width/2, top);
                            newBtn.BackColor = Color.FromArgb(54, 67, 2);
                            newBtn.FlatStyle = FlatStyle.Flat;
                            newBtn.ForeColor = Color.White;
                            newBtn.FlatAppearance.BorderColor = Color.Yellow;
                            newBtn.Font = new Font(new FontFamily("Arial"), 25, FontStyle.Bold);
                            top += 50;
                        }
                    }
                }
            }
        }

        public GameWindow()
            {
                InitializeComponent();
                this.Size = new Size(626, 704);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.KeyPreview = true;
                this.KeyUp += Clicked_Key;
                this.BackColor = Color.FromArgb(126, 158, 0);
                this.Text = "Dragon";                    
        }
           private void GameWindow_Load(object sender, EventArgs e)
            {
                ToDefineTimer();
                //Score quantity label
                scoreLbl = new Label();
                scoreLbl.Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);
                scoreLbl.ForeColor = Color.White;
                scoreLbl.AutoSize = true;
                Controls.Add(scoreLbl);
                scoreLbl.Location = new Point(250, 20);
                scoreLbl.Text = "Your Score : " + Score.ToString();
                //The Time viewer
                timerLbl = new Label();
                timerLbl.Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);
                timerLbl.ForeColor = Color.White;
                timerLbl.AutoSize = true;
                timerLbl.Location = new Point(450, 20);
                Controls.Add(timerLbl);
                //Game's current level
                level = new Label();
                level.Font = new Font(new FontFamily("Arial"), 14, FontStyle.Bold);
                Controls.Add(level);
                level.ForeColor = Color.White;
                level.AutoSize = true;
                level.Location = new Point(50, 20);
                level.Text ="Game level : "+ StartPage.Level;
            gamePanel = new Panel();
            Controls.Add(gamePanel);
            gamePanel.Size = new Size(510, 510);
            gamePanel.Location = new Point(50, 55);
            gamePanel.BackColor = Color.FromArgb(54, 67, 2);
            //Create Feed Of Snake
            Feed = new PictureBox();
            gamePanel.Controls.Add(Feed);
            Feed.Size = new Size(33, 33);
            Feed.Image = Properties.Resources.apple;
            Feed.SizeMode = PictureBoxSizeMode.CenterImage;

            CreateSnake();
                SnakeBodyList[0].BackColor = Color.FromArgb(130, 195, 95);
                SnakeBodyList[0].Image = Properties.Resources.SnakeHeadRight;
                SnakeBodyList[0].ImageAlign = ContentAlignment.MiddleCenter;
                DefinePositionOfFeed();
                DefinePositionOfSnake();
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, Feed.Width - 3, Feed.Height - 3);
                Region rg = new Region(gp);
                Feed.Region = rg;            
                this.FormClosing += delegate
                {
                    Application.Exit();
                };
            ToCreateWinnerAndLoserPanels();


        }

    }

    } 
