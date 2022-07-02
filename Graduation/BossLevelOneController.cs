using Graduation.Entities;
using Graduation.TestMap;

namespace Graduation
{
    class BossLevelOneController
    {
        private BossLevelOne _bossLevelOne;

        public BossLevelOneController(BossLevelOne bossLevelOne)
        {
            _bossLevelOne = bossLevelOne;
        }

        public void handleAttackPatterns(float timer, Map map)
        {
            if(timer % 3 == 0)
            {
                _bossLevelOne.dashAttack(map);
            }
        }
    }
}
