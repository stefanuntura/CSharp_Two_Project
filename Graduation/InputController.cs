using Graduation.Entities;
using Microsoft.Xna.Framework.Input;
using Graduation.TestMap;
using Graduation.States;

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

        public void handleInput(Map map, State gs)
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
                    case Keys.K:
                        _player.throwWeapon();
                        break;
                    case Keys.D1:
                        _player.switchWeapon(1);
                        break;
                    case Keys.D2:
                        _player.switchWeapon(2);
                        break;
                    case Keys.D3:
                        _player.switchWeapon(3);
                        break;
                    case Keys.Escape:
                        if (gs.GetType() != typeof(Lobby))
                        {
                            _player.Restart();
                        }
                        break;
                }
            }
        }
    }
}