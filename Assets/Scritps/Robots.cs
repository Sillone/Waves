using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class Robots : EntityBase
{
    public LinkedList<Vector2> way;
    public int[,] matrix;
    public int Count;

    public int Steps;
    void Start()
    {
        StartEntity();
        Steps = 0;
        way = new LinkedList<Vector2>();
        index = matrixController.GetIndex(MyValue);
    }

    void Update()
    {
        UpdateEntity();
        if (way != null)
            Count = way.Count;

        if (Count > 0 && isFree && Steps>0)
        {
            Steps--;
            var t = way.First.Value;
            GoTo(new Vector3(t.x, 0, t.y));
            if (progressRot <= 0)
                way.RemoveFirst();
        }
    }

    public void DoWave(Vector2 hero, int I, int J, int[,] arra)
    {
        var step = 2;
        var v = matrixController.GetIndex(MyValue);

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

            way = new LinkedList<Vector2>();

            flag = false;
            var iH = (int) hero.x;
            var jH = (int) hero.y;
            while (!flag)
            {

                if (iH - 1 >= 0 && arra[iH - 1, jH] == step - 1)
                {
                    way.AddFirst(new Vector2(1, 0));
                    iH--;
                    step--;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (iH + 1 < I && arra[iH + 1, jH] == step - 1)
                {
                    way.AddFirst(new Vector2(-1, 0));
                    step--;
                    iH++;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (jH - 1 >= 0 && arra[iH, jH - 1] == step - 1)
                {
                    way.AddFirst(new Vector2(0, 1));
                    step--;
                    jH--;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (jH + 1 < J && arra[iH, jH + 1] == step - 1)
                {
                    way.AddFirst(new Vector2(0, -1));
                    step--;
                    jH++;
                    if (step == 2)
                        flag = true;
                    continue;
                }
            }

            var str = string.Empty;
            foreach (var cur in way)
            {
                if (cur.x == -1)
                    str = str + "left ";
                if (cur.x == 1)
                    str = str + "right ";

                if (cur.y == -1)
                    str = str + "down ";
                if (cur.y == 1)
                    str = str + "up ";
            }
            //  print("way= " + str);
        }
        catch
        {
            Debug.LogError("Ebanaya volna");
        }
        Steps += 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("robot Triger");
        print(other.tag);
        if (other.tag == "Waves")
        {
            print("wave robot");
            var mc = GameObject.Find("_CONTROLLERS_").GetComponent<MatrixController>();
            var v = mc.GetIndex(-1);
            if (v.y == 1)
            {
                Vector2 her = new Vector2(v.x, v.z);
                DoWave(her, mc.I, mc.J, mc.Data.First.Next.Value);
            }
            else
                print("Walking error");
        }
    }
}