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

        public void handleAttackPatterns(double gravity, float timer, Map map)
        {
            bool attackPatternOne = false;
            bool attackPatternTwo = false;
            bool attackPatternThree = false;

            if(gravity == 0)
            {
                if ((int)timer % 2 == 0)
                {
                    _bossLevelOne.dashRight(map);
                    attackPatternOne = true;
                }
                else
                {
                    _bossLevelOne.dashLeft(map);
                    attackPatternOne = true;
                }
            }


            //if (attackPatternOne)
            //{
            //    _bossLevelOne.idle(map);
            //}
        }
    }
}
