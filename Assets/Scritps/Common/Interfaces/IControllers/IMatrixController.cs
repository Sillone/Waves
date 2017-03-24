using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scritps.Common.Interfaces
{
    public interface IMatrixController:IController
    {
        List<int[,]> Data { get; set; }
        int I { get; set; }
        int J { get; set; }
        int GetValue(Vector3 index);

        void SetValueWithIndex(int value, Vector3 index);
        Vector3 GetIndex(int value);

    }
}
