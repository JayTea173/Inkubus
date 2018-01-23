using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Inkubus.Engine.GameObjects
{
    class Actor : GameObject, ITranslateable2D
    {
        protected Vector2 position;
        protected float angle;
        protected Vector2 scale;

        public ActorFlags flags;

        public float Angle
        {
            get
            {
                return angle;
            }
        }



        public Vector2 GetPosition()
        {
            return position;
        }

        public void SetPosition(Vector2 v)
        {
            position = v;
        }

        public void SetPosition(float x, float y)
        {
            position = new Vector2(x, y);
        }

        public void Translate(Vector2 v)
        {
            position += v;
        }

        public void Translate(float x, float y)
        {
            position += new Vector2(x, y);
        }

        public void AddFlag(ActorFlags flag)
        {
            flags |= flag;
        }

        public void RemoveFlag(ActorFlags flag)
        {
            flags &= flag;

        }

        public bool HasFlag(ActorFlags flag)
        {
            return (flags & flag) == flag;
        }
    }
}
