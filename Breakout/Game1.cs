using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;

namespace Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private bool _isColliding = false;
        private RectangleF _rectA;
        private RectangleF _rectB;
        private float _speed = 200.0f;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _rectA = new Rectangle(100, 10, 50, 30);
            _rectB = new Rectangle(100, 100, 200, 100);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Up)) 
            {
                _rectA.Y -= _speed * gameTime.GetElapsedSeconds();
            }

            if (kbState.IsKeyDown(Keys.Down))
            {
                _rectA.Y += _speed * gameTime.GetElapsedSeconds();
            }

            if (kbState.IsKeyDown(Keys.Left))
            {
                _rectA.X -= _speed * gameTime.GetElapsedSeconds();
            }

            if (kbState.IsKeyDown(Keys.Right)) 
            {
                _rectA.X += _speed * gameTime.GetElapsedSeconds();
            }

            if (_rectA.Y <= 0) 
                _rectA.Y = 0;
            if (_rectA.Bottom >= _graphics.PreferredBackBufferHeight)
                _rectA.Y = _graphics.PreferredBackBufferHeight - _rectA.Height;

            if (_rectA.X <= 0)
                _rectA.X = 0;
            if (_rectA.Right >= _graphics.PreferredBackBufferWidth)
                _rectA.X = _graphics.PreferredBackBufferWidth - _rectA.Width;

            if (_rectA.Intersects(_rectB))
                _isColliding = true;
            else
                _isColliding = false;
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawRectangle(_rectA, _isColliding ? Color.Red : Color.White);
            _spriteBatch.DrawRectangle(_rectB, _isColliding ? Color.Red : Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
