using Extraterrestrial.ContentLoaders;
using Extraterrestrial.LevelManagers.Screens;
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

        public TestTile(int X, int Y, LevelScreen currentScreen, TileContentLoader tileContentLoader) : base(X, Y, currentScreen, tileContentLoader)
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
    }
}
