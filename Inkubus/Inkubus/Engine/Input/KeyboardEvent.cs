using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace Inkubus.Engine.Input
{
    public class KeyboardEvent : IEquatable<KeyboardEvent>
    {
        Key key;
        bool alt;
        bool ctrl;
        bool shift;

        public KeyboardEvent(Key _key, bool _shift = false, bool _ctrl = false, bool _alt = false)
        {
            key = _key;
            alt = _alt;
            ctrl = _ctrl;
            shift = _shift;
        }

        public KeyboardEvent(KeyboardKeyEventArgs e)
        {
            key = e.Key;
            alt = e.Alt;
            ctrl = e.Control;
            shift = e.Shift;
        }

        public override int GetHashCode()
        {
            return (int)key + ((shift) ? 0xF1 : 0) + ((ctrl) ? 0xF2 : 0) + ((alt) ? 0xF3 : 0);
        }

        public override bool Equals(object obj)
        {
            if (obj is KeyboardEvent)
                return this.Equals(obj as KeyboardEvent);
            else if (obj is KeyboardKeyEventArgs)
                return this.Equals(obj as KeyboardKeyEventArgs);
            else
                return false;
        }

        public bool Equals(KeyboardEvent evnt)
        {
            return (key == evnt.key && alt == evnt.alt && shift == evnt.shift && ctrl == evnt.ctrl);
        }

        public bool Equals(KeyboardKeyEventArgs evnt)
        {
            return (key == evnt.Key && alt == evnt.Alt && shift == evnt.Shift && ctrl == evnt.Control);
        }
    }
}
