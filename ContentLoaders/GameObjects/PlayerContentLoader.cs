using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Aseprite.Documents;
using Microsoft.Xna.Framework.Content;

namespace Extraterrestrial.ContentLoaders.GameObjects
{
    public class PlayerContentLoader
    {
        private ContentManager contentManager;
        public static AsepriteDocument playerSprite;

        public string playerSpritePath = "Player/player";

        public PlayerContentLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public void LoadContent()
        {
            playerSprite = contentManager.Load<AsepriteDocument>(playerSpritePath);
        }

        public static AsepriteDocument getPlayerSprite()
        {
            return playerSprite;
        }

    }
}
