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
                _bossLevelOne.dashLeft(map);
            }

            if ((int)timer > 0.5 && (int)timer < 2)
            {
                _bossLevelOne.dashRight(map);
            }

            if((int) timer > 2 && (int)timer < 2.5)
            {
                _bossLevelOne.idle(map);
            }

            if ((int)timer > 4 && (int)timer < 20)
            {
                _bossLevelOne.chase(player, map);
            }

            if ((int)timer > 20 && (int)timer < 22)
            {
                _bossLevelOne.dashLeft(map);
            }

            if ((int)timer > 22 && (int)timer < 25)
            {
                _bossLevelOne.dashRight(map);
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
