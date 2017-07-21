using MangaUpdatesCheck.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaUpdatesCheck
{
    public interface ISerializeResults : IDisposable
    {
        IResults Serialize(string data);
        IResults Serialize(byte[] data);
    }
}
