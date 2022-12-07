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
        public static int SCALE = 3;
        public static float CAMERA_SMOOTHING = 0.5f;

        private GraphicsDeviceManager _graphics;   
        private SpriteBatch _spriteBatch;

        //Managers
        /*
         * Managers are classes that handle the multiple objects that has methodslike Update() and Draw() to be called.
         * - GameObjectManager handles all of the gameobjects
         * - TileManager handles all the tiles
         */
        private GameObjectManager _gameObjectManager;
        private TileManager _tileManager;

        //Content Loaders
        /*
         * Content Loaders are classes which load content. They are needed because we would have to load the content
         * every time a new instance of an object is made, which is bad for performance and isn'
         */
        private PlayerContentLoader playerContentLoader;
        private TileContentLoader tileContentLoader;

        private OrthographicCamera _camera;
        private GameObject player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            Window.BeginScreenDeviceChange(false);
            Window.Title = "Extraterrestrial -Map-loading -Beta 0.2.2!";
            Window.EndScreenDeviceChange(Window.Title, 800, 480);
        }

        protected override void Initialize()
        {
            _graphics.ApplyChanges();

            _gameObjectManager = new GameObjectManager();
            _tileManager = new TileManager();

            playerContentLoader = new PlayerContentLoader(Content);
            tileContentLoader = new TileContentLoader(Content);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Window.BeginScreenDeviceChange(false);
            Window.Title = "Extraterrestrial -Map-loading";
            Window.EndScreenDeviceChange(Window.Title, 1920, 1080);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 960, 540);
            _camera = new OrthographicCamera(viewportadapter);

            playerContentLoader.LoadContent();
            tileContentLoader.LoadContent();

            //for (int i = 0; i <= 10; i++)
            //{
            //    for (int u = 0; u <= 10; u++)
            //    {
            //        tileManager.AddTile(new TestTile(i+1, u+1, tileManager, tileContentLoader));
            //    }
            //}
            _tileManager.AddTile(new TestTile(12, 11, _tileManager, tileContentLoader));
            _tileManager.AddTile(new TestTile(0, 10, _tileManager, tileContentLoader));
            _tileManager.AddTile(new TestTile(20, 10, _tileManager, tileContentLoader));
            _tileManager.AddTile(new TestTile(14, 0, _tileManager, tileContentLoader));
            _tileManager.AddTile(new TestTile(14, 1, _tileManager, tileContentLoader));
            _tileManager.AddTile(new TestTile(14, 2, _tileManager, tileContentLoader));
            _tileManager.AddTile(new TestTile(14, 5, _tileManager, tileContentLoader));

            _tileManager.UpdateAllTiles();

            player = new Player(new Vector2(-100, 100), this, _tileManager.GetColliders());

            _gameObjectManager.AddObject(player);
        }
        
        protected override void Update(GameTime gameTime)
        {
            _gameObjectManager.Update(gameTime);

            _camera.LookAt(Vector2.Lerp(_camera.Center, player.GetCenteredPosition(), CAMERA_SMOOTHING));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Camera affected, no-parralax sprite batch
            var transformMatrix = _camera.GetViewMatrix();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);

            _gameObjectManager.Draw(gameTime, _spriteBatch);
            _tileManager.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }



    }
}