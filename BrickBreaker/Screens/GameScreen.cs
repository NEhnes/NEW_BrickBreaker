/*  Created by: Liam, Sean, Nathan, Aaron
 *  Project: Brick Breaker
 *  Date: May '25
 */ 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Security.Cryptography.X509Certificates;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values
        bool piercingBall = false;

        // Cached block images
        private Image redCoralCache;
        private Image blueCoralCache;
        private Image pinkCoralCache;
        private Image yellowCoralCache;
        private Image greyCoralCache;

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown;

        // Game values
        int lives;

        // game score
        public static int score;

        // Paddle and Ball objects
        Paddle paddle;
        Ball ball;

        // Ball speed multiplier
        public static double speedMultiplier = 1.2;

        // Ball Colour
        public static Color ballColor = Color.White;

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();
        string currentLevel = "Level1";

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(ballColor);

        // Powerup management and random object
        List<Powerup> powerups = new List<Powerup>();
        List<String> activePowerupTypes = new List<String>();
        Random rand = new Random();

        // Powerup duration & timers
        private static int powerupTime = 5000; //5s life by default
        private static System.Timers.Timer SpeedBoostTimer = new System.Timers.Timer(powerupTime);
        private static System.Timers.Timer BigPaddleTimer = new System.Timers.Timer(powerupTime);
        private static System.Timers.Timer SpeedReductionTimer = new System.Timers.Timer(powerupTime);
        private static System.Timers.Timer BulletTimer = new System.Timers.Timer(powerupTime);

        // Game Sounds
        SoundPlayer popPlayer = new SoundPlayer(Properties.Resources.popSound);
        System.Windows.Media.MediaPlayer gameSound = new System.Windows.Media.MediaPlayer();

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
            LoadBlocks();

        }


        public void OnStart()
        {
            InitPowerupTimers(); // add event handlers for timers

            // set cached images
            redCoralCache = Properties.Resources.resized_redCoral;
            blueCoralCache = Properties.Resources.resized_blueCoral;
            pinkCoralCache = Properties.Resources.resized_pinkCoral;
            yellowCoralCache = Properties.Resources.resized_yellowCoral;
            greyCoralCache = Properties.Resources.resized_greyCoral;

            //set life counter
            lives = 3;

            // set running score
            score = 0;

            // run opening UI and UX code
            LFischStart();

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // set up powerup lifespans 
            SpeedBoostTimer.Interval = powerupTime;
            BigPaddleTimer.Interval = powerupTime;
            SpeedReductionTimer.Interval = powerupTime;
            BulletTimer.Interval = powerupTime;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 12;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.White);

            // setup starting ball values
            int ballSize = 20;
            int ballX = paddle.x + paddleWidth / 2 - ballSize / 2;
            int ballY = paddle.y - ballSize - 2;

            // Creates a new ball
            int xSpeed = 6;
            int ySpeed = 6;
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize, speedMultiplier); // added speed parameter

            blocks.Clear();
            LoadBlocks();

            // reset any variable affected by powerups
            paddle.width = 80; // reset paddle size
            paddle.speed = 12; // reset paddle speed
            ball.speedMultiplier = 1.2; // reset ball speed

            // don't start the game engine loop until space pressed
            gameTimer.Enabled = false;

            gameSound.Open(new Uri(Application.StartupPath + "/Resources/background.mp3"));
            gameSound.MediaEnded += new EventHandler(gameSound_MediaEnded);
            gameSound.Play();
        }

        public void LFischStart()
        {

            Random randBG = new Random();

            int backImg = randBG.Next(1, 11);

            if (backImg <= 8)
            {
                BackgroundImage = Properties.Resources.BasicImage;
            }
            else if (backImg == 9)
            {
                BackgroundImage = Properties.Resources.SpecImage1;
            }
            else
            {
                BackgroundImage = Properties.Resources.SpecImage2;
            }
        }

        private void gameSound_MediaEnded(object sender, EventArgs e)
        {
            gameSound.Stop();
            gameSound.Play();
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Space:
                    gameTimer.Enabled = true; // start the game timer
                    break;
                case Keys.Escape:
                    OnEnd();
                    break;
                case Keys.Tab:
                    gameTimer.Enabled = !gameTimer.Enabled;  // pause game -- TODO: pause powerup timers
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Space:
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            lifeLabel.Text = $"{lives}";

            // Move the paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }

            // Move ball
            ball.Move();

            // Check for collision with top and side walls
            ball.WallCollision(this);

            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this))
            {
                lives--;

                // Moves the ball back to origin
                ball.x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                ball.y = (this.Height - paddle.height) - 85;

                gameTimer.Enabled = false;

                if (lives == 0)
                {
                    OnEnd();
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle);

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    blocks.Remove(b);
                    b.AddScore();

                    scoreLabel.Text = $"{score}";

                    popPlayer.Play();

                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }
                    // Block was hit — now spawn a powerup
                    if (rand.Next(0, 100) < 100) // 25% chance
                    {
                        string[] types = { "ExtraLife", "SpeedBoost", "SpeedReduction", "BigPaddle", "BulletBoost" };
                        string type = types[rand.Next(types.Length)];

                        Powerup newPowerup = new Powerup(b.x, b.y, type);
                        powerups.Add(newPowerup);
                    }


                    break;
                }
            }
            Rectangle ballRect = new Rectangle(ball.x, ball.y, ball.size, ball.size);
            for (int i = blocks.Count - 1; i >= 0; i--)
            {
                Block b = blocks[i];
                Rectangle blockRect = new Rectangle(b.x, b.y, b.width, b.height);

                if (ballRect.IntersectsWith(blockRect))
                {
                    blocks.RemoveAt(i);
                    if (blocks.Count <= 0)
                    {
                        if (currentLevel == "Level1")
                        {
                            currentLevel = "Level2";
                        }
                        if (currentLevel == "Level2")
                        {
                            currentLevel = "Level3";
                        }
                        if (currentLevel == "Level3") 
                        {
                            Form form = this.FindForm();
                            Screens.EndScreenL ps = new Screens.EndScreenL();

                            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

                            form.Controls.Add(ps);
                            form.Controls.Remove(this);
                        }
                    }

                    if (!piercingBall)
                    {
                        // Reverse ball direction if not piercing
                        ball.speedMultiplier *= -1;
                        break; // Exit loop so only one block is hit
                    }
                    // If piercing, no bounce and continue checking next block
                }
            }

            PowerupCollision();

            // Update ball color
            ballBrush = new SolidBrush(ballColor);

            foreach (Powerup p in powerups)
            {
                p.Move();
            }
            Rectangle paddleRect = new Rectangle(paddle.x, paddle.y, paddle.width, paddle.height);

            for (int i = powerups.Count - 1; i >= 0; i--)
            {
                Powerup p = powerups[i];
                Rectangle powerupRect = new Rectangle(p.x, p.y, p.size, p.size);

                // Check if paddle collects the powerup
                if (powerupRect.IntersectsWith(paddleRect))
                {
                    ApplyPowerup(p.type);      // Apply effect
                    powerups.RemoveAt(i);      // Remove powerup from screen
                }
                else if (p.y > paddle.y + paddle.height)
                {
                    powerups.RemoveAt(i);      // Remove if it falls below screen
                }
                else
                {
                    p.Move();                  // Let it fall down
                }
            }

            //PowerupCollision();

            //redraw the screen
            Refresh();
        }

        public void OnEnd()
        {
            gameSound.Close();

            // Goes to the game over screen
            if (Screens.OptionsScreenL.light == true)
            {
                Form form = this.FindForm();
                Screens.EndScreenL ps = new Screens.EndScreenL();

                ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

                form.Controls.Add(ps);
                form.Controls.Remove(this);
            }
            else
            {
                Form form = this.FindForm();
                Screens.EndScreenD psd = new Screens.EndScreenD();

                psd.Location = new Point((form.Width - psd.Width) / 2, (form.Height - psd.Height) / 2);

                form.Controls.Add(psd);
                form.Controls.Remove(this);
            }
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(paddleBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draw collected powerups as squares in top right
            int collectedX = 850;
            int collectedY = 500;
            foreach (String type in activePowerupTypes)
            {
                collectedX -= 30;
                Rectangle powerupRect = new Rectangle(collectedX, collectedY, 15, 15);
                Brush powerupBrush = new SolidBrush(Color.Gray);

                switch (type)
                {
                    case "ExtraLife":
                        powerupBrush = new SolidBrush(Color.Green);
                        break;
                    case "SpeedBoost":
                        powerupBrush = new SolidBrush(Color.Blue);
                        break;
                    case "BigPaddle":
                        powerupBrush = new SolidBrush(Color.Orange);
                        break;
                    case "SpeedReduction":
                        powerupBrush = new SolidBrush(Color.Red);
                        break;
                    case "BulletBoost":
                        powerupBrush = new SolidBrush(Color.Yellow);
                        break;
                }

                e.Graphics.FillRectangle(powerupBrush, powerupRect);
            }

            // Draws blocks
            foreach (Block b in blocks)
            {
                Rectangle recNumber = new Rectangle(b.x, b.y, b.width, b.height);

                if (b.colour == Color.Red)
                {
                    e.Graphics.DrawImage(redCoralCache, recNumber);
                }
                else if (b.colour == Color.Blue)
                {
                    e.Graphics.DrawImage(blueCoralCache, recNumber);
                }
                else if (b.colour == Color.Pink)
                {
                    e.Graphics.DrawImage(pinkCoralCache, recNumber);
                }
                else if (b.colour == Color.Yellow)
                {
                    e.Graphics.DrawImage(yellowCoralCache, recNumber);
                }
                else if (b.colour == Color.Gray)
                {
                    e.Graphics.DrawImage(greyCoralCache, recNumber);
                }
            }

            // Draws ball
            e.Graphics.FillRectangle(ballBrush, ball.x, ball.y, ball.size, ball.size);

            foreach (Powerup p in powerups)
            {
                p.Draw(e.Graphics);
            }
        }

        private void LoadBlocks()
        {
            
            string newX, newY, newColour;

            //Open the XML file and place it in reader 
            XmlReader reader = XmlReader.Create($"{currentLevel}.xml");
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    newX = reader.ReadString();
                    int blockX = Convert.ToInt32(newX);

                    reader.ReadToNextSibling("y");
                    newY = reader.ReadString();
                    int blockY = Convert.ToInt32(newY);

                    reader.ReadToNextSibling("colour");
                    newColour = reader.ReadString();
                    Color blockColour = Color.FromName(newColour); // potential error source later on

                    Block b = new Block(blockX, blockY, blockColour);
                    blocks.Add(b);
                }
            }
        }

        private void PowerupCollision()
        {
            // add intersecting powerups to delete list
            // MUST be done (removing elements while iterating gives enumeration error)
            List<Powerup> intersectingList = new List<Powerup>();

            foreach (Powerup p in powerups)
            {
                Rectangle powerupRec = new Rectangle(p.x, p.y, p.size, p.size);
                Rectangle paddleRec = new Rectangle(paddle.x, paddle.y, paddle.width, paddle.height);

                if (powerupRec.IntersectsWith(paddleRec)) intersectingList.Add(p);
            }

            // add to activePowerupTypes, remove from powerups list, start corresponding timer
            foreach (Powerup p in intersectingList)
            {
                if (!activePowerupTypes.Contains(p.type) || p.type == "ExtraLife") // only add if not already active
                {
                    ApplyPowerup(p.type); // apply the powerup effect
                    activePowerupTypes.Add(p.type); // add to active types list
                } else
                {
                    resetTimers(p.type); // reset timer if already active
                }
                    powerups.Remove(p);
            }
        }

        private void resetTimers(String timerId)   // WORKING ONNN
        {
            switch (timerId) {
                case "SpeedBoost":
                    SpeedBoostTimer.Stop();
                    SpeedBoostTimer.Start();
                    break;
                case "BigPaddle":
                    BigPaddleTimer.Stop();
                    BigPaddleTimer.Start();
                    break;
                case "SpeedReduction":
                    SpeedReductionTimer.Stop();
                    SpeedReductionTimer.Start();
                    break;
                case "BulletBoost":
                    BulletTimer.Stop();
                    BulletTimer.Start();
                    break;
            }
        }

        private void ApplyPowerup(string type)
        {
            switch (type)
            {
                case "ExtraLife":
                    lives++; // Assuming you have a 'lives' variable
                    break;
                case "SpeedBoost":
                    SpeedBoostTimer.Start();
                    paddle.speed = 20;
                    break;
                case "BigPaddle":
                    BigPaddleTimer.Start();
                    paddle.width += 40; // Temporarily increase paddle size
                    break;
                case "SpeedReduction":
                    SpeedReductionTimer.Start();
                    ball.speedMultiplier = 0.5;
                    break;
                case "BulletBoost":
                    BulletTimer.Start();
                    piercingBall = true;
                    break;
            }
        }

        private void EndPowerupTimer(String timerId)
        {

        }

        private void TimerElapsed(object sender, ElapsedEventArgs e, String timerId)  // Timer elapsed event listener
        {
            Console.WriteLine($"Timer {timerId} finished at {DateTime.Now}");


            switch (timerId)
            {
                case "SpeedBoost":

                    paddle.speed = 12; // Reset paddle speed
                    activePowerupTypes.Remove("SpeedBoost");

                    break;
                case "BigPaddle":

                    paddle.width -= 40; // Reset paddle size
                    activePowerupTypes.Remove("BigPaddle");

                    break;
                case "SpeedReduction":

                    ball.speedMultiplier = 1.2; // Reset ball speed
                    activePowerupTypes.Remove("SpeedReduction");

                    break;
                case "Bullet":

                    // end effect

                    break;
                default:
                    break;
            }
        }


        private void InitPowerupTimers() // assign event handlers for powerup timers.
        {
            SpeedBoostTimer.Elapsed += (sender, e) => TimerElapsed(sender, e, "SpeedBoost");
            BigPaddleTimer.Elapsed += (sender, e) => TimerElapsed(sender, e, "BigPaddle");
            SpeedReductionTimer.Elapsed += (sender, e) => TimerElapsed(sender, e, "SpeedReduction");
            BulletTimer.Elapsed += (sender, e) => TimerElapsed(sender, e, "Bullet");

            SpeedBoostTimer.AutoReset = false;
            BigPaddleTimer.AutoReset = false;
            SpeedReductionTimer.AutoReset = false;
            BulletTimer.AutoReset = false;
        }
    }
}