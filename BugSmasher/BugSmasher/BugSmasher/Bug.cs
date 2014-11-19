using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BugSmasher
{
    class Bug : Sprite
    {
        public int playerscore = 0;
        public bool Dead = false;
        float timeRemaining = 0.0f;
        float TimePerUpdate = 1.00f;
        private Random rand = new Random((int)DateTime.UtcNow.Ticks);
        public Bug(
           Vector2 location,
           Texture2D texture,
           Rectangle initialFrame,
           Vector2 velocity) : base (location, texture, initialFrame, velocity)
        {
            System.Threading.Thread.Sleep(1);
        }

        public void Splat()
        {
            frames.Clear();
            frames.Add(new Rectangle(0, 128, 128, 128));
            this.TintColor = Color.Tomato;
            this.velocity = Vector2.Zero;
            this.Dead = true;

        }

        public void Collide()
        {
        }

        public override void Update(GameTime gameTime)
        {

            if (timeRemaining == 0.0f)
            {
                NewTarget();
                timeRemaining = TimePerUpdate;
            }

            timeRemaining = MathHelper.Max(0, timeRemaining -
            (float)gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }
        public void NewTarget()
        {
            Vector2 target;
            if (Velocity.X > 0)
                //target = new Vector2(Location.X + 400, Location.Y + rand.Next(-150, 150));
                target = new Vector2(Location.X + 400, rand.Next(0, 200));
            else
            {
                target = new Vector2(Location.X - 400, Location.Y + rand.Next(-150, 150));
                this.FlipHorizontal = false;
            }
            Vector2 vel = target - Location;
            vel.Normalize();
            vel *= 600;
            Velocity = vel;
            Rotation = (float)Math.Atan2(vel.Y, vel.X);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        internal void Remove(int p)
        {
            throw new NotImplementedException();
        }
    }
}
