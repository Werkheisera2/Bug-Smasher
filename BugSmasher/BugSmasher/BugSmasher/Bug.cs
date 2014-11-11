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
        public bool Dead = false;

        public Bug(
           Vector2 location,
           Texture2D texture,
           Rectangle initialFrame,
           Vector2 velocity) : base (location, texture, initialFrame, velocity)
        {

        }

        public void Splat()
        {
            frames.Clear();
            frames.Add(new Rectangle(0, 128, 128, 128));

            this.velocity = Vector2.Zero;
            this.Dead = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
