using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssestManagement
{
    public class AssetException : Exception
    {
        public AssetException(string message) : base(message) { }
    }
    public class InvalidDataException :AssetException
    {
        public InvalidDataException(string message) : base(message) { }
    }
}
