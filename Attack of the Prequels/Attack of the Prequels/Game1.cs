using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO.IsolatedStorage;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;



namespace Attack_of_the_Prequels
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        static public SoundEffect lazerEffect,enemyLazerEffect,explosionTempSound,explosionSound;
        private SpriteFont font;

        //two player stuff
        Viewport mainViewport;
        Viewport leftViewport;
        Viewport rightViewport;
        bool multiPlayer = false;

        private int score = 0;
        public int wave;
        //public static bool inverted;
        GraphicsDeviceManager graphics;
        private bool leftLastPressed;
        private bool rightLastPressed;
        private bool left2;
        private bool right2;
        private bool left3;
        private bool right3;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        //Matrix stuff for camera and projection
       public  static Matrix view= Matrix.CreateLookAt(new Vector3(0, 0, 175), new Vector3(0, 0, 0), Vector3.UnitY);
       public static Matrix projection=Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 4f, 5550000f);
       public static Matrix projection2 = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 400f / 480f, 4f, 5550000f);

       public static Matrix view2 = Matrix.CreateLookAt(new Vector3(0, 0, 175), new Vector3(0, 0, 0), Vector3.UnitY);
       //public static Matrix projection2 = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 4f, 5550000f);
       //private Camera camera;
       private int enemyNumber = 0;
       private int buzzerCount = 1000;
       private int gameMode = 0;
       private Player2 player4;
        /*Game Mode: 
            
            0: start menu
            1: singleplayerGamePlay
            2: pause
            3/4: lose singleplayer
            5: singlePlayerFirstMenu
            6: twoPlayerFirstMenu
            7: one-on-one battle
            8: playerOneLose
            9: playerTwoLose
         
         
         */
       private float screenWidth;
       private float screenHeight;
       List<Vector3> tenWaves = new List<Vector3>();

        //some of my objects
        private Player player;
        
        private Skysphere skySphere;
        private EnemyBase enemyBase,executor;
        private Camera camera;

        private List<EnemyShip> enemyShips;
        public static List<Lazer> enemyLazers;
        //pause screens and stuff images made into  textures
        private Texture2D beginningTexture, pauseTexture, loseTexture, winTexture,crosshairs,crosshairs2;
        public static Vector3 deathStarPosition;
        protected int intersection;
        int initialCount;
        public int highScore;

        private Camera camera2;
        public bool flag = false;
        public bool flag2 = false;
        List<Missile> missiles = new List<Missile>();
        //int finalCount;

        static public Random rand = new Random();

        protected Song song,intro;
        //protected SpriteFont font;

   

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //aspectRatio = (float)GraphicsDeviceManager.DefaultBackBufferWidth /
       // (2 * GraphicsDeviceManager.DefaultBackBufferHeight);

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
             //Console.Write(highScore);
           
            
            
            
            initialCount = 0;
           // finalCount = 0;
            //inverted = false;
            wave = 0;
            leftLastPressed = false;
            rightLastPressed = false;
            left2 = false;
            left3 = false;
            right2 = false;
            right3 = false;
            
            explosionTempSound = Content.Load<SoundEffect>("Explo5a (mp3cut.net)");
            explosionSound = Content.Load<SoundEffect>("Explo5a");
            lazerEffect = Content.Load<SoundEffect>("pistol-1 (mp3cut.net)");
            enemyLazerEffect = Content.Load<SoundEffect>("turret-1 (mp3cut.net)");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            font = Content.Load<SpriteFont>("SpriteFont1");
            deathStarPosition=new Vector3(200090f,1850900f,-200000f);
            player = new Player(Content,new Vector3(100000,1000000,10000),PlayerIndex.One);
            intersection = 10000;
            device.RasterizerState = RasterizerState.CullCounterClockwise; 
            enemyBase = new EnemyBase(Content,"tradaFedaerationCruiser",new Vector3(0,0,-1500f));
            executor = new EnemyBase(Content, "strikeBack8", deathStarPosition);
            
            

            camera = new Camera();
            camera2 = new Camera();

            enemyShips = new List<EnemyShip>();

            enemyLazers = new List<Lazer>();

            skySphere = new Skysphere(Content);

            tenWaves.Add(new Vector3(2, 0, 0));
            tenWaves.Add(new Vector3(5, 0, 0));
            tenWaves.Add(new Vector3(2, 1, 0));
            tenWaves.Add(new Vector3(5, 2, 0));
            tenWaves.Add(new Vector3(0, 0, 3));
            tenWaves.Add(new Vector3(3, 2, 2));
            tenWaves.Add(new Vector3(0, 5, 0));
            tenWaves.Add(new Vector3(15, 0, 0));
            tenWaves.Add(new Vector3(0, 0, 10));
            tenWaves.Add(new Vector3(15, 2, 5));
            tenWaves.Add(new Vector3(7, 3, 7));
            tenWaves.Add(new Vector3(50, 0, 0));


            beginningTexture = Content.Load<Texture2D>("background");
            pauseTexture = Content.Load<Texture2D>("background2");
            loseTexture = Content.Load<Texture2D>("endScreen.jpg");
            winTexture = Content.Load<Texture2D>("endScreenWin");
            crosshairs = Content.Load<Texture2D>("crosshairs");
            crosshairs2 = Content.Load<Texture2D>("crosshairs2");
            //crosshairs

            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;

            enemyShips.Add(new Gunship(Content, player, rand));
            enemyShips.Add(new TaiFighter(Content, player, rand));
            enemyShips.Add(new Interceptor(Content, player, rand));
            enemyShips.Clear();

            for (int i = 0; i < tenWaves[wave].X; i++)
            {
                enemyShips.Add(new TaiFighter(Content, player,rand));
                enemyNumber++;
            }
            for (int i = 0; i < tenWaves[wave].Y; i++)
            {
                enemyShips.Add(new Gunship(Content, player, rand));
                enemyNumber++;
            }
            for (int i = 0; i < tenWaves[wave].Z; i++)
            {
                enemyShips.Add(new Interceptor(Content, player, rand));
                enemyNumber++;
            }
            song = Content.Load<Song>("Star Wars Epic Music Mix (Medley)");
            intro = Content.Load<Song>("John Williams2");
            MediaPlayer.Play(intro);
            MediaPlayer.IsRepeating = true;
            //camera = new Camera();
            //world = Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);
            //angle = 0;
            
            //two player stuff
            mainViewport = GraphicsDevice.Viewport;
            leftViewport = mainViewport;
            rightViewport = mainViewport;
            leftViewport.Height = mainViewport.Height / 2;
            rightViewport.Height = mainViewport.Height / 2;
            rightViewport.Y = leftViewport.Height;

            // TODO: use this.Content to load your game content here


            player4 = new Player2(Content, player, PlayerIndex.Two);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            
            
            if (buzzerCount < 100)
            {

                GamePad.SetVibration(PlayerIndex.One, 1f, 1f);
                buzzerCount++;
            }
            else
            {

                GamePad.SetVibration(PlayerIndex.One, 0f, 0f); 
            }
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (gameMode == 5)
            {
#if XBOX360
                if (GamePad.GetState(PlayerIndex.One).DPad.Right==ButtonState.Pressed){
                      if(player.inverted==false&&rightLastPressed==false){
                    player.inverted=true;
                    }
                    rightLastPressed = true;
                    
                }
                else {
                    rightLastPressed=false;
                }
                 if (GamePad.GetState(PlayerIndex.One).DPad.Left==ButtonState.Pressed){
                     if (player.inverted == true&&leftLastPressed==false)
                    {
                        player.inverted = false;
                    }
                    leftLastPressed = true;
                }
                else
                {
                    leftLastPressed = false;
                }
                
                
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed){
                    gameMode=1;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song);
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
                    

