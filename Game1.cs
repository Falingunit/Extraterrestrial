using System;
using Extraterrestrial.ContentLoaders.GameObjects;
using Extraterrestrial.GameObjects;
using Extraterrestrial.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using Extraterrestrial.Tiles;
using Extraterrestrial.ContentLoaders;

namespace Extraterrestrial
{
    public class Game1 : Game
    {
        public static Vector2 GRAVITY = new Vector2(0, 1f);

        private GraphicsDeviceManager _graphics;   
        private SpriteBatch _spriteBatch;

        private GameObjectManager gameObjectManager;
        private TileManager tileManager;

        private PlayerContentLoader playerContentLoader;
        private TileContentLoader tileContentLoader;

        //temporary
        private OrthographicCamera camera;
        private GameObject player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.IsFullScreen = false;
        }

        protected override void Initialize()
        {
            _graphics.ApplyChanges();

            gameObjectManager = new GameObjectManager(this);
            tileManager = new TileManager();

            playerContentLoader = new PlayerContentLoader(Content);
            tileContentLoader = new TileContentLoader(Content);

            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 600);
            camera = new OrthographicCamera(viewportadapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerContentLoader.LoadContent();

            player = gameObjectManager.AddObject(new Player(new Vector2(10, 100), this));
            for (int i = 0; i <= 10; i++)
            {
                for (int u = 0; u <= 10; u++)
                {
                    tileManager.AddTile(new TestTile(i, u, tileManager, tileContentLoader));
                }
            }
            tileManager.AddTile(new TestTile(11, 10, tileManager, tileContentLoader));
            tileManager.AddTile(new TestTile(13, 10, tileManager, tileContentLoader));
            tileManager.AddTile(new TestTile(14, 10, tileManager, tileContentLoader));
            tileManager.AddTile(new TestTile(14, 0, tileManager, tileContentLoader));
            tileManager.AddTile(new TestTile(14, 1, tileManager, tileContentLoader));
            tileManager.AddTile(new TestTile(14, 2, tileManager, tileContentLoader));
            tileManager.AddTile(new TestTile(14, 5, tileManager, tileContentLoader));

            tileManager.UpdateAllTiles();
        }
        
        protected override void Update(GameTime gameTime)
        {
            gameObjectManager.Update(gameTime);
            tileManager.Update(gameTime);

            camera.LookAt(player.GetPosition());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            gameObjectManager.Draw(gameTime, _spriteBatch);
            tileManager.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}