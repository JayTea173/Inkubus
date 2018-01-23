using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Inkubus.Engine.GameObjects
{
    interface ITranslateable2D
    {
        void SetPosition(Vector2 v);
        void SetPosition(float x, float y);

        void Translate(Vector2 v);
        void Translate(float x, float y);

        Vector2 GetPosition();



    }
}
