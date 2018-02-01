using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine.IO
{


    class MetadataHandler : Dictionary<Object, Metadata>
    {
        public static MetadataHandler instance;

        static MetadataHandler()
        {
            instance = new MetadataHandler();
        }
    }
}
