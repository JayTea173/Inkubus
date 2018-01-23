using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK;

namespace Inkubus.Engine.Input.Controllers
{
    using GameObjects;
    using Graphics;

    class CharacterController : Controller
    {
        private Character character;
        private Camera camera;

        public CharacterController(Character _character, Camera _camera)
        {
            character = _character;
            camera = _camera;
        }

        public override void RegisterEventHandlers(InputManager inputManager)
        {
            base.RegisterEventHandlers(inputManager);
            inputManager.RegisterInputAxis(new AxisEvent(Key.S, Key.W, 1000.0f), character.Motor.MoveVertical);
            inputManager.RegisterInputAxis(new AxisEvent(Key.A, Key.D, 1000.0f), character.Motor.MoveHorizontal);

            inputManager.RegisterKeybinding(new KeyboardEvent(Key.Space), character.Attack);

            /*inputManager.RegisterKeybinding(new KeyboardEvent(Key.W), character.Motor.MoveUp);
            inputManager.RegisterKeybinding(new KeyboardEvent(Key.S), character.Motor.MoveDown);
            inputManager.RegisterKeybinding(new KeyboardEvent(Key.A), character.Motor.MoveLeft);
            inputManager.RegisterKeybinding(new KeyboardEvent(Key.D), character.Motor.MoveRight);*/
        }

        public void Update()
        {
            Vector2 pos = Camera.current.GetPosition();
            Vector2 newPos = character.GetPosition();
            /*float lerp = InkubusCore.deltaTime * 50f;

            Vector2 interpolPos = new Vector2(pos.X * lerp + (1f-lerp) * newPos.X,
                pos.Y * lerp + (1f - lerp) * newPos.Y);*/

            var interpolPos = new Vector2((float)Math.Round(newPos.X * 2f), (float)Math.Round(newPos.Y * 2f));
            interpolPos *= 0.5f;
            Camera.current.SetPosition(interpolPos);
        }

        public override void UnregisterEventHandlers(InputManager inputManager)
        {
            base.UnregisterEventHandlers(inputManager);
        }
    }
}
