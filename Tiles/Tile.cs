using Extraterrestrial.ContentLoaders;
using Extraterrestrial.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraterrestrial.Tiles
{
    public abstract class Tile
    {
        protected enum TileType {
            top_normal = 0,
            top_left_normal,
            top_right_normal,
            bottom_normal,
            bottom_left_normal,
            bottom_right_normal,
            right_normal,
            left_normal,
            center_normal,
            top_bottom_normal,
            right_left_normal,
            top_bottom_right_normal,
            top_bottom_left_normal,
            top_right_left_normal,
            bottom_right_left_normal,
            top_bottom_right_left_normal
        }

        protected const int scale = 2;

        public int X { get; protected set; }
        public int Y { get; protected set; }
        protected TileType type;
        protected TileManager TileManager;
        protected TileContentLoader TileContentLoader;
        protected string tileSlice;
        protected AsepriteDocument tileset;
        protected Texture2D texture;
        protected Rectangle sourceRect;

        public Tile(int X, int Y, TileManager tileManager, TileContentLoader tileContentLoader)
        {
            this.X = X;
            this.Y = Y;
            TileManager = tileManager;
            TileContentLoader = tileContentLoader;
        }

        public abstract void Load();
        public abstract void UpdateTileType();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
