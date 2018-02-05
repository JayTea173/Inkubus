using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inkubus.Engine.GameObjects;
using OpenTK;

namespace Inkubus.Engine.Gfx.Renderers
{
    class Renderer : IDisposable
    {
        protected Vector3 scale;

        public Vector3 Scale
        {
            get
            {
                return scale;
            }
        }

        public virtual void Render(Actor actor)
        {

        }

        public virtual void Dispose()
        {

        }

    }
}
