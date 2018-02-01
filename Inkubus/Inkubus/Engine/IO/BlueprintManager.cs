using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine.IO
{
    public class BlueprintManager
    {
        public List<BlueprintEntry> entries;

        public static BlueprintManager instance;

        public BlueprintManager()
        {
            entries = new List<BlueprintEntry>();

            instance = this;
        }

    }

    public class BlueprintEntry
    {
        public List<Object> instances;
        public BlueprintEntry meta;
    }

}
