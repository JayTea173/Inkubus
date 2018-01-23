using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine
{
    class Object : IDisposable
    {
        public string Name
        {
            get
            {
                return string.Empty;
            }
            set
            {

            }
        }

        public virtual void Dispose()
        {

        }
    }
}
