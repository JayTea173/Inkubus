using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Inkubus.Engine.Gfx.Animation
{

    using Renderers;

    delegate void AnimationEventFunction(SpriteRenderer renderer);


    class SpriteAnimationList
    {
        protected SpriteAnimation[] animations;

        public SpriteAnimationList(string textureDir, int spriteSizeX, int spriteSizeY)
        {
            animations = new SpriteAnimation[(int)AnimationName._COUNT];
            LoadFromDirectory(textureDir, spriteSizeX, spriteSizeY);

        }

        public void LoadFromDirectory(string dir, int spriteSizeX, int spriteSizeY)
        {
            string reldir = "..\\data\\textures" + dir;
            string dirName = dir.Replace("\\", string.Empty);
            string[] files = Directory.GetFiles(reldir);

            foreach (var file in files)
            {

                string fileNameNoExt = Path.GetFileNameWithoutExtension(file);
                string animationName = fileNameNoExt.Substring(fileNameNoExt.IndexOf(dirName) + dirName.Length + 1);

                AnimationName anim;
                if (System.Enum.TryParse<AnimationName>(animationName, out anim))
                
                    Set(new Sprite(file, spriteSizeX, spriteSizeY, 15.0f), anim);
                else
                
                    Console.WriteLine("Error parsing animationName: " + animationName + " from " + file + ". The file might not be named correctly.");
                
            }
            this.ToString();
            return;
            
        }



        public void Set(Sprite sprite, AnimationName animationName)
        {
            animations[(int)animationName] = new SpriteAnimation(sprite, animationName);
        }

        public SpriteAnimation Get(AnimationName animationName)
        {
            return animations[(int)animationName];
        }



    }



    class SpriteAnimation
    {
        public Sprite[] spriteSheetVariants;
        public AnimationName name;
        public ActorFlags flags;
        public bool loops = true;
        public bool playing = false;
        public int currentVariation = 0;
        //public event AnimationEventFunction onFinish;


        public SpriteAnimation(Sprite sprite, AnimationName animationName)
        {
            spriteSheetVariants = new Sprite[1];
            spriteSheetVariants[0] = sprite;
            name = animationName;
            loops = !(animationName == AnimationName.Attack || animationName == AnimationName.Death);
            

        }
   
        public SpriteAnimation(Sprite[] sprite, AnimationName animationName)
        {
            spriteSheetVariants = sprite;
            name = animationName;
            loops = !(animationName == AnimationName.Attack || animationName == AnimationName.Death);
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

        public int GetFrameByTime(float animationTime)
        {
            var sprite = spriteSheetVariants[currentVariation];

            if (loops)
                return (int)(animationTime * sprite.FPS) % (sprite.Frames);
            else {
                int frame = (int)(animationTime * sprite.FPS);
                if (frame >= sprite.Frames)
                {
                    frame = sprite.Frames - 1;
                }
                return frame;
            }
        }

        public bool IsDone(int frame)
        {
            return (frame+1) >= spriteSheetVariants[currentVariation].Frames;
        }
    }

    public enum AnimationName
    {
        Idle,
        Walk,
        Attack,
        Death,
        _COUNT
    }
}
