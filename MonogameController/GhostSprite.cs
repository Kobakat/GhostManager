using System;
using System.CodeDom;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace MonogameController
{
    public class GhostSprite : Sprite
    {
        public Vector2 Forward { get; set; }

        public Texture2D ghostHunt;
        public Texture2D ghostHide;
        public GhostController ghostController;
        public MarioSprite mario;
        public SpriteBatch spriteBatch;

        

        Random r;

        public GhostSprite(Game game, MarioSprite Mario) : base(game)
        {
            this.ghostController = new GhostController() { ghostSprite = this };
            this.r = new Random();

            this.mario = Mario;
            this.Forward = new Vector2(1, 0);       
        }

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);

            this.ghostHide = this.Game.Content.Load<Texture2D>("BooHide");
            this.ghostHunt = this.Game.Content.Load<Texture2D>("BooHunt");

            this.SpriteTexture = this.ghostHide;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            ghostController.ControllerUpdate();
            base.Update(gameTime);
        }

        public Vector2 GetRandLocation()
        {
            Vector2 loc;
            loc.X = r.Next(0, GraphicsDevice.Viewport.Width - this.spriteTexture.Width);
            loc.Y = r.Next(0, GraphicsDevice.Viewport.Height - this.spriteTexture.Height);
            return loc;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.SpriteTexture, this.Location, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
