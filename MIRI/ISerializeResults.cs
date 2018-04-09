using MIRI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIRI
{
    public interface ISerializeResults : IDisposable
    {
        IResults Serialize(string data);
        IResults Serialize(byte[] data);
    }
}
