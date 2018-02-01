using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.IO;

namespace Inkubus.Engine.GameObjects
{
    using Graphics;
    using Graphics.Animation;
    using Graphics.Renderers;
    using Graphics.Shaders;
    using Physics;
    using Characters;
    using IO;


    [BPDClassMetadataAttribute(typeof(CharacterBlueprintMetadata))]
    class Character : Actor
    {
        protected SpriteRenderer renderer;

        public SpriteRenderer GetRenderer()
        {
            return renderer;
        }

        protected ActorMotor motor;

        [BPD(1)] public float MovementSpeed
        {
            get
            {
                return motor.movementSpeed;
            }
            set
            {
                motor.movementSpeed = value;
            }
        }

        [BPD(2)] public float TurnRate
        {
            get
            {
                return motor.turnRate;
            }
            set
            {
                motor.turnRate = value;
            }
        }



        

        public ActorMotor Motor
        {
            get
            {
                return motor;
            }
        }

        public virtual void SetupRenderer(string dataDir, string textureDir, int shaderId, int spriteSizeX, int spriteSizeY)
        {
            renderer = new SpriteRenderer(this, ShaderManager.Instance.GetShaderProgramById(shaderId), textureDir, spriteSizeX, spriteSizeY);
            renderer.SetAnimation(AnimationName.Idle);
        }

        public Character()
        {
            motor = new ActorMotor(this);
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

        public void OnAttackAnimationEnd(SpriteRenderer renderer)
        {
            renderer.SetAnimation(AnimationName.Idle, true);
        }

        public void Serialize(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.ASCII);

            bw.Write(MovementSpeed);

            bw.Close();
            fs.Close();
        }

        public void Deserialize(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs, Encoding.ASCII);

            MovementSpeed = br.ReadSingle();

            br.Close();
            fs.Close();
        }
    }


}
