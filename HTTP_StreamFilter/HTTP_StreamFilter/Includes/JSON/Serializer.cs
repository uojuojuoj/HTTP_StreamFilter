using Newtonsoft.Json;
using System.Collections.Generic;

namespace HTTP_StreamFilter.JSON
{
    public class Serializer
    {
        public string SerializeToString(Model model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public Model SerializeToObj(string target)
        {
            return JsonConvert.DeserializeObject<Model>(target);
        }


    }
}
