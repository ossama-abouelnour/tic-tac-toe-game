using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_Game.Properties;

namespace Tic_Tac_Toe_Game
{
    public partial class Form1 : Form
    {
        stGameStatus GameStatus;
        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        enum enPlayerTurn
        {
            Player1,
            Player2
        }

        byte PlayCount = 0;

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color White = Color.White;

            Pen Pen = new Pen(White);
            Pen.Width = 15;

            Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(Pen, 520, 100, 520, 450);
            e.Graphics.DrawLine(Pen, 680, 100, 680, 450);

            e.Graphics.DrawLine(Pen, 350, 210, 830, 210);
            e.Graphics.DrawLine(Pen, 350, 330, 830, 330);

        }

        public void EndGame()
        {
            lblPlayerTurn.Text = "GameOver";

            switch(GameStatus.Winner)
            {
                case enWinner.Player1:
                    lblWinner.Text = "Player 1";
                    break;

                case enWinner.Player2:
                    lblWinner.Text = "Player 2";
                    break;

                default:
                    lblWinner.Text = "Draw";
                    break;
            }

            if(GameStatus.Winner == enWinner.Player1 || GameStatus.Winner == enWinner.Player2)
            {
                MessageBox.Show(GameStatus.Winner.ToString() + " Won", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show(GameStatus.Winner.ToString(), "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }

        public bool CheckValues(Button btn1, Button btn2, Button btn3)
        {
            if (btn1.Tag.ToString() != "?" && btn1.Tag.ToString() == btn2.Tag.ToString() && btn1.Tag.ToString() == btn3.Tag.ToString())
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }

                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    GameStatus.GameOver = true;
                    EndGame();
                    return true;
                }

            }


            
            GameStatus.GameOver = true;
            return false;
            
        }

        public void CheckWinner()
        {
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return;

            if(CheckValues(button7, button8, button9))
                return;

            if (CheckValues(button1, button4, button7))
                return; 

            if(CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;

            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button3, button5, button7))
                return;
        }

        enPlayerTurn PlayerTurn;

        void ChangeImage(Button btn)
        {
            if(btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayerTurn.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayerTurn.Player2;
                        lblPlayerTurn.Text = "PLayer 2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        CheckWinner();
                        break;


                    case enPlayerTurn.Player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayerTurn.Player1;
                        lblPlayerTurn.Text = "PLayer 1";
                        GameStatus.PlayCount++;
                        btn.Tag = "O";
                        CheckWinner();
                        break;               
                }
            }

            if(GameStatus.PlayCount == 9)
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }

        }
        private void button_Click(object sender, EventArgs e)
        {
            ChangeImage((Button)sender);
        }

        void ResetButtons(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;
            btn.Enabled = true;
        }

        void RestartGame()
        {
            ResetButtons(button1);
            ResetButtons(button2);
            ResetButtons(button3);
            ResetButtons(button4);
            ResetButtons(button5);
            ResetButtons(button6);
            ResetButtons(button7);
            ResetButtons(button8);
            ResetButtons(button9);

            PlayerTurn = enPlayerTurn.Player1;
            lblPlayerTurn.Text = "Player 1";
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";
            GameStatus.PlayCount = 0;
        }

        private void btnRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        
    }
}
