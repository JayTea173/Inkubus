using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace Inkubus.Engine.Input
{
    public class AxisEvent : IEquatable<AxisEvent>
    {
        protected Key negative, positive;
        protected float sensitivity;

        protected float value = 0.0f;

        public float Value
        {
            get
            {
                return value;
            }
        }

        public AxisEvent(Key _negative, Key _positive, float _sensitivity)
        {
            negative = _negative;
            positive = _positive;
            sensitivity = _sensitivity;
        }


        public override bool Equals(object obj)
        {
            if (obj is KeyboardEvent)
                return this.Equals(obj as KeyboardEvent);
            else if (obj is KeyboardKeyEventArgs)
                return this.Equals(obj as KeyboardKeyEventArgs);
            else if (obj is AxisEvent)
                return this.Equals(obj as AxisEvent);
            else
                return false;
        }

        public bool Equals(Key k)
        {
            return negative == k || positive == k;
        }

        public bool Equals(AxisEvent evnt)
        {
            return (negative == evnt.negative || positive == evnt.positive);
        }

        public bool Equals(KeyboardKeyEventArgs evnt)
        {
            return (negative == evnt.Key || positive == evnt.Key);
        }

        public override int GetHashCode()
        {
            return (int)negative.GetHashCode();
        }

        public float Handle(KeyboardKeyEventArgs e)
        {

            if (e.Key == negative)
            {
                value -= sensitivity;
                if (value < -1.0f)
                    value = -1.0f;
                return value;
            }

            if (e.Key == positive)
            {
                value += sensitivity;
                if (value > 1.0f)
                    value = 1.0f;
                return value;
            }
            return 0.0f;


        }

    }
}
