using Extraterrestrial.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extraterrestrial.Managers
{
    public class GameObjectManager
    {

        private LinkedList<GameObject> GameObjects = new LinkedList<GameObject>();

        public GameObjectManager()
        {
        }

        public GameObject AddObject(GameObject gameObject)
        {
            gameObject.Load();
            gameObject.Initialize();

            GameObjects.AddLast(gameObject);
            return gameObject;
        }

        public void SetObjects(LinkedList<GameObject> gameObjects)
        {
            GameObjects = gameObjects;
        }

        public void RemoveObject(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
        }

        public void RemoveObject(int index)
        {
            GameObjects.Remove(GameObjects.ElementAt(index));
        }

        public GameObject GetObject(int index)
        {
            return GameObjects.ElementAt(index);
        }

        public void ClearAll()
        {
            GameObjects.Clear();
        }
        
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject tempObject = GameObjects.ElementAt(i);

                tempObject.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObject tempObject = GameObjects.ElementAt(i);

                tempObject.Draw(gameTime, _spriteBatch);
            }
        }
    }
}
