using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Inkubus.Engine
{
    using GameObjects;
    using GameObjects.Characters;
    using IO;
    using Graphics.Animation;

    class CharacterManager : List<Character>, IDisposable
    {
        const string charDir = "..\\data\\characters\\";

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                WriteBlueprints();
                foreach (var chr in this)
                {
                    chr.Dispose();
                }
            }
        }

        public void Update()
        {
            foreach (var chr in this)
            {
                chr.Update();
            }
        }

        public void Render()
        {
            foreach (var chr in this)
            {
                chr.Render();
            }
        }

        public void ReadBlueprints()
        {
            Console.WriteLine("Reading character blueprints");
            
            string[] dirs = Directory.GetDirectories(charDir);

            foreach (var subdir in dirs)
            {
                string dirName = Path.GetFileName(subdir);
                string bpPath = subdir + "\\" + dirName + ".bp";

                if (!File.Exists(bpPath))
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: Unable to locate " + dirName + ".bp in " + subdir);
                    continue;
                }
                Character chr = new Character();
                
                Blueprint.Read<Character>(ref chr, bpPath);

                CharacterBlueprintMetadata cbpmd = (CharacterBlueprintMetadata)Metadata.Get(chr);
                chr.SetupRenderer(cbpmd.bpPath, cbpmd.sprPath, 0, 192, 192);

                var r = chr.GetRenderer();
                r.onAnimationDone += chr.OnAttackAnimationEnd;
                r.Animations.Get(AnimationName.Attack).AddFlag(ActorFlags.CantMove);
                Add(chr);
            }
            


            /*
            Character chr = new Character("Hunter", "Hunter", 0, 192, 192);
            chr.Name = "Hunter";
            //Blueprint.Read<Character>(ref chr, bpPath);

            var r = chr.GetRenderer();
            r.onAnimationDone += chr.OnAttackAnimationEnd;
            r.Animations.Get(AnimationName.Attack).AddFlag(ActorFlags.CantMove);

            chr.SetMovementSpeed(28.0f);
            chr.TurnRate = 270.0f;
            
            Add(chr);*/
        }

        public void WriteBlueprints()
        {
            Blueprint.Write<Character>(this[0], "..\\data\\characters\\Hunter\\hunter.bp");
        }
    }
}
