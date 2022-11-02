using System;
using Extraterrestrial.ContentLoaders.GameObjects;
using Extraterrestrial.GameObjects;
using Extraterrestrial.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite.Documents;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;

namespace Extraterrestrial
{
    public class Game1 : Game
    {
        public static Vector2 GRAVITY = new Vector2(0, 1f);

        private GraphicsDeviceManager _graphics;   
        private SpriteBatch _spriteBatch;

        private GameObjectManager gameObjectManager;

        private PlayerContentLoader playerContentLoader;

        //temporary
        private TiledMap tiledMap;
        private TiledMapRenderer tiledMapRenderer;
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

            playerContentLoader = new PlayerContentLoader(Content);

            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 600);
            camera = new OrthographicCamera(viewportadapter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            playerContentLoader.LoadContent();

            tiledMap = Content.Load<TiledMap>("Maps/1");
            tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, tiledMap);

            player = gameObjectManager.AddObject(new Player(new Vector2(10, 100), this));
        }
        
        protected override void Update(GameTime gameTime)
        {
            gameObjectManager.Update(gameTime);

            tiledMapRenderer.Update(gameTime);

            camera.LookAt(player.GetPosition());

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            gameObjectManager.Draw(gameTime, _spriteBatch);

            tiledMapRenderer.Draw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}