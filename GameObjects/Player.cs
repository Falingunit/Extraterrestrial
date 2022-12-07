using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGame.Aseprite.Graphics;
using MonoGame.Extended;
using System.Threading.Tasks;
using Extraterrestrial.ContentLoaders.GameObjects;
using System.Collections.Generic;
using MonoGame.Extended.Sprites;

namespace Extraterrestrial.GameObjects
{
    public class Player : GameObject
    {
        private enum SpriteMode { idle, running, jumping, falling };
        private enum MoveDirection { none, left, right };

        private const int moveSpeed = 7;
        private const int ground = 300;

        private bool isJumping = false, isFalling = false, isGrounded = false;
        private MoveDirection moveDirection = MoveDirection.none;
        private SpriteMode spriteMode = SpriteMode.idle;

        public Player(Vector2 Position, Game1 game, List<Rectangle> collidables) : base(Position, game, collidables)
        {
        }

        public override void Initialize()
        {
        }

        public override void Load()
        {
            SpriteDocument = PlayerContentLoader.getPlayerSprite();
            Sprite = new MonoGame.Aseprite.Graphics.AnimatedSprite(SpriteDocument, Position)
            {
                Scale = new Vector2(Game1.SCALE, Game1.SCALE),
                Color = Color.White
            };
        }

        public override void Update(GameTime gameTime)
        {
            PlayerMovement();

            if (Position.Y < ground) Velocity += Game1.GRAVITY;
            if (Position.Y >= ground) Position.Y = ground;

            foreach (var sprite in Collidables)
            {
                if ((Velocity.X > 0 && IsTouchingLeft(sprite)) ||
                    (Velocity.X < 0 && IsTouchingRight(sprite)))
                    Velocity.X = 0;

                if (IsTouchingBottom(sprite))
                    isGrounded = true;
                else isGrounded = false;

                if ((Velocity.Y > 0 && IsTouchingTop(sprite)) ||
                    (Velocity.Y < 0 && IsTouchingBottom(sprite)))
                    Velocity.Y = 0;
            }


            DefaultUpdates(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < Collidables.Count; i++)
            {
                _spriteBatch.DrawRectangle(Collidables[i], Color.IndianRed, 1, 0);
            }
            PlayerAnimation();
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

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && Position.Y >= ground || isGrounded)
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
            } else if (isFalling && Position.Y < ground && !isGrounded)
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
