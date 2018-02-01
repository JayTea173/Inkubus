using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine.GameObjects.Characters
{
    using IO;

    class CharacterBlueprintMetadata : Metadata
    {
        [BPD()] public string bpPath;
        [BPD()] public string sprPath;

        public CharacterBlueprintMetadata(string _bpName)
        {
            bpPath = _bpName;
            sprPath = _bpName;
        }
        public CharacterBlueprintMetadata()
        {
        }

        }
}
