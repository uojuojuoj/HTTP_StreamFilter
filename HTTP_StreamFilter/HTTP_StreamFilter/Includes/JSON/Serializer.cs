using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace HTTP_StreamFilter.JSON
{
    public class Serializer
    {
        private JsonSerializer jsonSerializer = new JsonSerializer();
        private List<Model> model = new List<Model>();

        public void Serialize(string outDir, List<Model> model)
        {
            using (StreamWriter sw = new StreamWriter(outDir))
            using (JsonWriter writer = new JsonTextWriter(sw))
                jsonSerializer.Serialize(writer, model);
        }


    }
}
