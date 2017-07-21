using MangaUpdatesCheck.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck
{
    public class SerializeResults : ISerializeResults
    {
        public IResults Serialize(string data)
        {
            return Serialize(Encoding.UTF8.GetBytes(data));
        }

        public IResults Serialize(byte[] data)
        {
            string sanitizedResponse = Encoding.UTF8.GetString(data);
            sanitizedResponse = sanitizedResponse.Remove(0, 11);
            sanitizedResponse = sanitizedResponse.Substring(0, sanitizedResponse.Length - 1);

            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(sanitizedResponse)))
            {
                Serialization.Results results = new Serialization.Results();

                DataContractJsonSerializer ser = new DataContractJsonSerializer(results.GetType());

                return (Serialization.Results)ser.ReadObject(ms);
            }
        }

        public void Dispose()
        {

        }
    }
}
