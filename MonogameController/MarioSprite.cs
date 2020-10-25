using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace MonogameController
{
    public class MarioSprite : Sprite
    {
        public Vector2 Bounds { get; protected set; }
        public Vector2 SpriteSize { get; protected set; }
        public Vector2 Forward { get; set; }

        public float GravityAcceleration { get; protected set; }
        public float JumpPower { get; protected set; }

        public Texture2D mario { get; protected set; }
        public Texture2D marioJump { get; protected set; }

        public MarioController marioController;

        SpriteBatch spriteBatch;
        SpriteFont font;

        public MarioSprite(Game game) : base(game)
        {        
            this.Forward = new Vector2(1, 0);
            this.Direction = Vector2.Zero;

            this.GravityAcceleration = 3;
            this.JumpPower = 325;
            this.Speed = 300;
            this.Scale = 1;
            this.marioController = new MarioController() { sprite = this };
        }

        public override void Update(GameTime gameTime)
        {        
            this.marioController.ControllerUpdate();
            this.KeepMarioInBounds();
        }        

        public override void Draw(GameTime gameTime)
        {
            this.spriteBatch.Begin();
            this.spriteBatch.Draw(this.spriteTexture, this.Location, null, Color.White, 0, this.Origin, 1, this.SpriteEffects, 0);
            this.spriteBatch.DrawString(font, $"Mario is: {this.marioController.marioState.state}", new Vector2(300, 0), Color.White);

            //Putting this here because I am lazy
            this.spriteBatch.DrawString(font, $"Use A and D to move. Use Space to jump.", new Vector2(300, 20), Color.Black);
            this.spriteBatch.DrawString(font, $"Press Q to add a ghost. Press E to delete one.", new Vector2(260, 40), Color.Black);
            this.spriteBatch.DrawString(font, $"Ghost hide when mario looks at them. And chase when he looks away", new Vector2(200, 60), Color.Black);
            this.spriteBatch.DrawString(font, $"If ghost collide with eachother. They move away for 3 seconds.", new Vector2(200, 80), Color.Black);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.mario = this.Game.Content.Load<Texture2D>("Mario");
            this.marioJump = this.Game.Content.Load<Texture2D>("MarioJump");
            this.font = this.Game.Content.Load<SpriteFont>("font");

            this.SpriteTexture = marioJump;

            this.Bounds = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            this.SpriteSize = new Vector2(this.mario.Width, this.mario.Height);

            base.Initialize();
        }
        void KeepMarioInBounds()
        {
            this.Bounds = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            //Adjusts mario's position if he leaves the view frustum
            if (this.Location.X > Bounds.X - SpriteSize.X)
                this.Location = new Vector2(Bounds.X - SpriteSize.X, this.Location.Y);

            if (this.Location.X < 0)
                this.Location = new Vector2(0, this.Location.Y);

            if (this.Location.Y > Bounds.Y - SpriteSize.Y)
                this.Location = new Vector2(this.Location.X, this.Bounds.Y - this.SpriteSize.Y);

            if (this.Location.Y < 0)
                this.Location = new Vector2(this.Location.X, 0);
        }
    }
}
