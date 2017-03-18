using System;
using UnityEngine;
using System.Collections;
using UnityEditorInternal;
using Assets.Scritps.Common;

public class HeroController : MonoBehaviour ,IController
{
    private MatrixController _mc;
    public Hero hero;
    public Robots robot;
    public void Start()
    {
        _mc = gameObject.GetComponent<MatrixController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMatrix();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //hero.GoForward();
            hero.GoTo(new Vector3(0, 0, 1));
            GoRobot();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
          //  hero.Rot(false);
            hero.GoTo(new Vector3(0, 0, -1));
            GoRobot();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
           // hero.Rot(true);
            hero.GoTo(new Vector3(1, 0, 0));
            GoRobot();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
           // hero.Rot(false);
            hero.GoTo(new Vector3(-1, 0, 0));
            GoRobot();
        }
    }

    public void GoRobot()
    {
        var v = _mc.GetIndex(-1);
        if ((int)v.y == 1)
        {
            var her = new Vector2(v.x, v.z);
            if (robot != null)
                robot.DoWave(her, _mc.I, _mc.J, _mc.Data[1]);
        }
        else
            print("What with robot?");
    }

    public void ShowMatrix()
    {
        var matrix = _mc.Data[1];
        for (var i = 0; i < _mc.I; i++)
        {
            var s = string.Empty;
            for (var j = 0; j < _mc.J; j++)
                s = s + " " + matrix[i, j].ToString();
            print(i.ToString() + ")  " + s);
        }
    }
}