using Graduation.Entities;
using Graduation.TestMap;
using System.Diagnostics;

namespace Graduation
{
    class BossLevelOneController
    {
        private BossLevelOne _bossLevelOne;

        public BossLevelOneController(BossLevelOne bossLevelOne)
        {
            _bossLevelOne = bossLevelOne;
        }

        public void attackPatterns(float timer, Player player, Map map)
        {
            if ((int)timer < 0.5)
            {
                _bossLevelOne.moveLeft(map);
            }

            if ((int)timer > 0.5 && (int)timer < 2)
            {
                _bossLevelOne.moveRight(map);
            }

            if ((int)timer > 4 && (int)timer < 20)
            {
                _bossLevelOne.chase(player, map);
            }

            if ((int)timer > 20 && (int)timer < 22)
            {
                _bossLevelOne.moveLeft(map);
            }
        }

        public void handleAttackPatterns(float timer, Player player, Map map)
        {
            if (_bossLevelOne.Position.Y != player.Position.Y)
            {
                return;
            }
            else
            {
                attackPatterns(timer, player, map);
            }
        }
    }
}
