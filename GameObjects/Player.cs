using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.Aseprite.Graphics;
using MonoGame.Extended;
using System.Threading.Tasks;
using Extraterrestrial.ContentLoaders.GameObjects;

namespace Extraterrestrial.GameObjects
{
    public class Player : GameObject
    {
        private enum SpriteMode { idle, running, jumping, falling };
        private enum MoveDirection { none, left, right };

        private const int moveSpeed = 7;
        private const int ground = 300;

        private bool isJumping = false, isFalling = false;
        private MoveDirection moveDirection = MoveDirection.none;
        private SpriteMode spriteMode = SpriteMode.idle;

        public Player(Vector2 Position, Game1 game) : base(Position, game)
        {
        }

        public override void Initialize()
        {
            Console.WriteLine("Player Initialized!");
        }

        public override void Load()
        {
            Scale = 3;
            SpriteDocument = PlayerContentLoader.getPlayerSprite();
            Sprite = new AnimatedSprite(SpriteDocument, Position)
            {
                Scale = new Vector2(Scale, Scale),
                Color = Color.White
            };
        }

        public override void Update(GameTime gameTime)
        {
            DefaultUpdates(gameTime);
            PlayerMovement();

            if (Position.Y < ground) Velocity += Game1.GRAVITY;
            if (Position.Y >= ground) Position.Y = ground;

            PlayerAnimation();
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            DefaultDraw(gameTime, _spriteBatch);
        }

        private void PlayerMovement()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Velocity.X = (float)-moveSpeed;
                moveDirection = MoveDirection.left;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Velocity.X = (float)moveSpeed;
                moveDirection = MoveDirection.right;
            }
            else
            {
                Velocity.X = 0;
                moveDirection = MoveDirection.none;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && Position.Y >= ground)
            {
                Velocity.Y = -15;
                isFalling = false;
                isJumping = true;
            }

            if (Velocity.Y >= 0f)
            {
                isJumping = false;
                isFalling = true;
            }

        }

        private void PlayerAnimation()
        {
            switch (moveDirection)
            {
                case MoveDirection.left:
                    Sprite.SpriteEffect = SpriteEffects.FlipHorizontally;
                    spriteMode = SpriteMode.running;
                    break;
                case MoveDirection.right:
                    Sprite.SpriteEffect = SpriteEffects.None;
                    spriteMode = SpriteMode.running;
                    break;
                case MoveDirection.none:
                    spriteMode = SpriteMode.idle;
                    break;
            }
            
            if (isJumping)
            {
                spriteMode = SpriteMode.jumping;
            } else if (isFalling && Position.Y < ground)
            {
                spriteMode = SpriteMode.falling;
            }

            Sprite.Play(GetSpriteMode());
        }

        private string GetSpriteMode()
        {
            return spriteMode switch
            {
                SpriteMode.idle => "idle",
                SpriteMode.running => "run",
                SpriteMode.jumping => "jump",
                SpriteMode.falling => "fall",
                _ => "idle",
            };
        }

    }
}