#else
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.Right))
                {

                    if (player.inverted == false && rightLastPressed == false)
                    {
                        player.inverted = true;
                    }
                    rightLastPressed = true;

                }
                else
                {
                    rightLastPressed = false;
                }

                if (state.IsKeyDown(Keys.Left))
                {

                    if (player.inverted == true && leftLastPressed == false)
                    {
                        player.inverted = false;
                    }
                    leftLastPressed = true;
                }
                else
                {
                    leftLastPressed = false;
                }

                if (state.IsKeyDown(Keys.Enter))
                {
                    gameMode = 1;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song);
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
#endif
            }
            else if (gameMode == 6)
            {
                #if XBOX360
                if (GamePad.GetState(PlayerIndex.One).DPad.Right==ButtonState.Pressed){
                      if(player.inverted==false&&rightLastPressed==false){
                    player.inverted=true;
                    }
                    rightLastPressed = true;
                    
                }
                else {
                    rightLastPressed=false;
                }
                 if (GamePad.GetState(PlayerIndex.One).DPad.Left==ButtonState.Pressed){
                     if (player.inverted == true&&leftLastPressed==false)
                    {
                        player.inverted = false;
                    }
                    leftLastPressed = true;
                }
                else
                {
                    leftLastPressed = false;
                }
                
                
                if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed){
                    gameMode=7;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song);
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
                   
 
                //Player2

                if (GamePad.GetState(PlayerIndex.Two).DPad.Right==ButtonState.Pressed){
                      if(player4.inverted==false&&right2==false){
                    player4.inverted=true;
                    }
                    right2 = true;
                    
                }
                else {
                    right2=false;
                }
                 if (GamePad.GetState(PlayerIndex.Two).DPad.Left==ButtonState.Pressed){
                     if (player4.inverted == true&&left2==false)
                    {
                        player4.inverted = false;
                    }
                    left2 = true;
                }
                else
                {
                    left2 = false;
                }
                
                
                

