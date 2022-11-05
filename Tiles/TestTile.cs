using Extraterrestrial.ContentLoaders;
using Extraterrestrial.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Documents;
using MonoGame.Extended.Collections;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraterrestrial.Tiles
{
    public class TestTile : Tile
    {

        public TestTile(int X, int Y, TileManager tileManager, TileContentLoader tileContentLoader) : base(X, Y, tileManager, tileContentLoader)
        {
            Load();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(X * 16 * scale, Y * 16 * scale, scale * 16, scale * 16), sourceRect, Color.White);
        }

        public override void Load()
        {
            tileset = TileContentLoader.TestTileset;
            texture = tileset.Texture;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void UpdateTileType()
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
    }
}
