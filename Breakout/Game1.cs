using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using System.Collections.Generic;

namespace Breakout
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private bool _isColliding = false;
        private RectangleF _rectA;
        private CircleF _circle;
        private List<RectangleF> _bricksList = new List<RectangleF>();
        private Vector2 _ballSpeed = new Vector2(100.0f, 100.0f);
        private float _rectangleSpeed = 300.0f;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _circle = new CircleF(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 6.0f);
            BricksInitializer();
            _rectA = new RectangleF(_graphics.PreferredBackBufferWidth / 2 - (50 / 2.0f), _graphics.PreferredBackBufferHeight - 20, 50, 10);

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

            _circle.Center.X += _ballSpeed.X * gameTime.GetElapsedSeconds();
            _circle.Center.Y += _ballSpeed.Y * gameTime.GetElapsedSeconds();
            
            // ball top, left, right collisions
            if (_circle.Center.Y <= 0)
            {
                _circle.Center.Y = 0;
                _ballSpeed.Y *= -1.1f;
            }

            if (_circle.Center.X <= 0)
            {
                _circle.Center.X = 0;
                _ballSpeed.X *= -1.1f;
            }
            if (_circle.Center.X + _circle.Radius >= _graphics.PreferredBackBufferWidth)
            {
                _circle.Center.X = _graphics.PreferredBackBufferWidth - _circle.Radius;
                _ballSpeed.X *= -1.1f;
            }


            if (kbState.IsKeyDown(Keys.Q))
            {
                _rectA.X -= _rectangleSpeed * gameTime.GetElapsedSeconds();
            }

            if (kbState.IsKeyDown(Keys.D)) 
            {
                _rectA.X += _rectangleSpeed * gameTime.GetElapsedSeconds();
            }

            if (_circle.Intersects(_rectA.BoundingRectangle))
                _ballSpeed.Y *= -1.1f;
            

            if (_rectA.X <= 0)
                _rectA.X = 0;
            if (_rectA.Right >= _graphics.PreferredBackBufferWidth)
                _rectA.X = _graphics.PreferredBackBufferWidth - _rectA.Width;

            for (int i = 0; i < _bricksList.Count; i++)
            {
                if (_circle.Intersects(_bricksList[i].BoundingRectangle))
                {
                    _bricksList.Remove(_bricksList[i]);
                    _ballSpeed.Y *= -1.1f;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.DrawCircle(_circle, 20, Color.White);
            foreach (var rect in _bricksList)
            {
                _spriteBatch.DrawRectangle(rect, Color.White);
            }
            _spriteBatch.DrawRectangle(_rectA, _isColliding ? Color.Red : Color.White);

            _spriteBatch.End();


            base.Draw(gameTime);
        }

        private void BricksInitializer()
        {
            SizeF size = new SizeF(20, 10);
            Vector2 pos = new Vector2(10, 10);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 39; j++)
                {
                    _bricksList.Add(new RectangleF(pos, size));
                    pos.X += size.Width;
                }
                pos.X = 10;
                pos.Y += size.Height;
            }
        }
    }
}