#endif         
                
                
            }
            else if (gameMode == 7)
            {
                //multiPlayer gameplay 
                //Console.WriteLine("a");

                player.update();
              //  player4.scale = 100;
                //player2.update();
                player.scale = 100f;
                player4.update();
                enemyBase.update();
                executor.update();

                if (Vector3.Distance(player.position, player4.position)<intersection*4)
                {

                    gameMode = 10;
                    explosionSound.Play();
                }

                List<Lazer> lazers = player.lazers;
                for (int i=lazers.Count-1;i>=0;i--)
                {
                    lazers[i].update();
                    if (lazers[i].scale < 101)
                    {

                        lazers[i].scale *= 20;
                        lazers[i].velocity *= 5;
                    }
                    if (Vector3.Distance(lazers[i].lazerPosition, player4.position) < intersection*3)
                    {
                        player4.health--;
                        lazers.RemoveAt(i);
                        explosionTempSound.Play();
                      

                    }

                }

                List<Lazer> lazers2 = player4.lazers;

                for (int i = lazers2.Count - 1; i >= 0; i--)
                {
                    lazers2[i].update();
                    if (lazers2[i].scale < 101)
                    {

                        lazers2[i].scale *= 20;
                        lazers2[i].velocity *= 5;

                    }

                    if (Vector3.Distance(lazers2[i].lazerPosition, player.position) < intersection*2)
                    {
                        player.health--;
                        lazers2.RemoveAt(i);
                        
                        explosionTempSound.Play();
                    }


                }
                if (player.health <= 0)
                {

                    gameMode = 9;
                    explosionSound.Play();
                }
                else if (player4.health <= 0)
                {

                    gameMode = 8;
                    explosionSound.Play();
                }

               

            }

            else if (gameMode == 8 || gameMode==9 || gameMode==10)
            {
                

                
#if XBOX360
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed){
                    player=new Player(Content,PlayerIndex.One);
                    player4 = new Player2(Content, player, PlayerIndex.Two);
                gameMode=7;
                }

                if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed){
                     gameMode = 1;
                    initialCount = 0;
                    // finalCount = 0;
                    wave = 0;
                    spriteBatch = new SpriteBatch(GraphicsDevice);
                    device = graphics.GraphicsDevice;
                    deathStarPosition = new Vector3(100090f, 950900f, -100000f);
                    player = new Player(Content,PlayerIndex.One);
                    //intersection = 90000;
                    device.RasterizerState = RasterizerState.CullCounterClockwise;
                    enemyBase = new EnemyBase(Content, "tradaFedaerationCruiser", new Vector3(0, 0, -1500f));
                    executor = new EnemyBase(Content, "strikeBack8", deathStarPosition);
                    //enemyNumber = 15;
                    camera = new Camera();
                    score = 0;

                    enemyShips = new List<EnemyShip>();

                    enemyLazers = new List<Lazer>();
                   
                    

                    enemyShips.Add(new Gunship(Content, player, rand));
                    enemyShips.Add(new TaiFighter(Content, player, rand));
                    enemyShips.Add(new Interceptor(Content, player, rand));
                    enemyShips.Clear();

                    for (int i = 0; i < tenWaves[wave].X; i++)
                    {
                        enemyShips.Add(new TaiFighter(Content, player, rand));
                        enemyNumber++;
                    }
                    for (int i = 0; i < tenWaves[wave].Y; i++)
                    {
                        enemyShips.Add(new Gunship(Content, player, rand));
                        enemyNumber++;
                    }
                    for (int i = 0; i < tenWaves[wave].Z; i++)
                    {
                        enemyShips.Add(new Interceptor(Content, player, rand));
                        enemyNumber++;
                    }


                    //skySphere = new Skysphere(Content);

                    
                    //crosshairs

                    


                    
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };

                }

#endif


            }
            else if (gameMode == 0)
            {

#if XBOX360
                if (GamePad.GetState(PlayerIndex.One).DPad.Right==ButtonState.Pressed){
                      if(multiPlayer==false&&rightLastPressed==false){
                    multiPlayer=true;
                    }
                    rightLastPressed = true;
                    
                }
                else {
                    rightLastPressed=false;
                }
                 if (GamePad.GetState(PlayerIndex.One).DPad.Left==ButtonState.Pressed){
                     if (multiPlayer == true&&leftLastPressed==false)
                    {
                        multiPlayer = false;
                    }
                    leftLastPressed = true;
                }
                else
                {
                    leftLastPressed = false;
                }
                
                
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed){
                    if (multiPlayer){
                gameMode =6;
                }
                else {
                    gameMode=5;
                }
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song);
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
                    

