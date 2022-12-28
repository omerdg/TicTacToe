using System.Media;
using System.Windows.Forms.VisualStyles;
using System.Timers;

namespace TicTacToeGame
{
    public partial class Form1 : Form
    {

        public bool pOneTurn = true;

        public Form1()
        {
            InitializeComponent();
            CenterToScreen();

        }

        Button[,] playButtons = new Button[3, 3];
        int pOneScore = 0;
        int pTwoScore = 0;
        System.Timers.Timer wonTextVisibityTime = new System.Timers.Timer(2000);


        public void Form1_Load(object sender, EventArgs e)
        {

            int leftPosition = 300;
            int topPosition = 80;


            for (int i = 0; i <= playButtons.GetUpperBound(0); i++)
            {
                topPosition = 80 + i * 120;

                for (int j = 0; j <= playButtons.GetUpperBound(1); j++)
                {
                    leftPosition = 300 + j * 120;
                    playButtons[i, j] = new Button();
                    playButtons[i, j].Width = 120;
                    playButtons[i, j].Height = 120;
                    playButtons[i, j].Left = leftPosition;
                    playButtons[i, j].Top = topPosition;
                    playButtons[i, j].Font = new Font("Calibri", 64);

                    this.Controls.Add(playButtons[i, j]);


                }
            }

            foreach (Button button in playButtons)
            {
                button.Click += new EventHandler(PlayerClicked);

            }
        }


        public void PlayerClicked(object sender, EventArgs e)
        {
            Button anyButton = (Button)sender;

            if (pOneTurn == true)
            {
                anyButton.Text = "X";
                pOneTurn = false;
                anyButton.Enabled = false;
                textTurn.Text = @"Turn
O";

            }
            else if (pOneTurn == false)
            {
                anyButton.Text = "O";
                pOneTurn = true;
                anyButton.Enabled = false;
                textTurn.Text = @"Turn
X";
            }

            Draw(playButtons);
            SetScore();
            IsFinished().BringToFront();
        }

        public void PlayAgainClicked(object sender, EventArgs e)
        {
            Button anyButton = (Button)sender;

            this.Controls.Remove(anyButton);

            pOneScore = 0;
            pTwoScore = 0;
            SetScore();
            textBox2.Text = $@"Player One 
X 
{pOneScore}";
            textBox3.Text = $@"Player Two 
O 
{pTwoScore}";
        }


        public bool HaveAWinner(Button[,] buttonArray, string winnerLetter)
        {
            bool winner = false;
            int horizontalCounter = 0;
            int verticalCounter = 0;
            int posCrossCounter = 0;
            int negCrossCounter = 0;

            for (int i = 0; i <= buttonArray.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= buttonArray.GetUpperBound(1); j++)
                {
                    if (buttonArray[i, j].Text == winnerLetter)
                    {
                        horizontalCounter += 1;
                    }

                    if (buttonArray[j, i].Text == winnerLetter)
                    {
                        verticalCounter += 1;
                    }

                    if (buttonArray[j, j].Text == winnerLetter)
                    {
                        posCrossCounter += 1;
                    }

                    if (buttonArray[j, (2 - j)].Text == winnerLetter)
                    {
                        negCrossCounter += 1;
                    }

                }
                if ((horizontalCounter == 3 || verticalCounter == 3) || ((posCrossCounter == 3) || (negCrossCounter == 3)))
                {
                    winner = true;
                    break;
                }
                else
                {
                    horizontalCounter = 0;
                    verticalCounter = 0;
                    posCrossCounter = 0;
                    negCrossCounter = 0;
                }

            }

            return winner;
        }

        public void SetScore()
        {
            if (HaveAWinner(playButtons, "X") == true)
            {
                pOneScore += 1;
                textBox2.Text = $@"Player One 
X 
{pOneScore}";
                foreach (Button button in playButtons)
                {
                    button.Enabled = true;
                    button.Text = null;
                }
            }
            else if (HaveAWinner(playButtons, "O") == true)
            {
                pTwoScore += 1;
                textBox3.Text = $@"Player Two 
O 
{pTwoScore}";
                foreach (Button button in playButtons)
                {
                    button.Enabled = true;
                    button.Text = null;
                }
            }


        }

        public TextBox IsFinished()
        {
            TextBox youWonText = new TextBox();

            if ((pOneScore == 5) || (pTwoScore == 5))
            {
                foreach (Button button in playButtons)
                {
                    button.Enabled = true;
                    button.Text = null;
                }

                switch (pOneScore)
                {
                    case (5):
                        youWonText.Text = "Player One Wins!";
                        youWonText.BackColor = Color.RosyBrown;
                        youWonText.ForeColor = Color.Moccasin;
                        break;
                    default:
                        youWonText.Text = "Player Two Wins!";
                        break;
                }

                youWonText.Width = 400;
                youWonText.Height = 300;
                youWonText.Font = new Font("Calibri", 40);
                youWonText.BringToFront();
                this.Controls.Add(youWonText);

                NewGame();
                this.Controls.Remove(youWonText);

            }


            return youWonText;
        }

        private void NewGame()
        {
            Button newGameButton = new Button();
            newGameButton.Text = "Play Again";
            newGameButton.Font = new Font("Calibri", 32);
            newGameButton.BackColor = Color.RosyBrown;
            newGameButton.ForeColor = Color.Moccasin;
            newGameButton.Bounds = new Rectangle(300, 80, 360, 100);
            this.Controls.Add(newGameButton);
            newGameButton.BringToFront();
            newGameButton.Click += new EventHandler(PlayAgainClicked);
        }

        private Button[,] Draw(Button[,] buttonArray)
        {
            int countClickeds = 0;

            foreach (Button button in buttonArray)
            {
                if ((button.Text == "") == true)
                {
                    countClickeds += 1;
                }
            }

            if (countClickeds == 0)
            {
                foreach (Button aButton in buttonArray)
                {
                    aButton.Text = null;
                    aButton.Enabled = true;
                }
            }

            return buttonArray;
        }
    }
}