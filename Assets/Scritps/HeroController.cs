using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{
    private MatrixController mc;
    public Hero hero;
    public Robots robot;
    private void Start()
    {
        mc = gameObject.GetComponent<MatrixController>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ShowMatrix();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            hero.GoForward();
           // hero.GoTo(new Vector3(0, 0, 1));
            GoRobot();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
          //  hero.Rot(false);
            //hero.GoTo(new Vector3(0, 0, -1));
            GoRobot();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            hero.Rot(true);
           // hero.GoTo(new Vector3(1, 0, 0));
            GoRobot();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            hero.Rot(false);
           // hero.GoTo(new Vector3(-1, 0, 0));
            GoRobot();
        }
    }

    public void GoRobot()
    {
        var v = mc.GetIndex(-1);
        if (v.y == 1)
        {
            Vector2 her = new Vector2(v.x, v.z);
            if (robot != null)
                robot.DoWave(her, mc.I, mc.J, mc.Data.First.Next.Value);
        }
        else
            print("Walking error");
    }

    public void ShowMatrix()
    {
        var matrix = mc.Data.First.Next.Value;
        for (int i = 0; i < mc.I; i++)
        {
            string s = string.Empty;
            for (int j = 0; j < mc.J; j++)
                s = s + " " + matrix[i, j].ToString();
            print(i.ToString() + ")  " + s);
        }
    }
}