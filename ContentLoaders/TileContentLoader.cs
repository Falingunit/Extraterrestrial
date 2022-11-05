using Microsoft.Xna.Framework.Content;
using MonoGame.Aseprite.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraterrestrial.ContentLoaders
{
    public class TileContentLoader
    {
        private ContentManager Content;

        private const string TestTilesetPath = "Tilesets/terrain";
        public AsepriteDocument TestTileset { get; private set; }

        public TileContentLoader(ContentManager content)
        {
            Content = content;
        }

        public void LoadContent()
        {
            TestTileset = Content.Load<AsepriteDocument>(TestTilesetPath);
        }

    }
}
