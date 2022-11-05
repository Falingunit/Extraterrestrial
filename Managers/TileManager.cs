using Extraterrestrial.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraterrestrial.Managers
{
    public class TileManager
    {

        private LinkedList<Tile> Tiles = new LinkedList<Tile>();

        public Tile AddTile(Tile tile)
        {
            Tiles.AddLast(tile);
            return tile;
        }

        public void UpdateAllTiles()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tile tile = Tiles.ElementAt(i);
                tile.UpdateTileType();
            }
        }

        public void Update(GameTime gameTime)
        {
            for(int i = 0; i < Tiles.Count; i++)
            {
                Tile tile = Tiles.ElementAt(i);
                tile.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tile tile = Tiles.ElementAt(i);
                tile.Draw(gameTime, spriteBatch);
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
