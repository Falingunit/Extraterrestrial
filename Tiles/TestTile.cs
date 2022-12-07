using Extraterrestrial.ContentLoaders;
using Extraterrestrial.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Documents;
using MonoGame.Extended;
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
            DefaultDraw(gameTime, spriteBatch);
        }

        public override void Load()
        {
            tileset = TileContentLoader.TestTileset;
            texture = tileset.Texture;
        }
    }
}
