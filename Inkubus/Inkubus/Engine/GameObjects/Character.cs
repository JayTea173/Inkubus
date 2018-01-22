using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkubus.Engine.GameObjects
{
    using Graphics;
    using Graphics.Renderers;
    using Graphics.Shaders;
    using Physics;



    class Character : Actor
    {
        protected SpriteRenderer renderer;
        protected ActorMotor motor;

        public ActorMotor Motor
        {
            get
            {
                return motor;
            }
        }

        public Character(Sprite sprite, ShaderProgram shader)
        {
            renderer = new SpriteRenderer(sprite, shader);
            motor = new ActorMotor(this);
        }


        public Character(Sprite sprite, int shaderId)
        {
            renderer = new SpriteRenderer(sprite, ShaderManager.Instance.GetShaderProgramById(shaderId));
            motor = new ActorMotor(this);
        }

        public void Update()
        {
            renderer.Animate(motor.MoveDir);
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
    }

 
}
