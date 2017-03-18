using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assets.Scritps.Common;

public class Robots : EntityBase ,IRobot
{
    private List<Vector2> _way;

    private int _steps;
    void Start()
    {
        StartEntity();
        _steps = 0;
        _way = new List<Vector2>();
        Index = mc.GetIndex(MyValue);
    }

    void Update()
    {
        UpdateEntity();
        if (_way == null)
            return;

        if (_way.Count > 0 && IsFree && _steps>0)
        {
            _steps--;
            var t = _way.FirstOrDefault();
            GoTo(new Vector3(t.x, 0, t.y));
            if (_progressRot <= 0)
                _way.Remove(t);
        }
    }

    public void DoWave(Vector2 hero, int I, int J, int[,] arra)
    {
        var step = 2;
        var v = mc.GetIndex(MyValue);

        if (Mathf.Abs(hero.x - v.x) <=2 && Mathf.Abs(hero.y - v.z) <= 2)
        try
        {
            arra[(int) v.x, (int) v.z] = step;

            var flag = false;
            var end = false;
            while (!flag && !end)
            {
                flag = true;

                for (var i = 0; i < I; i++)
                    for (var j = 0; j < J; j++)
                        if (arra[i, j] == step && !end)
                        {
                            if (i - 1 >= 0)
                            {
                                if (arra[i - 1, j] == -1)
                                {
                                    arra[i - 1, j] = step + 1;
                                    flag = false;
                                    end = true;
                                }
                                if (arra[i - 1, j] == 0)
                                {
                                    arra[i - 1, j] = step + 1;
                                    flag = false;
                                }
                            }

                            if (i + 1 < I)
                            {
                                if (arra[i + 1, j] == -1)
                                {
                                    arra[i + 1, j] = step + 1;
                                    flag = false;
                                    end = true;
                                }
                                if (arra[i + 1, j] == 0)
                                {
                                    arra[i + 1, j] = step + 1;
                                    flag = false;
                                }
                            }

                            if (j - 1 >= 0)
                            {
                                if (arra[i, j - 1] == -1)
                                {
                                    arra[i, j - 1] = step + 1;
                                    flag = false;
                                    end = true;
                                }
                                if (arra[i, j - 1] == 0)
                                {
                                    arra[i, j - 1] = step + 1;
                                    flag = false;
                                }
                            }

                            if (j + 1 < J)
                            {
                                if (arra[i, j + 1] == -1)
                                {
                                    arra[i, j + 1] = step + 1;
                                    flag = false;
                                    end = true;
                                }
                                if (arra[i, j + 1] == 0)
                                {
                                    arra[i, j + 1] = step + 1;
                                    flag = false;
                                }
                            }
                        }
                step++;
            }

            _way = new List<Vector2>();

            flag = false;
            var iH = (int) hero.x;
            var jH = (int) hero.y;
            while (!flag)
            {

                if (iH - 1 >= 0 && arra[iH - 1, jH] == step - 1)
                {
                    _way.Add(new Vector2(1, 0));
                    iH--;
                    step--;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (iH + 1 < I && arra[iH + 1, jH] == step - 1)
                {
                    _way.Add(new Vector2(-1, 0));
                    step--;
                    iH++;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (jH - 1 >= 0 && arra[iH, jH - 1] == step - 1)
                {
                    _way.Add(new Vector2(0, 1));
                    step--;
                    jH--;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (jH + 1 < J && arra[iH, jH + 1] == step - 1)
                {
                    _way.Add(new Vector2(0, -1));
                    step--;
                    jH++;
                    if (step == 2)
                        flag = true;
                    continue;
                }
            }
            _way.Reverse();
        }
        catch
        {
            Debug.LogError("Ebanaya volna");
        }
        _steps += 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("robot Triger");
        print(other.tag);
        if (other.tag == "Waves")
        {
            print("Wave robot");
            var mc = GameObject.Find("_CONTROLLERS_").GetComponent<MatrixController>();
            var v = mc.GetIndex(-1);
            if (v.y == 1)
            {
                var her = new Vector2(v.x, v.z);
                DoWave(her, mc.I, mc.J, mc.Data[1]);
            }
            else
                print("Walking error");
        }
    }
}