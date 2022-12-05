using Microsoft.Xna.Framework.Content;
using Newtonsoft.Json;
using MonoGame.Extended.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Extraterrestrial.ContentLoaders.FileReaders
{
    public class MapContentReader
    {
        private JsonTextReader _reader;
        private TextReader textReader;

        public MapContentReader(ContentManager content)
        {
            _reader = new JsonTextReader(textReader);
        }

        public void Load(string path)
        {
            _reader.Read();
            _reader.Close();
        }

    }
}
