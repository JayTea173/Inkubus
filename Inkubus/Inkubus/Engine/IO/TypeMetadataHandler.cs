using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine.IO
{


    class TypeMetadataHandler : Dictionary<Type, TypeMetadata>
    {
        public static TypeMetadataHandler instance;

        static TypeMetadataHandler()
        {
            instance = new TypeMetadataHandler();
        }
    }
}
