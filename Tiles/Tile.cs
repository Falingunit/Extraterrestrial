using Extraterrestrial.ContentLoaders;
using Extraterrestrial.LevelManagers.Screens;
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
        protected LevelScreen Screen;
        protected TileContentLoader TileContentLoader;
        protected string tileSlice;
        protected AsepriteDocument tileset;
        protected Texture2D texture;
        protected Rectangle sourceRect;

        public Tile(int X, int Y, LevelScreen currentScreen, TileContentLoader tileContentLoader)
        {
            this.X = X;
            this.Y = Y;
            Screen = currentScreen;
            TileContentLoader = tileContentLoader;
        }

        public abstract void Load();
        public virtual void SetTileType()
        {
            string final = "";

            if (Screen.GetTileAt(X, Y - 1) == null) final += "top_";
            if (Screen.GetTileAt(X, Y + 1) == null) final += "bottom_";
            if (Screen.GetTileAt(X + 1, Y) == null) final += "right_";
            if (Screen.GetTileAt(X - 1, Y) == null) final += "left_";
            if (final == "") final = "center_";

            final += "normal";

            Console.WriteLine(final);

            tileSlice = final;
            for (int i = 0; i <= 15; i++)
            {
                type = (TileType)i;
                if (tileSlice.Equals(type)) break;
            }

            AsepriteSliceKey slice = tileset.Slices.GetValueOrDefault(tileSlice).SliceKeys.GetValueOrDefault(0);
            int x, y, width, height;
            x = slice.X;
            y = slice.Y;
            width = slice.Width;
            height = slice.Height;

            sourceRect = new Rectangle(x, y, width, height);
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    }
}
