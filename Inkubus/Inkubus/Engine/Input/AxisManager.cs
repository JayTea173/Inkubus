using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace Inkubus.Engine.Input
{
    class AxisManager
    {
        List<AxisEvent> axis;
        List<AxisEventFunction> callback;

        public AxisManager()
        {
            axis = new List<AxisEvent>();
            callback = new List<AxisEventFunction>();
        }

        public void Register(AxisEvent evnt, AxisEventFunction _callback)
        {
            int index = axis.IndexOf(evnt);
            if (index >= 0)
            {
                axis.RemoveAt(index);
                callback.RemoveAt(index);
            }

            axis.Add(evnt);
            callback.Add(_callback);


        }

        public List<int> Handle(KeyboardKeyEventArgs e)
        {
            List<int> events = new List<int>();
            for (int i = 0; i < axis.Count; i++)
            {
                float f = axis[i].Handle(e);
                if (f != 0.0f)
                {
                    events.Add(i);
                }

            }

            return events;
        }

        public void ExecuteById(int id)
        {
            callback[id].Invoke(axis[id].Value);
        }
    }
}
