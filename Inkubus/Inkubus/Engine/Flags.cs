using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine
{
    [Flags]
    enum ActorFlags : int
    {
        None = 0,
        CantMove = 1,
        CantTurn = 2
    }
}
