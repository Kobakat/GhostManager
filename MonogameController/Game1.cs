using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;

namespace MonogameController
{
    public class Game1 : Game
    {
        //lets controllers reference time without being a game component
        public static float deltaTime;
        public static float time;

        GraphicsDeviceManager graphics;
        InputHandler inputHandler;
        MarioSprite mario;
        GhostManager ghostManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            inputHandler = new InputHandler(this);
            this.Components.Add(inputHandler);

            mario = new MarioSprite(this);
            this.Components.Add(mario);

            ghostManager = new GhostManager(this, mario) { inputHandler = inputHandler };                               
            this.Components.Add(ghostManager);

            //Feels a bit hacky
            mario.marioController.inputHandler = inputHandler;
        }

        protected override void Initialize() { base.Initialize(); }
        protected override void LoadContent() { }
        protected override void UnloadContent() { }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            deltaTime = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / 1000);
            time = (float)(gameTime.TotalGameTime.TotalMilliseconds / 1000);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
