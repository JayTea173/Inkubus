using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Inkubus.Engine.Graphics.Animation
{
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
            string[] files = Directory.GetFiles("..\\data\\textures\\" + dir);

            foreach (var file in files)
            {

                string fileNameNoExt = Path.GetFileNameWithoutExtension(file);
                string animationName = fileNameNoExt.Replace(dir + "_", string.Empty);

                AnimationName anim;
                if (System.Enum.TryParse(animationName, out anim))
                {
                    Set(new Sprite(file, spriteSizeX, spriteSizeY, 15.0f), anim);
                }
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
        public int animationFlags;
        public bool loops = true;

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
