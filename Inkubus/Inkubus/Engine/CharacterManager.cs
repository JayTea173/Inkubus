using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine
{
    using GameObjects;
    class CharacterManager : List<Character>, IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            foreach (var chr in this)
            {
                chr.Dispose();
            }
        }

        public void Update()
        {
            foreach (var chr in this)
            {
                chr.Update();
            }
        }

        public void Render()
        {
            foreach (var chr in this)
            {
                chr.Render();
            }
        }
    }
}
