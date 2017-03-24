using Assets.Scritps.Common;
using Assets.Scritps.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatrixController : MonoBehaviour , IMatrixController
{
    public enum Values : int { HeroVal = -1, RobotVal = -2, EmptyVal = 0, WallVal = 1, Lift = -8 }
    private List<int[,]> _data;
    public List<int[,]> Data
    {
        get
        {
            var res = new List<int[,]>();
            foreach (var item in _data)
            {
                var addition = new int[I, J];
                for (var i = 0; i < I; i++)
                    for (var j = 0; j < J; j++)
                        addition[i, j] = item[i, j];
                res.Add(addition);
            }
            return res;
        }
        set { _data = value; }
    }

    public int I { get; set; }
    public int J { get; set; }
    public void Start()
    {
        /* GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>().Index = GetIndex(-1);
        GameObject.FindGameObjectWithTag("Rob").GetComponent<Robots>().Index = GetIndex(-2);*/
    }

    public int GetValue(Vector3 index)
    {
        var result = 0;
        try
        {
            result = _data[(int) index.y][(int) index.x, (int) index.z];
        }
        catch (ArgumentOutOfRangeException e)
        {
            result = -666;
        }

        return result;
    }

    public void SetValueWithIndex(int value, Vector3 index)
    {
        _data[(int)index.y][(int)index.x, (int)index.z] = value;
    }

    public Vector3 GetIndex(int value)
    {
        for (var y = 0; y < _data.Count; y++)
        {
            for (var i = 0; i < I; i++)
                for (var j = 0; j < J; j++)
                {
                    if (_data[y][i, j] == value)
                        return new Vector3(i, y, j);
                }
        }
        return new Vector3();
    }

}