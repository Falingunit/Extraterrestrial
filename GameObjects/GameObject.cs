using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraterrestrial.GameObjects
{
    public abstract class GameObject
    {
        protected Vector2 Position;
        protected Vector2 Velocity = Vector2.Zero;

        protected AsepriteDocument spriteDocument;
        protected AnimatedSprite sprite;

        protected Game1 game;

        public GameObject(Vector2 Position, Game1 game)
        {
            this.Position = Position;
            this.game = game;
        }

        public abstract void Load();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch);

        public Vector2 getVelocity()
        {
            return Velocity;
        }
        public void SetVelocity(Vector2 newVelocity)
        {
            Velocity = newVelocity;
        }

        public Vector2 GetPosition()
        {
            return Position;
        }
        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

    }
}
