using Extraterrestrial.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Extraterrestrial.LevelManagers
{
    public class Map
    {
        public LinkedList<Tile> Tiles = new LinkedList<Tile>();
        public List<Vector2> PlayerSpawnPoints = new List<Vector2>();

        public Map(LinkedList<Tile> tiles)
        {
            Tiles = tiles;
        }

    }
}
