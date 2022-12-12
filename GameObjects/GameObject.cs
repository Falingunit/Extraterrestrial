using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;
using MonoGame.Extended;
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
        protected SliceKey BoundsSlice;
        protected Rectangle Bounds;
        protected List<Rectangle> Collidables;

        protected AsepriteDocument SpriteDocument;
        protected AnimatedSprite Sprite;

        protected Game1 Game;

        public GameObject(Vector2 Position, Game1 game, List<Rectangle> collidables)
        {
            this.Position = Position;
            Game = game;
            Collidables = collidables;
        }

        public abstract void Load();
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch _spriteBatch);

        protected virtual void DefaultUpdates(GameTime gameTime)
        {
            Position += Velocity;
            Sprite.Position = Position;
            Sprite.TryGetCurrentFrameSlice("Bounds", out BoundsSlice);
            Bounds = BoundsSlice.Bounds;
            Sprite.Update(gameTime);
        }
        protected virtual void DefaultDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Render(spriteBatch);
            spriteBatch.DrawRectangle(Bounds, Color.Red);
        }

        public Vector2 GetVelocity()
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
        public Vector2 GetCenteredPosition()
        {
            return new Vector2(Bounds.Center.X, Bounds.Center.Y);
        }
        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }
        public Rectangle GetBounds()
        {
            return Bounds;
        }
    }
}
