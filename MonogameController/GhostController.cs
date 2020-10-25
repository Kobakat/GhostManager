using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameController
{ 
    public class GhostController : IFlipable
    {
        public GhostSprite ghostSprite;
        public GhostState ghostState;

        public float startAvoidTime;

        public GhostController()
        {
            this.ghostState = new GhostState();
            this.startAvoidTime = 0;
        }

        public void ControllerUpdate()
        {
            FlipSprite();

            EvaluateGhostState();

            switch(this.ghostState.state)
            {
                case GhostMode.Hunting:
                    GhostHuntingMove();
                    break;
                case GhostMode.Avoiding:
                    GhostAvoidingMove();
                    break;
            }
                
        }

        public void FlipSprite()
        {
            if (ghostSprite.Location.X > this.ghostSprite.mario.Location.X)
            {
                this.ghostSprite.SpriteEffects = SpriteEffects.None;
                this.ghostSprite.Forward = new Vector2(1, 0);
            }

            else
            {
                ghostSprite.SpriteEffects = SpriteEffects.FlipHorizontally;
                this.ghostSprite.Forward = new Vector2(-1, 0);
            }

        }

        void GhostHuntingMove()
        {
            //Normalizing a zero vector is very bad.
            if(this.ghostSprite.mario.Location - this.ghostSprite.Location != Vector2.Zero)
            {
                this.ghostSprite.Direction = Vector2.Normalize(this.ghostSprite.mario.Location - this.ghostSprite.Location);
            }

            this.ghostSprite.Location += this.ghostSprite.Direction * this.ghostSprite.Speed * Game1.deltaTime; 
        }

        void GhostAvoidingMove()
        {
            //lazy magic number, can be replaced with "avoidDuration"
            if (Game1.time < this.startAvoidTime + 3)
                this.ghostSprite.Location += this.ghostSprite.Direction * this.ghostSprite.Speed * Game1.deltaTime;

            else
                this.ghostState.state = GhostMode.Hidden;
        }


        void EvaluateGhostState()
        {
            if (Vector2.Dot(ghostSprite.Forward, this.ghostSprite.mario.Forward) < 0 && this.ghostState.state != GhostMode.Avoiding)
            {
                this.ghostState.state = GhostMode.Hunting;
                this.ghostSprite.SpriteTexture = this.ghostSprite.ghostHunt;
            }

            else if(this.ghostState.state != GhostMode.Avoiding)
            {
                ghostState.state = GhostMode.Hidden;
                this.ghostSprite.SpriteTexture = this.ghostSprite.ghostHide;
            }
        }
    }
}
