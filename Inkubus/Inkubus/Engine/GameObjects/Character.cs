﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Inkubus.Engine.GameObjects
{
    using Graphics;
    using Graphics.Animation;
    using Graphics.Renderers;
    using Graphics.Shaders;
    using Physics;



    class Character : Actor
    {
        protected SpriteRenderer renderer;

        public SpriteRenderer GetRenderer()
        {
            return renderer;
        }

        protected ActorMotor motor;

        protected float movementSpeed = 1f;



        

        public ActorMotor Motor
        {
            get
            {
                return motor;
            }
        }



        public Character(string textureDir, int shaderId, int spriteSizeX, int spriteSizeY)
        {
            renderer = new SpriteRenderer(this, ShaderManager.Instance.GetShaderProgramById(shaderId), textureDir, spriteSizeX, spriteSizeY);
            motor = new ActorMotor(this);


            renderer.SetAnimation(AnimationName.Idle);

        }

        public void Update()
        {


            renderer.Animate(motor.Facing, motor.MoveDir);

            motor.Update();
        }

        public void Render()
        {
            renderer.Render(this);
        }

        public override void Dispose()
        {
            base.Dispose();
            renderer.Dispose();

        }

        struct Stats
        {

        }

        enum StatNames
        {

        }

        public void Attack()
        {
            if (renderer.CurrentAnimation.name == AnimationName.Idle || renderer.CurrentAnimation.name == AnimationName.Walk)
            {
                renderer.SetAnimation(AnimationName.Attack, true);
                
            }
        }

        public void SetMovementSpeed(float pixelsPerSecond)
        {
            motor.movementSpeed = pixelsPerSecond;
        }

        public void SetTurnRate(float degreesPerSecond)
        {
            motor.turnRate = degreesPerSecond;
        }

        public void OnAttackAnimationEnd(SpriteRenderer renderer)
        {
            renderer.SetAnimation(AnimationName.Idle, true);
        }

    }


}
