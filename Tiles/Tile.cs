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
        public static int TILE_LENGHT = 16;

        public int X { get; protected set; }
        public int Y { get; protected set; }
        protected TileType type;
        protected TileManager TileManager;
        protected TileContentLoader TileContentLoader;
        protected string tileSlice;
        protected AsepriteDocument tileset;
        protected Texture2D texture;
        public Rectangle sourceRect;
        public Rectangle destinationRectangle;

        public Tile(int X, int Y, TileManager tileManager, TileContentLoader tileContentLoader)
        {
            this.X = X;
            this.Y = Y;
            TileManager = tileManager;
            TileContentLoader = tileContentLoader;
        }

        public abstract void Load();
        public virtual void SetTileType()
        {
            string final = "";

            if (TileManager.GetTileAt(X, Y - 1) == null) final += "top_";
            if (TileManager.GetTileAt(X, Y + 1) == null) final += "bottom_";
            if (TileManager.GetTileAt(X + 1, Y) == null) final += "right_";
            if (TileManager.GetTileAt(X - 1, Y) == null) final += "left_";
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

        public void DefaultDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            destinationRectangle = new Rectangle(X * TILE_LENGHT * Game1.SCALE, Y * TILE_LENGHT * Game1.SCALE, Game1.SCALE * TILE_LENGHT, Game1.SCALE * TILE_LENGHT);
            spriteBatch.Draw(texture, destinationRectangle, sourceRect, Color.White);
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public Rectangle GetDestinationRectangle()
        {
            return destinationRectangle;
        }

    }
}
