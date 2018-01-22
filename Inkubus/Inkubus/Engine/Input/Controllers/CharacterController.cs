using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace Inkubus.Engine.Input.Controllers
{
    using GameObjects;

    class CharacterController : Controller
    {
        private Character character;

        public CharacterController(Character _character)
        {
            character = _character;
        }

        public override void RegisterEventHandlers(InputManager inputManager)
        {
            base.RegisterEventHandlers(inputManager);
            inputManager.RegisterInputAxis(new AxisEvent(Key.S, Key.W, 1000.0f), character.Motor.MoveVertical);
            inputManager.RegisterInputAxis(new AxisEvent(Key.A, Key.D, 1000.0f), character.Motor.MoveHorizontal);
            /*inputManager.RegisterKeybinding(new KeyboardEvent(Key.W), character.Motor.MoveUp);
            inputManager.RegisterKeybinding(new KeyboardEvent(Key.S), character.Motor.MoveDown);
            inputManager.RegisterKeybinding(new KeyboardEvent(Key.A), character.Motor.MoveLeft);
            inputManager.RegisterKeybinding(new KeyboardEvent(Key.D), character.Motor.MoveRight);*/
        }

        public override void UnregisterEventHandlers(InputManager inputManager)
        {
            base.UnregisterEventHandlers(inputManager);
        }
    }
}
