using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scritps
{
    public static class Extentions
    {
        public static List<Vector2> DoWave(this int[,] arra, Vector2 hero, int I, int J, int step, Vector3 v)
        {
            var way = new List<Vector2>();
            if (Mathf.Abs(hero.x - v.x) <= 2 && Mathf.Abs(hero.y - v.z) <= 2)
                arra[(int)v.x, (int)v.z] = step;

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



            flag = false;
            var iH = (int)hero.x;
            var jH = (int)hero.y;
            while (!flag)
            {

                if (iH - 1 >= 0 && arra[iH - 1, jH] == step - 1)
                {
                    way.Add(new Vector2(1, 0));
                    iH--;
                    step--;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (iH + 1 < I && arra[iH + 1, jH] == step - 1)
                {
                    way.Add(new Vector2(-1, 0));
                    step--;
                    iH++;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (jH - 1 >= 0 && arra[iH, jH - 1] == step - 1)
                {
                    way.Add(new Vector2(0, 1));
                    step--;
                    jH--;
                    if (step == 2)
                        flag = true;
                    continue;
                }
                if (jH + 1 < J && arra[iH, jH + 1] == step - 1)
                {
                    way.Add(new Vector2(0, -1));
                    step--;
                    jH++;
                    if (step == 2)
                        flag = true;
                    continue;
                }
            }
            way.Reverse();

            return way;
        }
    }
}