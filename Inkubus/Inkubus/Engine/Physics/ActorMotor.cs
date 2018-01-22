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
        protected Vector2 targetMoveDir;
        protected Vector2 moveDir; 

        public float movementSpeed = 1f;



        public Vector2 MoveDir
        {
            get
            {
                return targetMoveDir;
            }
        }

        public ActorMotor(Actor _actor)
        {
            actor = _actor;
            targetMoveDir = Vector2.Zero;
        }


        public void Move(float x, float y)
        {
            targetMoveDir += Vector2.UnitX * x - Vector2.UnitY * y;
        }

        public void Update()
        {
            if (targetMoveDir != Vector2.Zero) {
                targetMoveDir.Normalize();

                float lerp = InkubusCore.deltaTime * 50f;
                if (lerp > 1f)
                    lerp = 1f;
                moveDir = moveDir * lerp + targetMoveDir * (1f - lerp);
                moveDir.Normalize();
            } else {
                moveDir = Vector2.Zero;
            }
            actor.Translate(moveDir * movementSpeed * InkubusCore.deltaTime);
            targetMoveDir = Vector2.Zero;
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
