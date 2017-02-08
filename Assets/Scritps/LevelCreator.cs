using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    public LinkedList<int[,]> Data;

    public int I;
    public int J;

    public GameObject hero;
    public GameObject robot;

    public GameObject Floor;
    public GameObject Wall;
    public GameObject Capsula;
    public GameObject Door;
    public GameObject ButtonOff;
    public GameObject Water;

    public float step;
    private List<GameObject> Mesh;

    void Start()
    {
     /*   I = J = 6;
        Data = new LinkedList<int[,]>();
        var first = new int[,] { {1,1,1,1,1,1 },
                                 {1,1,1,1,1,1 },
                                 {1,1,1,1,1 ,1},
                                 {1,1,1,1,1 ,1},
                                 {1,1,1,1,1 ,1},
                                 {1,1,1,1,1 ,1}};

        var second = new int[,] { {1,1,0,0,0 ,1},
                                 {1,-1,0,1,0,1 },
                                 {0,0,0,1,0,0 },
                                 {0,1,0 ,0,0,1 },
                                 {0,0,0,0,0,0 },
                                 {0,0,0,0,-2,0 }};
        Data.AddLast(first);
        Data.AddLast(second);*/
        var mc = gameObject.GetComponent<MatrixController>();
        mc.I = I;
        mc.J = J;
        mc.Data = Data;
        Build();
    }

    private void Build()
    {
        Mesh = new List<GameObject>();
        var size = 1f / 10f;
        var Scale = new Vector3(size, size, size);
        int k = 0;
        if (Data == null)
            print("pizdec");
        else
            print(Data.Count.ToString() +"= levels");
        foreach (var ArrayMap in Data)
        {
            for (int i = 0; i < I; i++)
                for (int j = 0; j < J; j++)
                {
                    switch (ArrayMap[i, j])
                    {
                        case -1:
                            {
                                var ent = Instantiate(hero, new Vector3(i, k+1, j), Quaternion.identity);
                                print(i + "= i   " + k + "=k   " + j + "=j");
                              //  var scale = 5;
                               // ent.transform.localScale = new Vector3(scale, scale, scale);
                                gameObject.GetComponent<HeroController>().hero = ent.GetComponent<Hero>();
                                ent.GetComponent<Hero>().index = new Vector3(i, k, j);
                                break;
                            }
                        case -2:
                            {
                                var ent = Instantiate(robot, new Vector3(i, k+1, j), Quaternion.identity);
                                gameObject.GetComponent<HeroController>().robot = ent.GetComponent<Robots>();
                                ent.GetComponent<Robots>().index = new Vector3(i, k, j);
                                break;
                            }

                     /*   case 1:
                            {

                                if (k == 0)
                                {
                                    var go = (GameObject)Instantiate(Floor, new Vector3(i, k+1, j), new Quaternion());
                                    Mesh.Add(go);
                                }
                                else
                                    Mesh.Add((GameObject)Instantiate(Wall, new Vector3(i, k+1, j), new Quaternion()));
                                break;
                            }*/

                        case -5:
                            {
                                var go = (GameObject)Instantiate(Door, new Vector3(i, k+1, j), new Quaternion());
                                go.GetComponent<DoorScr>().index = new Vector3(i, k, j);
                                go.transform.Rotate(new Vector3(0, 1, 0), 90);
                                Mesh.Add(go);
                                break;
                            }
                        case -6:
                            {
                                var go = (GameObject)Instantiate(ButtonOff, new Vector3(i, k+1, j), new Quaternion());
                               
                                Mesh.Add(go);
                                break;
                            }
                    }

                    var parent = GameObject.Find("Meshes").transform;
                    foreach (var current in Mesh)
                    {
                        current.transform.parent = parent;
                    }
                }
            k++;
        }

        foreach (var current in Mesh)
        {
            current.transform.localScale = Scale;
        }
    }
}