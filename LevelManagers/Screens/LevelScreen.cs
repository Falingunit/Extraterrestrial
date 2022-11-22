using Extraterrestrial.GameObjects;
using Extraterrestrial.Managers;
using Extraterrestrial.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Timers;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Extraterrestrial.LevelManagers.Screens
{
    public class LevelScreen : GameScreen {

        private new Game1 Game => (Game1)base.Game;
        private GameWindow Window;
        private GameObjectManager _gameObjectManager;
        private SpriteBatch _spriteBatch;
        private OrthographicCamera _camera;

        private LinkedList<Tile> Tiles = new LinkedList<Tile>();
        private LinkedList<GameObject> InitialGameObjects = new LinkedList<GameObject>();
        private int MapWidth;
        private int MapHeight;
        private GameObject Player;

        public LevelScreen(Game game, LinkedList<Tile> tiles, int mapWidth, int mapHeight, LinkedList<GameObject> gameObjects, GameObjectManager gameObjectManager, SpriteBatch _spriteBatch, GameWindow window, GameObject player) : base(game)
        {
            Tiles = tiles;
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            _gameObjectManager = gameObjectManager;
            Window = window;
            Player = player;
            InitialGameObjects = gameObjects;
        }

        public override void Initialize()
        {
            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            _camera = new OrthographicCamera(viewportadapter);
            _gameObjectManager.ClearAll();
            _gameObjectManager.SetObjects(InitialGameObjects);

            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {

            var transformMatrix = _camera.GetViewMatrix();
            _spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

            _gameObjectManager.Draw(gameTime, _spriteBatch);
            DrawAllTiles(gameTime);

            _spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            _gameObjectManager.Update(gameTime);
        }

        public void SetCameraPosition()
        {
            _camera.LookAt(Vector2.Lerp(_camera.Center, new Vector2(Player.GetPosition().X + 24, Player.GetPosition().Y + 24), 0.2f));
        }
        public void DrawAllTiles(GameTime gameTime)
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tile tile = Tiles.ElementAt(i);
                tile.Draw(gameTime, _spriteBatch);
            }
        }
        public Tile GetTileAt(int X, int Y)
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tile tile = Tiles.ElementAt(i);
                if (tile.X == X && tile.Y == Y)
                {
                    return tile;
                }
            }

            return null;
        }

    }
}