#else
                
                    KeyboardState state = Keyboard.GetState();
                   
                if (state.IsKeyDown(Keys.Enter))
                {
                    gameMode = 1;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song);
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
#endif

            }

            

            // TODO: Add your update logic here

            if (player.health <= 0 && gameMode != 3&&gameMode==1)
            {
#if XBOX360
                buzzerCount=0;
#endif
                int check = 0;
                // Save the game state (in this case, the high score).
               // File file = new File();
                using (StreamReader sr = new StreamReader("Content\\final.txt"))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        check = Convert.ToInt32(line);
                    }
                }
                if (score > check)
                {
                    
                    highScore = score;
                    FileStream fs = new FileStream("Content\\two.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);

                    sw.WriteLine(score);


                    sw.Close();
                    fs.Close();

                }
                else
                {

                    highScore = check;
                }

               
               


                // GamePad.SetVibration(PlayerIndex.One, 1f, 1f);

                explosionSound.Play();
                gameMode = 3;
                // Save the game state (in this case, the high score)
            }
            
            if (enemyNumber <= 0 && initialCount == 0)
            {
                wave++;
                player.health =10;
            }
            if (enemyNumber <= 0)
            {
                
                initialCount++;
                if (initialCount == 500)
                {
                    initialCount = 0;
                    if (wave <= 11)
                    {
                        for (int i = 0; i < tenWaves[wave].X; i++)
                        {
                            enemyShips.Add(new TaiFighter(Content, player, rand));
                            enemyNumber++;
                        }
                        for (int i = 0; i < tenWaves[wave].Y; i++)
                        {
                            enemyShips.Add(new Gunship(Content, player, rand));
                            enemyNumber++;
                        }
                        for (int i = 0; i < tenWaves[wave].Z; i++)
                        {
                            enemyShips.Add(new Interceptor(Content, player, rand));
                            enemyNumber++;
                        }
                    }

                    else
                    {
                        for (int i = 0; i < rand.Next(0, wave * 3); i++)
                        {
                            enemyShips.Add(new TaiFighter(Content, player, rand));
                            enemyNumber++;
                        }
                        for (int i = 0; i < rand.Next((int)wave + (int)(wave / 1.5)); i++)
                        {
                            enemyShips.Add(new Gunship(Content, player, rand));
                            enemyNumber++;
                        }
                        for (int i = 0; i < rand.Next(0, wave * 2); i++)
                        {
                            enemyShips.Add(new Interceptor(Content, player, rand));
                            enemyNumber++;
                        }

                    }
                }

            }
            if (gameMode == 4 || gameMode == 3)
            {
                
                //if end game check if any key pressed and then restart by just redoing setup
                
#if XBOX360
               if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed){
                    player=new Player(Content,PlayerIndex.One);
                    player4 = new Player2(Content, player, PlayerIndex.Two);
                    gameMode=7;

                }


                else if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                {
                  // Save the game state (in this case, the high score).

            
                     gameMode = 1;
                    initialCount = 0;
                    // finalCount = 0;
                    wave = 0;
                    spriteBatch = new SpriteBatch(GraphicsDevice);
                    device = graphics.GraphicsDevice;
                    deathStarPosition = new Vector3(100090f, 950900f, -100000f);
                    player = new Player(Content,PlayerIndex.One);
                    //intersection = 90000;
                    device.RasterizerState = RasterizerState.CullCounterClockwise;
                    enemyBase = new EnemyBase(Content, "tradaFedaerationCruiser", new Vector3(0, 0, -1500f));
                    executor = new EnemyBase(Content, "strikeBack8", deathStarPosition);
                    //enemyNumber = 15;
                    camera = new Camera();
                    score = 0;

                    enemyShips = new List<EnemyShip>();

                    enemyLazers = new List<Lazer>();
                   
                    

                    enemyShips.Add(new Gunship(Content, player, rand));
                    enemyShips.Add(new TaiFighter(Content, player, rand));
                    enemyShips.Add(new Interceptor(Content, player, rand));
                    enemyShips.Clear();

                    for (int i = 0; i < tenWaves[wave].X; i++)
                    {
                        enemyShips.Add(new TaiFighter(Content, player, rand));
                        enemyNumber++;
                    }
                    for (int i = 0; i < tenWaves[wave].Y; i++)
                    {
                        enemyShips.Add(new Gunship(Content, player, rand));
                        enemyNumber++;
                    }
                    for (int i = 0; i < tenWaves[wave].Z; i++)
                    {
                        enemyShips.Add(new Interceptor(Content, player, rand));
                        enemyNumber++;
                    }


                    //skySphere = new Skysphere(Content);

                    
                    //crosshairs

                    


                    
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
#else

                KeyboardState state = Keyboard.GetState();
                if (state.GetPressedKeys().Length > 0 && state.IsKeyDown(Keys.Space) == false && state.IsKeyDown(Keys.Up) == false && state.IsKeyDown(Keys.W) == false && state.IsKeyDown(Keys.Right) == false && state.IsKeyDown(Keys.Left) == false && state.IsKeyDown(Keys.Down) == false)
                {
                    
                    gameMode = 1;
                    initialCount = 0;
                    // finalCount = 0;
                    wave = 0;
                    spriteBatch = new SpriteBatch(GraphicsDevice);
                    device = graphics.GraphicsDevice;
                    deathStarPosition = new Vector3(100090f, 950900f, -100000f);
                    player = new Player(Content,PlayerIndex.One);
                    //intersection = 90000;
                    device.RasterizerState = RasterizerState.CullCounterClockwise;
                    enemyBase = new EnemyBase(Content, "tradaFedaerationCruiser", new Vector3(0, 0, -1500f));
                    executor = new EnemyBase(Content, "strikeBack8", deathStarPosition);
                    //enemyNumber = 15;
                    camera = new Camera();
                    score = 0;

                    enemyShips = new List<EnemyShip>();

                    enemyLazers = new List<Lazer>();
                   
                    

                    enemyShips.Add(new Gunship(Content, player, rand));
                    enemyShips.Add(new TaiFighter(Content, player, rand));
                    enemyShips.Add(new Interceptor(Content, player, rand));
                    enemyShips.Clear();

                    for (int i = 0; i < tenWaves[wave].X; i++)
                    {
                        enemyShips.Add(new TaiFighter(Content, player, rand));
                        enemyNumber++;
                    }
                    for (int i = 0; i < tenWaves[wave].Y; i++)
                    {
                        enemyShips.Add(new Gunship(Content, player, rand));
                        enemyNumber++;
                    }
                    for (int i = 0; i < tenWaves[wave].Z; i++)
                    {
                        enemyShips.Add(new Interceptor(Content, player, rand));
                        enemyNumber++;
                    }


                    //skySphere = new Skysphere(Content);

                    
                    //crosshairs

                    


                    
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
#endif
            }
            if (gameMode == 2)
            {

#if XBOX360
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed){
                    gameMode =1;
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
                    

#else
                KeyboardState state = Keyboard.GetState();
                if (state.GetPressedKeys().Length > 0)
                {
                    gameMode = 1;
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
#endif
            }
            if (gameMode == 1)
            {
#if XBOX360
                if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed){
                    gameMode =2;
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
                    

#else
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.P))
                {
                    gameMode = 2;
                    GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                }
#endif
                player.update();
                for (int i=missiles.Count-1;i>=0;i--){
                    if (Vector3.Distance(missiles[i].lazerPosition, missiles[i].target.position) < intersection)
                    {
                        missiles[i].target.health -= 15;
                        explosionTempSound.Play();
                        missiles.RemoveAt(i);
                    }
                    else
                    {
                        missiles[i].update();
                    }
                }

                for (int i=enemyShips.Count-1;i>=0;i--){

                    if (enemyShips[i].health <= 0)
                    {
                        enemyShips.RemoveAt(i);
                        explosionTempSound.Play();
                       

                    }
                }
                
                enemyBase.update();
                executor.update();
                

                List<Lazer> lazers = player.lazers;
                foreach (Lazer lazer in lazers)
                {
                    lazer.update();
                    

                }
                for (int i = enemyLazers.Count - 1; i >= 0; i--)
                {
                    enemyLazers[i].update();
                    if (Vector3.Distance(enemyLazers[i].lazerPosition, player.position) < 100000)
                    {
                        enemyLazers[i].scale = enemyLazers[i].scale + 20;
                        enemyLazers[i].velocity = enemyLazers[i].velocity + 80;
                    }
                    if (Vector3.Distance(enemyLazers[i].lazerPosition, player.position) < intersection/2)
                    {

                        enemyLazers.RemoveAt(i);
                        player.health--;
                        explosionTempSound.Play(0.2f, 0, 0);
#if XBOX360
                        //GamePad.SetVibration(PlayerIndex.One, 0.5f, 0.5f);
                        buzzerCount=0;
#endif


                    }
                    else if (Vector3.Distance(enemyLazers[i].lazerPosition, player.position) >10000000)
                    {
                        //Console.WriteLine("a");
                        enemyLazers.RemoveAt(i);
                    }

                }
                List<EnemyShip> check = new List<EnemyShip>();
                for (int b = enemyShips.Count - 1; b >= 0; b--)
                {

                    for (int c = enemyShips.Count - 1; c >= 0; c--)
                    {
                        if (c != b)
                        {

                            if (Vector3.Distance(enemyShips[b].position, enemyShips[c].position) < intersection*3)
                            {
                                check.Add(enemyShips[b]);
                                check.Add(enemyShips[c]);
                                //explosionTempSound.Play();
                               // enemyNumber = enemyNumber - 2;

                            }
                        }

                    }
                }

                enemyShips = enemyShips.Except(check).ToList();
                enemyNumber = enemyShips.Count;
               

                for (int b = enemyShips.Count - 1; b >= 0; b--)
                {
                    
                        if (enemyShips[b].angleVelocity == 1f)
                        {
                            if (Vector3.Distance(enemyShips[b].position, player.position) < intersection)
                            {
                                player.health = 0;
                                enemyShips.RemoveAt(b);
                                score++;
                                //enemyNumber--;
                            }

                        }
                        else
                        {
                            if (Vector3.Distance(enemyShips[b].position, player.position) < intersection*2)
                            {
                                player.health = 0;
                                enemyShips.RemoveAt(b);
                                score++;
                                //enemyNumber--;
                            }
                        }

                    
                    

                }
                flag = false;
                foreach (EnemyShip enemy in enemyShips)
                {
                    enemy.update();
                     flag2 = false;
                    float distance=Vector3.Distance(player.position,enemy.position);
                    if (Vector3.Distance(player.position + player.world.Down * distance, enemy.position) < intersection*4)
                    {
                        enemy.lockOnCount++;
                        
                        if (enemy.lockOnCount > 182)
                            
                        {
#if XBOX360
                            if (GamePad.GetState(PlayerIndex.One).Buttons.RightShoulder == ButtonState.Pressed)
#else
                            if (state.IsKeyDown(Keys.M))

#endif
                            {
                                  missiles.Add(new Missile(Content,player.world,1000,enemy, player.world.Translation + (player.world.Forward * 50f)));
                                  enemy.lockOnCount = 0;
                            }
                            flag2 = true;
                            flag = true;

                            
                        }
                    }
                    else
                    {

                        enemy.lockOnCount = 0;
                    }

                   




                }

                
                //fixthisx
                //checking lazer/enemy ship collisions
                bool lazerRemove = false;
                for (int i = lazers.Count - 1; i >= 0; i--)
                {

                    for (int b = enemyShips.Count - 1; b >= 0; b--)
                    {

                        if (enemyShips[b].angleVelocity == 1f)
                        {
                            if (Vector3.Distance(lazers[i].lazerPosition, enemyShips[b].position) < intersection)
                            {
                                enemyShips[b].health--;
                                if (enemyShips[b].health <= 0)
                                {
                                    explosionTempSound.Play();
                                    enemyShips.RemoveAt(b);
                                    enemyNumber--;
                                    score++;
                                }
                                lazerRemove = true;

                            }
                            else if (Vector3.Distance(lazers[i].lazerPosition, player.position) > 5000000)
                            {

                                lazerRemove = true;
                            }
                        }
                        else
                        {
                            if (Vector3.Distance(lazers[i].lazerPosition, enemyShips[b].position) < intersection*2)
                            {
                                enemyShips[b].health--;
                                if (enemyShips[b].health <= 0)
                                {
                                    explosionTempSound.Play();
                                    enemyShips.RemoveAt(b);
                                    enemyNumber--;
                                    score++;
                                }
                                lazerRemove = true;

                            }
                            else if (Vector3.Distance(lazers[i].lazerPosition, player.position) > 5000000)
                            {

                                lazerRemove = true;
                            }

                        }


                    }
                    if (lazerRemove)
                    {
                        lazers.RemoveAt(i);
                    }
                    lazerRemove = false;

                }

                // camera.updateCamera(player);
               
                

               



                view = camera.getViewMatrix(view, player);
                
            

            }

             if (gameMode==7){
                 view2 = camera2.getViewMatrix(view2, player4);
                 view = camera.getViewMatrix(view, player);
                 

                }

            //world = Matrix.CreateRotationX(vAngle) * Matrix.CreateTranslation(position);
            
          
           
           // view = Matrix.CreateRotation
          

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        void DrawPlayer(Viewport viewport)
        {
            GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
            graphics.GraphicsDevice.Viewport = viewport;
           if (gameMode==1  || gameMode==7){
            if (gameMode == 1)
            {
                


                // TODO: Add your drawing code here
                enemyBase.display();
                executor.display();
                skySphere.display();

                foreach (Missile missile in missiles)
                {

                    missile.display();
                }
                //player4.display();
               // player4.update();

                //player2.scale = 10000f;
                //player2.display();
                //Console.WriteLine(player2.position + " second" + player.position);
                player.display();
                //Console.Write("a");
                
                //base.Draw(gameTime);
                List<Lazer> lazers = player.lazers;
                foreach (Lazer lazer in lazers)
                {
                    lazer.display();

                }
                List<Lazer> lazers2 = player4.lazers;
                foreach (Lazer lazer in lazers2)
                {
                    lazer.display();

                }
                foreach (Lazer lazer in enemyLazers)
                {
                    lazer.display();


                }

                foreach (EnemyShip enemy in enemyShips)
                {
                    enemy.display();
                   // Console.WriteLine("a");

                }
                spriteBatch.Begin();
                DrawScenery(viewport, player);
                spriteBatch.DrawString(font, "Score:" + score, new Vector2(viewport.X, viewport.Y), Color.Gold);
                spriteBatch.DrawString(font, "Health:" + player.health, new Vector2(viewport.Width - 100, 0), Color.Gold);
                spriteBatch.DrawString(font, "Wave:" + (wave + 1), new Vector2(viewport.Width / 2 - 25, 0), Color.Gold);

                spriteBatch.End();

            }


                
                //CullMode.CullCounterClockwiseFace;
                if (gameMode == 7)
                {
                    if (viewport.Equals(leftViewport))
                    {
                        
                        //Console.Write("a");
                        player.display(view);
                        
                        //player2.display(view);
                        player4.display(view);
                        enemyBase.display(view);
                        executor.display(view);
                        skySphere.display(view);

                        //base.Draw(gameTime);
                        List<Lazer> lazers = player.lazers;
                        foreach (Lazer lazer in lazers)
                        {
                            lazer.display(view);

                        }
                        List<Lazer> lazers2 = player4.lazers;
                        foreach (Lazer lazer in lazers2)
                        {
                            lazer.display(view);

                        }
                        spriteBatch.Begin();
                        DrawScenery(viewport, player);

                        spriteBatch.DrawString(font, "Health:" + player.health, new Vector2(viewport.Width - 100, 0), Color.Gold);


                        spriteBatch.End();
                    }
                    else
                    {
                        //Console.WriteLine("a");
                        player.display(view2);
                       
                        

                        //Console.WriteLine("a");
                        player4.display(view2);
                        enemyBase.display(view2);
                        executor.display(view2);
                        skySphere.display(view2);

                        //base.Draw(gameTime);
                        List<Lazer> lazers = player.lazers;
                        foreach (Lazer lazer in lazers)
                        {
                            lazer.display(view2);

                        }
                        List<Lazer> lazers2 = player4.lazers;
                        foreach (Lazer lazer in lazers2)
                        {
                            lazer.display(view2);

                        }

                        spriteBatch.Begin();
                        DrawScenery(viewport, player);
                        spriteBatch.DrawString(font, "Health:" + player4.health, new Vector2(viewport.Width - 100, 0), Color.Gold);

                        spriteBatch.End();


                    }

                   

                }




               
                

            }
            spriteBatch.Begin();
            DrawScenery(viewport,player);
            

            spriteBatch.End();
            
        }

        private void DrawScenery(Viewport viewport,Player p )
        {
            
            Rectangle screenRectangle = new Rectangle(0, 0, (int)viewport.Width, (int)viewport.Height);
            //Rectangle screenRectangle = new Rectangle(-100, 0, (int)screenWidth, (int)screenHeight);
            if (gameMode == 0)
            {
                spriteBatch.Draw(beginningTexture, screenRectangle, Color.White);

#if XBOX360
                spriteBatch.DrawString(font, "Press Start to Play", new Vector2(screenRectangle.Width/2-75, screenRectangle.Height-100), Color.Gold);
#else
                spriteBatch.DrawString(font, "Press Enter to Play", new Vector2(screenRectangle.Width / 2 - 75, screenRectangle.Height - 100), Color.Gold);
#endif
#if XBOX360
                if (multiPlayer == true)
                {
                    spriteBatch.DrawString(font, "Singleplayer", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.White);
                    spriteBatch.DrawString(font, "Multiplayer", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.Gold);
                }
                else
                {
                    spriteBatch.DrawString(font, "Singleplayer", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.Gold);
                    spriteBatch.DrawString(font, "Multiplayer", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.White);
                }

#else
                spriteBatch.DrawString(font, "Singleplayer", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.Gold);
                spriteBatch.DrawString(font, "Multiplayer", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.White);
                
#endif
            }
            if (gameMode == 1)
            {
                if (flag == false)
                {
                    spriteBatch.Draw(crosshairs, new Vector2((int)screenRectangle.Width / 2 - (int)crosshairs.Width / 2, (int)screenRectangle.Height / 2 - (int)crosshairs.Height - 77), Color.White);
                }

                else
                {

                    spriteBatch.Draw(crosshairs2, new Vector2((int)screenRectangle.Width / 2 - (int)crosshairs.Width / 2, (int)screenRectangle.Height / 2 - (int)crosshairs.Height - 77), Color.White);
                }
                }
            else if (gameMode == 2)
            {
                spriteBatch.Draw(pauseTexture, screenRectangle, Color.White);
                spriteBatch.DrawString(font, "Paused", new Vector2(screenRectangle.Width / 2 - 25, 0), Color.Gold);
#if XBOX360
                spriteBatch.DrawString(font, "Press Start to Continue", new Vector2(screenRectangle.Width/2 - 100, screenRectangle.Height - 100), Color.Gold);
#else
                spriteBatch.DrawString(font, "Press Enter to Continue", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height - 100), Color.Gold);
#endif
                spriteBatch.DrawString(font, "Right trigger to fire, Left trigger to boost, Left Bumper to slow down, DPad aim, Joysticks to steer, use brain  ", new Vector2(0, screenRectangle.Width - 250), Color.Gold);

            }
            else if (gameMode == 3)
            {
                spriteBatch.Draw(pauseTexture, screenRectangle, Color.White);

                spriteBatch.DrawString(font, "You Lose with a score of: " + score, new Vector2(screenRectangle.Width / 2 - 75, 0), Color.Red);
                spriteBatch.DrawString(font, "Highscore: " + highScore, new Vector2(screenRectangle.Width / 2 - 75, screenRectangle.Height / 2), Color.Gold);
#if XBOX360
                spriteBatch.DrawString(font, "Press Start to Continue", new Vector2(screenRectangle.Width/2 - 75, screenRectangle.Height - 100), Color.Red);
#else
                spriteBatch.DrawString(font, "Press Enter to Continue", new Vector2(screenRectangle.Width / 2 - 75, screenRectangle.Height - 100), Color.Red);
#endif
            }

            else if (gameMode == 4)
            {
                spriteBatch.Draw(pauseTexture, screenRectangle, Color.White);

                spriteBatch.DrawString(font, "You Lose with a score of: " + score, new Vector2(screenWidth / 2 - 75, 0), Color.Red);
                spriteBatch.DrawString(font, "Highscore: " + highScore, new Vector2(screenRectangle.Width / 2 - 75, screenRectangle.Height / 2), Color.Gold);
#if XBOX360
                spriteBatch.DrawString(font, "Press Start to Continue", new Vector2(screenRectangle.Width/2 - 75, screenRectangle.Height - 100), Color.Red);
#else
                spriteBatch.DrawString(font, "Press Enter to Continue", new Vector2(screenRectangle.Width / 2 - 75, screenRectangle.Height - 100), Color.Red);
#endif
            }

            else if (gameMode == 5)
            {

#if XBOX360
                spriteBatch.DrawString(font, "Press A to Play", new Vector2(screenRectangle.Width/2-75, screenRectangle.Height-100), Color.Gold);
#else
                spriteBatch.DrawString(font, "Press Enter to Play", new Vector2(screenRectangle.Width / 2 - 75, screenRectangle.Height - 100), Color.Gold);
#endif
                if (player.inverted == true)
                {
                    spriteBatch.DrawString(font, "Regular", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.White);
                    spriteBatch.DrawString(font, "Inverted", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.Gold);
                }
                else
                {
                    spriteBatch.DrawString(font, "Regular", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.Gold);
                    spriteBatch.DrawString(font, "Inverted", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.White);
                }
                //Console.WriteLine("a");

            }
            else if (gameMode == 6)
            {
#if XBOX360
                spriteBatch.DrawString(font, "Press A to Start", new Vector2(screenRectangle.Width/2-75, screenRectangle.Height-100), Color.Gold);
#else
                spriteBatch.DrawString(font, "Press Enter to Play", new Vector2(screenRectangle.Width / 2 - 75, screenRectangle.Height - 100), Color.Gold);
#endif
                if (viewport.Equals(leftViewport)||viewport.Equals(mainViewport))
                {
                    if (player.inverted == true)
                    {
                        spriteBatch.DrawString(font, "Regular", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.White);
                        spriteBatch.DrawString(font, "Inverted", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.Gold);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Regular", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.Gold);
                        spriteBatch.DrawString(font, "Inverted", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.White);
                    }
                }
                    else {

                    if (player4.inverted == true)
                    {
                        spriteBatch.DrawString(font, "Regular", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.White);
                        spriteBatch.DrawString(font, "Inverted", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.Gold);
                    }
                    else
                    {
                        spriteBatch.DrawString(font, "Regular", new Vector2(screenRectangle.Width / 2 - 100, screenRectangle.Height / 2 + 80), Color.Gold);
                        spriteBatch.DrawString(font, "Inverted", new Vector2(screenRectangle.Width / 2 + 100, screenRectangle.Height / 2 + 80), Color.White);
                    }
                }
                    

                
               

            }
            else if (gameMode == 7)
            {
                spriteBatch.Draw(crosshairs, new Vector2((int)screenRectangle.Width / 2 - (int)crosshairs.Width / 2, (int)screenRectangle.Height / 2 - (int)crosshairs.Height/20 - 77), Color.White);   

            }

            else if (gameMode == 8)
            {
                spriteBatch.Draw(pauseTexture, screenRectangle, Color.White);
                if (viewport.Equals(leftViewport))
                {

                    spriteBatch.DrawString(font, "You Win!", new Vector2(screenRectangle.Width / 2 - 75, 0), Color.Gold);
                }
                else
                {
                    spriteBatch.DrawString(font, "You Lose!", new Vector2(screenRectangle.Width / 2 - 75, 0), Color.Red);

                }
                
#if XBOX360
                spriteBatch.DrawString(font, "Press Start to Continue", new Vector2(screenRectangle.Width/2 - 75, screenRectangle.Height - 100), Color.Red);
#endif

            }
            else if (gameMode == 9)
            {
                spriteBatch.Draw(pauseTexture, screenRectangle, Color.White);
                if (viewport.Equals(leftViewport))
                {

                    spriteBatch.DrawString(font, "You Lose!", new Vector2(screenRectangle.Width / 2 - 75, 0), Color.Red);
                }
                else
                {
                    spriteBatch.DrawString(font, "You Win!", new Vector2(screenRectangle.Width / 2 - 75, 0), Color.Gold);

                }

#if XBOX360
                spriteBatch.DrawString(font, "Press Start to Continue", new Vector2(screenRectangle.Width/2 - 75, screenRectangle.Height - 100), Color.Red);
#endif

            }
            else if (gameMode == 10)
            {
                spriteBatch.Draw(pauseTexture, screenRectangle, Color.White);

                spriteBatch.DrawString(font, "You tied!", new Vector2(screenRectangle.Width / 2 - 75, 0), Color.White);


            }
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            //Console.WriteLine("c");
            if (multiPlayer && (gameMode==7 || gameMode==6||gameMode==8||gameMode==9 || gameMode==10))
            {
                DrawPlayer(leftViewport);
                DrawPlayer(rightViewport);
               // Console.WriteLine("b");

            }
            else
            {
                DrawPlayer(mainViewport);
                //Console.WriteLine("d");
            }
            base.Draw(gameTime);
        }

       


    }

}

