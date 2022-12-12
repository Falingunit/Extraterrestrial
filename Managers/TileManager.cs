using Extraterrestrial.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Aseprite.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public void AddTiles(LinkedList<Tile> tiles)
        {
            Tiles = tiles;
        }

        public void UpdateAllTiles()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tile tile = Tiles.ElementAt(i);
                tile.SetTileType();
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

#nullable enable
        public List<Rectangle> GetColliders()
        {
            int scale = Game1.SCALE;
            List<Rectangle> colliders = new List<Rectangle>();
            for (int i = 0; i < Tiles.Count; i++)
            {
                Tile tile = Tiles.ElementAt(i);
                Rectangle rect = new Rectangle(tile.X * Tile.TILE_LENGHT * scale, tile.Y * Tile.TILE_LENGHT * scale, scale * Tile.TILE_LENGHT, scale * Tile.TILE_LENGHT);
                colliders.Add(rect);
            }

            return colliders;
        }

    }
}
