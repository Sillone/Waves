using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scritps.Common
{
    interface IRobot:IEntityBase
    {
        void DoWave(Vector2 hero, int I, int J, int[,] arra);
    }
}
