using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
using MonoGameLibrary.Sprite;


namespace MonogameController
{
    public class GhostManager : DrawableGameComponent
    {
        List<GhostSprite> ghosts;
        List<GhostSprite> removeGhosts;

        Random r;

        public InputHandler inputHandler;
        public MarioSprite mario;


        public GhostManager(Game game, MarioSprite Mario) : base(game)
        {
            this.ghosts = new List<GhostSprite>();
            this.removeGhosts = new List<GhostSprite>();
            this.mario = Mario;
            this.r = new Random();
        }

        #region Add or Remove ghost
        private void AddGhost()
        {
            GhostSprite g = new GhostSprite(Game, this.mario);

            g.Initialize();
            g.Location = g.GetRandLocation();
            g.SetTranformAndRect(); //Ghost location changed and update wasn't called to we need to update the rectagle

            //no overlapping
            foreach (GhostSprite otherGhost in ghosts)
            {
                while (g.Intersects(otherGhost))
                {
                    g.Location = g.GetRandLocation();
                    g.SetTranformAndRect(); //Ghost location changed and update wasn't called to we need to update the rectagle
                }
            }

            g.Speed = r.Next(50, 101);
            g.Scale = 1.0f;
            g.Enabled = true;
            g.Visible = true;
            ghosts.Add(g);

        }

        //Remove a random ghost
        private void RemoveGhost()
        {
            if(this.ghosts.Count > 0)
            {
                int choice = r.Next(0, this.ghosts.Count);

                removeGhosts.Add(ghosts[choice]);
            }        
        }

        #endregion

        void ReadInput()
        {
            if (inputHandler.WasKeyPressed(Keys.Q))
            {
                this.AddGhost();
            }

            if (inputHandler.WasKeyPressed(Keys.E))
            {
                this.RemoveGhost();
            }
        }

        //If ghosts collide, they "bounce" off one another and move away from eachother for 3 seconds
        void CheckForCollions(GhostSprite g)
        {
            foreach (GhostSprite o in ghosts)
            {
                //Make sure the ghost isn't colliding with itself
                if (g != o)
                {
                    if(Sprite.IntersectPixels(g.LocationRect, g.SpriteTextureData, o.LocationRect, o.SpriteTextureData))
                    {
                        g.Direction = Vector2.Normalize(o.Location - g.Location) * -1;
                        o.Direction = Vector2.Normalize(g.Location - o.Location) * -1;

                        g.ghostController.ghostState.state = GhostMode.Avoiding;
                        o.ghostController.ghostState.state = GhostMode.Avoiding;

                        g.ghostController.startAvoidTime = Game1.time;
                        o.ghostController.startAvoidTime = Game1.time;

                        g.SpriteTexture = g.ghostHide;
                        o.SpriteTexture = o.ghostHide;
                    }                               
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            this.ReadInput();

            foreach (GhostSprite ghost in ghosts)
            {
                if (ghost.Enabled)
                {
                    ghost.Update(gameTime);

                    CheckForCollions(ghost);
                }                           
            }

            foreach(GhostSprite ghost in removeGhosts)
            {
                ghosts.Remove(ghost);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(GhostSprite ghost in ghosts)
            {
                if(ghost.Visible)
                {
                    ghost.Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }
    }
}
