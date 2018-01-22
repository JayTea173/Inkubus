using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Inkubus.Engine.Physics
{
    using GameObjects;

    class ActorMotor
    {
        protected Actor actor;
        protected Vector2 moveDir;

        public Vector2 MoveDir
        {
            get
            {
                return moveDir;
            }
        }

        public ActorMotor(Actor _actor)
        {
            actor = _actor;
            moveDir = Vector2.Zero;
        }


        public void Move(float x, float y)
        {
            moveDir += Vector2.UnitX * x - Vector2.UnitY * y;
        }

        public void Update()
        {
            if (moveDir != Vector2.Zero)
                moveDir.Normalize();
            actor.Translate(moveDir);
            moveDir = Vector2.Zero;
        }
 

        public void MoveVertical(float v)
        {
            Move(0f, v);
        }

        public void MoveHorizontal(float v)
        {
            Move(v, 0f);
        }

        public void MoveUp()
        {
            Move(0f, 1f);
        }

        public void MoveDown()
        {
            Move(0f, -1f);
        }

        public void MoveLeft()
        {
            Move(-1f, 0f);
        }

        public void MoveRight()
        {
            Move(1f, 0f);
        }
    }
}
