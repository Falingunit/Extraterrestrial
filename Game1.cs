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
using Microsoft.Xna.Framework.Input;

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

        private OrthographicCamera camera;
        private GameObject player;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            _graphics.IsFullScreen = true;
        }

        protected override void Initialize()
        {
            _graphics.ApplyChanges();

            gameObjectManager = new GameObjectManager(this);
            tileManager = new TileManager();

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
            camera = new OrthographicCamera(viewportadapter);

            playerContentLoader.LoadContent();
            tileContentLoader.LoadContent();

            player = new Player(new Vector2(10, 100), this);
            gameObjectManager.AddObject(player);
            for (int i = 0; i <= 10; i++)
            {
                for (int u = 0; u <= 10; u++)
                {
                    tileManager.AddTile(new TestTile(i+1, u+1, tileManager, tileContentLoader));
                }
            }
            tileManager.AddTile(new TestTile(12, 11, tileManager, tileContentLoader));
            tileManager.AddTile(new TestTile(0, 10, tileManager, tileContentLoader));
            tileManager.AddTile(new TestTile(20, 10, tileManager, tileContentLoader));
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

            camera.LookAt(Vector2.Lerp(camera.Center, new Vector2(player.GetPosition().X+24, player.GetPosition().Y+24), 0.2f));

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Camera affected, no-parralax sprite batch
            var transformMatrix = camera.GetViewMatrix();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);

            gameObjectManager.Draw(gameTime, _spriteBatch);
            tileManager.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }



    }
}