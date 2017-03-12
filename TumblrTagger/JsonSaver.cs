using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TumblrTagger
{
    public class JsonSaver
    {
        public void Save(object content, string fileDir)
        {
            var json = JsonConvert.SerializeObject(content);

            File.WriteAllText(fileDir, json.ToString());
        }
    }
}
