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

namespace BugSmasher
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Texture2D BugsSheet;
        Texture2D msvecSheet;
        Texture2D squishSheet;
        
        Random rand = new Random((int)DateTime.UtcNow.Ticks);
        

        Vector2 target = new Vector2(10, 100);

        
        Sprite msvec;
        Sprite squish;
        List<Bug> bugs;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            

            Content.RootDirectory = "Content";
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            MouseState ms = Mouse.GetState();
            

            msvecSheet = Content.Load<Texture2D>("bugs");

            squishSheet = Content.Load<Texture2D>("bugs");

            BugsSheet = Content.Load<Texture2D>("bugs");

            bugs = new List<Bug>();

            background = Content.Load<Texture2D>("background");

           
            msvec = new Sprite(new Vector2(ms.X, ms.Y),
                                 msvecSheet,
                                 new Rectangle(134, 203, 44, 44),
                                 Vector2.Zero);

            squish = new Sprite(new Vector2(40, 60),
                                squishSheet,
                                new Rectangle(19, 156, 44, 44),
                                Vector2.Zero);


            for (int i = 0; i < 100 ; i++)
            {
                bugs.Add(new Bug(new Vector2(-50 + rand.Next(-500, 0), rand.Next(50, 700)),
                    BugsSheet,
                    new Rectangle(rand.Next(0, 3) * 64, rand.Next(0, 2) * 64, 64, 64),
                    new Vector2(100, 0)));
            }

            
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState ms = Mouse.GetState();
                msvec.Location = new Vector2(ms.X - msvec.BoundingBoxRect.Width/2, ms.Y - msvec.BoundingBoxRect.Height/2);

                if (bugs[1].Dead)
                {
                    bugs.Add(new Bug(new Vector2(-10 + rand.Next(-500, 0), rand.Next(50, 700)),
                        BugsSheet,
                        new Rectangle(rand.Next(0, 3) * 64, rand.Next(0, 2) * 64, 64, 64),
                        new Vector2(100, 0)));
                }
           



            for (int i = 0; i < bugs.Count; i++)
            {
                bugs[i].Update(gameTime);


                if (msvec.IsBoxColliding(bugs[i].BoundingBoxRect) && ms.LeftButton == ButtonState.Pressed)
                {
                    bugs[i].Splat();
                }
            }

            msvec.Update(gameTime);
            base.Update(gameTime);

        }           

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            MouseState ms = Mouse.GetState();

            if (ms.RightButton == ButtonState.Pressed && ms.LeftButton == ButtonState.Pressed)
            {
                spriteBatch.Draw(background, Vector2.Zero, Color.Red);
            }
            else
            {
                spriteBatch.Draw(background, Vector2.Zero, Color.White);
            }

            for (int i = 0; i < bugs.Count; i++)
            {
                if (bugs[i].Dead)
                    bugs[i].Draw(spriteBatch);

               

                    if (bugs.Count > 10000)
                        bugs.RemoveAt(10000);

                    if (bugs.Count < 10)
                    {
                        bugs.Add(new Bug(new Vector2(-10 + rand.Next(-500, 0), rand.Next(50, 700)),
                        BugsSheet,
                        new Rectangle(rand.Next(0, 3) * 64, rand.Next(0, 2) * 64, 64, 64),
                        new Vector2(100, 0)));
                    }
                    

                    
            }

            for (int i = 0; i < bugs.Count; i++)
            {
                if (!bugs[i].Dead)
                    bugs[i].Draw(spriteBatch);
            }

            msvec.Draw(spriteBatch);

            

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
