using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using MonoGameLibrary.Util;

namespace MonogameController
{
    public class MarioController : IFlipable
    {
        public InputHandler inputHandler;
        public MarioState marioState;
        public MarioSprite sprite;

        public MarioController()
        {
            this.marioState = new MarioState();
        }

        public void ControllerUpdate()
        {
            InputUpdate();
            FlipSprite();
            MoveMario();
        }

        //Read in the player input and change mario's move direction based on it
        void InputUpdate()
        {

            if (this.inputHandler.KeyboardState.IsHoldingKey(Keys.A))
            {
                this.sprite.Direction += new Vector2(-1, 0);
            }

            if (this.inputHandler.KeyboardState.IsHoldingKey(Keys.D))
            {
                this.sprite.Direction += new Vector2(1, 0);
            }

            if (this.marioState.state == State.Grounded)
            {
                if (this.inputHandler.WasKeyPressed(Keys.Space))
                {
                    SetJumpSprite();
                    this.sprite.Direction += this.sprite.JumpPower * new Vector2(0, -1);
                    this.marioState.state = State.Airborne;
                }
            }

            //Clamp mario's side to side movement speed
            this.sprite.Direction = new Vector2(
                MathHelper.Clamp(this.sprite.Direction.X, -1, 1),
                this.sprite.Direction.Y);
        }

        //Move mario based on his state
        void MoveMario()
        {
            switch (this.marioState.state)
            {
                case State.Airborne:
                    MarioAirborneMoveUpdate();
                    break;
                case State.Grounded:
                    MarioGroundedMoveUpdate();
                    break;
            }
        }


        void MarioAirborneMoveUpdate()
        {
            this.sprite.Direction += new Vector2(0, 1) * this.sprite.GravityAcceleration;

            this.sprite.Location += new Vector2(this.sprite.Direction.X * this.sprite.Speed, this.sprite.Direction.Y) * Game1.deltaTime;
            

            if (this.sprite.Location.Y >= this.sprite.Bounds.Y - this.sprite.SpriteSize.Y)
            {
                SetMarioSprite();
                this.marioState.state = State.Grounded;
                this.sprite.Direction = Vector2.Zero;
            }
        }

        void MarioGroundedMoveUpdate()
        {
            this.sprite.Location += this.sprite.Direction * Game1.deltaTime * this.sprite.Speed;
            this.sprite.Direction = Vector2.Zero;
        }

        public void FlipSprite()
        {
            if (this.sprite.Direction.X > 0)
            {
                this.sprite.SpriteEffects = SpriteEffects.FlipHorizontally;
                this.sprite.Forward = new Vector2(1, 0);
            }

            if (this.sprite.Direction.X < 0)
            {
                this.sprite.SpriteEffects = SpriteEffects.None;
                this.sprite.Forward = new Vector2(-1, 0);
            }
        }

        void SetMarioSprite()
        {
            this.sprite.spriteTexture = this.sprite.mario;
        }

        void SetJumpSprite()
        {
            this.sprite.spriteTexture = this.sprite.marioJump;
        }
    }

}

