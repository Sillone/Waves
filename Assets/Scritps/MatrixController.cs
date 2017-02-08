using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixController : MonoBehaviour
{
    private LinkedList<int[,]> _data;
    public LinkedList<int[,]> Data
    {
        get
        {
            var res = new LinkedList<int[,]>();
            foreach (var item in _data)
            {
                int[,] addition = new int[I, J];
                for (int i = 0; i < I; i++)
                    for (int j = 0; j < J; j++)
                        addition[i, j] = item[i, j];
                res.AddLast(addition);
            }
            return res;
        }
        set { _data = value; }
    }

    public int I,J;
    private void Start()
    {
       /* GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>().index = GetIndex(-1);
        GameObject.FindGameObjectWithTag("Rob").GetComponent<Robots>().index = GetIndex(-2);*/
    }

    public int GetValue(Vector3 index)
    {
        var level = _data.First;
        for (int y = 0; y < index.y; y++)
            level = level.Next;

        return level.Value[(int)index.x, (int)index.z];
    }

    public void SetValueWithIndex(int value, Vector3 index)
    {
        var level = _data.First;
        for (int y = 0; y < index.y; y++)
            level = level.Next;
        level.Value[(int)index.x, (int)index.z] = value;
    }

    public Vector3 GetIndex(int value)
    {
        var level = _data.First;
        for (int y = 0; y < _data.Count; y++)
        {
            for (int i = 0; i < I; i++)
                for (int j = 0; j < J; j++)
                {
                    if (level.Value[i, j] == value)
                        return new Vector3(i, y, j);
                }
            level = level.Next;
        }
        return new Vector3();
    }
      
}