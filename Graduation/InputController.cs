using Graduation.Entities;
using Microsoft.Xna.Framework.Input;
using Graduation.TestMap;

namespace Graduation
{
    class InputController
    {
        private Player _player;
        private KeyboardState _kState;

        public InputController(Player player)
        {
            _player = player;
        }

        public void handleInput(Map map)
        {
            _kState = Keyboard.GetState(); ;
            Keys[] keys = _kState.GetPressedKeys();

            foreach (Keys key in keys)
            {
                switch (key)
                {
                    case Keys.D:
                        _player.moveRight(map);
                        break;
                    case Keys.A:
                        _player.moveLeft(map);
                        break;
                    case Keys.Space:
                        _player.jump();
                        break;
                }
            }
        }
    }
}