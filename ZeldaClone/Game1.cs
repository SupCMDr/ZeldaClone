using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ZeldaClone
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D character;
        private Vector2 characterPos;

        private Direction characterDirection = Direction.LEFT;

        private enum Direction
        {
            LEFT,
            RIGHT
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            character = Content.Load<Texture2D>("Art/Character/idle_1");
            characterPos = Vector2.Zero;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState inputState = Keyboard.GetState();

            if(inputState.IsKeyDown(Keys.A))
            {
                characterPos.X -= 1;
                characterDirection = Direction.LEFT;
            }
            if(inputState.IsKeyDown(Keys.D))
            {
                characterPos.X += 1;
                characterDirection = Direction.RIGHT;
            }
            if(inputState.IsKeyDown(Keys.W))
            {
                characterPos.Y -= 1;
            }
            if(inputState.IsKeyDown(Keys.S))
            {
                characterPos.Y += 1;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // TODO: Add your drawing code here
            if(characterDirection == Direction.LEFT)
                _spriteBatch.Draw(character, characterPos, Color.White);
            else if(characterDirection == Direction.RIGHT)
                _spriteBatch.Draw(character, characterPos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0.0f);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
