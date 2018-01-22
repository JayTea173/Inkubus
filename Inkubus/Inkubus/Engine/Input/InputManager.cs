using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace Inkubus.Engine.Input
{
    public delegate void KeyboardEventFunction();
    public delegate void AxisEventFunction(float value);

    class InputManager
    {
        Dictionary<KeyboardEvent, KeyboardEventFunction> keyBindings;

        AxisManager axisManager;

        List<KeyboardEvent> activeKeys;
        List<int> activeAxis;


        public InputManager()
        {
            keyBindings = new Dictionary<KeyboardEvent, KeyboardEventFunction>();
            axisManager = new AxisManager();
            activeKeys = new List<KeyboardEvent>();
            activeAxis = new List<int>();
        }


        public void RegisterKeybinding(KeyboardEvent evnt, KeyboardEventFunction callback)
        {
            KeyboardEventFunction cb;
            if (keyBindings.TryGetValue(evnt, out cb))
                cb += callback;
            else
                keyBindings.Add(evnt, callback);
        }


        public void RegisterInputAxis(AxisEvent evnt, AxisEventFunction callback)
        {
            axisManager.Register(evnt, callback);
        }

        public void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.IsRepeat)
                return;
            KeyboardEventFunction callbacks = null;
            var evnt = new KeyboardEvent(e);
            keyBindings.TryGetValue(evnt, out callbacks);
            
            if (callbacks != null)
            {
                activeKeys.Add(evnt);
            }
            var newAxis = axisManager.Handle(e);
            foreach (var a in newAxis)
                if (!activeAxis.Contains(a))
                    activeAxis.Add(a);
            
        }

        public void OnKeyUp(KeyboardKeyEventArgs e)
        {
            activeKeys.Remove(new KeyboardEvent(e));

            var removeAxis = axisManager.Handle(e);
            while (removeAxis.Count > 0)
            {
                activeAxis.Remove(removeAxis[0]);
                removeAxis.RemoveAt(0);
            }
        }

        public void DigestAll()
        {
            foreach (var k in activeKeys)
            {

                KeyboardEventFunction callbacks = null;
                keyBindings.TryGetValue(k, out callbacks);
                if (callbacks != null)
                    callbacks.Invoke();
             
            }

            foreach (var a in activeAxis)
            {
                axisManager.ExecuteById(a);
            }
        }

    }
}
