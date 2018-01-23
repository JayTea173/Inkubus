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

    using Graphics.Animation;

    class ActorMotor
    {
        protected Actor actor;
        protected Vector2 targetDir;
        protected Vector2 moveDir;
        protected Vector2 facing;

        public float movementSpeed = 1f;
        public float turnRate = 1f;




        public Vector2 MoveDir
        {
            get
            {
                return moveDir;
            }
        }

        public Vector2 Facing
        {
            get
            {
                return facing;
            }
        }

        public ActorMotor(Actor _actor)
        {
            actor = _actor;
            targetDir = Vector2.UnitY;
            facing = targetDir;

            moveDir = targetDir;
        }


        public void Move(float x, float y)
        {
            targetDir += Vector2.UnitX * x - Vector2.UnitY * y;
            moveDir = targetDir;
        }

        public void Update()
        {


            if (targetDir != Vector2.Zero)
            {
                targetDir.Normalize();

                facing.Normalize();

                float dot = Vector2.Dot(targetDir, new Vector2(-facing.Y, facing.X));
                if (dot == 0f && facing != targetDir)
                {
                    //this is when we look in the exact opposite direction we want to go - always turn right
                    dot = 1;
                }
                
                float angle = turnRate * Mathf.ToDeg * InkubusCore.deltaTime;
                float dotAbs = dot;

                if (dotAbs < 0f)
                    dotAbs = -dotAbs;

                if (dotAbs < 0.01 * turnRate * InkubusCore.deltaTime)
                {
                    facing = targetDir;
                }
                else {
                    if (dot < 0f)
                        angle *= -1f;


                    float sin = (float)Math.Sin(angle);
                    float cos = (float)Math.Cos(angle);

                    facing = new Vector2(facing.X * cos - facing.Y * sin, facing.X * sin + facing.Y * cos);
                }
            }
            else {
                moveDir = Vector2.Zero;
            }

            if (!actor.HasFlag(ActorFlags.CantMove))
                actor.Translate(moveDir * movementSpeed * InkubusCore.deltaTime);
            targetDir = Vector2.Zero;
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
