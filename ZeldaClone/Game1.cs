using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Entities;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;

namespace ZeldaClone
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D character;
        private Vector2 characterPos;

        private Direction characterDirection = Direction.RIGHT;

        private TiledMap TMap;
        private TiledMapRenderer MapRenderer;

        private ViewportAdapter viewportAdapter;
        private OrthographicCamera _camera;

        public float scale = 0.44444f;

        public World _world;

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
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1200;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here
            TMap = Content.Load<TiledMap>("Maps/TestMap02");
            MapRenderer = new TiledMapRenderer(GraphicsDevice, TMap);

            viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 32*16, 18*16);
            _camera = new OrthographicCamera(viewportAdapter);

            Window.AllowUserResizing = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _camera.LookAt(new Vector2(TMap.WidthInPixels, TMap.HeightInPixels) *0.5f);
            _camera.Position = new Vector2(0, 0);

            // TODO: use this.Content to load your game content here
            character = Content.Load<Texture2D>("Art/Character/idle_1");
            characterPos = Vector2.Zero;
        }

        protected override void Update(GameTime gameTime)
        {
            
            MapRenderer.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState inputState = Keyboard.GetState();

            // LEFT
            if(inputState.IsKeyDown(Keys.A) || inputState.IsKeyDown(Keys.Left))
            {
                characterPos.X -= 120 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                characterDirection = Direction.LEFT;
            }
            // RIGHT
            if(inputState.IsKeyDown(Keys.D) || inputState.IsKeyDown(Keys.Right))
            {
                characterPos.X += 120 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                characterDirection = Direction.RIGHT;
            }
            // UP
            if(inputState.IsKeyDown(Keys.W) || inputState.IsKeyDown(Keys.Up))
            {
                characterPos.Y -= 120 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            // DOWN
            if(inputState.IsKeyDown(Keys.S) || inputState.IsKeyDown(Keys.Down))
            {
                characterPos.Y += 120 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var viewMatrix = _camera.GetViewMatrix();
            var projectionMatrix = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width-1, GraphicsDevice.Viewport.Height-1, 0, 0f, -1f);

            //MapRenderer.Draw(null, null, null, 0f);
            MapRenderer.Draw(viewMatrix, projectionMatrix, null, 0);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            
            // TODO: Add your drawing code here
            if (characterDirection == Direction.LEFT)
                _spriteBatch.Draw(character, characterPos, null, Color.White, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, 0f);
            else if(characterDirection == Direction.RIGHT)
                _spriteBatch.Draw(character, characterPos, null, Color.White, 0f, Vector2.Zero, 1.25f, SpriteEffects.FlipHorizontally, 0f);

            

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
