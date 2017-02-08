using UnityEngine;
using System.Collections;

public class Hero : EntityBase
{
    public GameObject wave;
    void Start()
    {
        StartEntity();
        index = matrixController.GetIndex(-1);
    }
    private void Update()
    {
        UpdateEntity();
        if (progressWalk==2)
        {
            Instantiate(wave, _trans.position, Quaternion.identity);
        }
    }

    public void GoForward()
    {
        var y =(int) _trans.rotation.y;
        switch (y)
        {
            case 360:
            case 0:
                {
                    GoTo(new Vector3(0, 0, 1));
                    break;
                }
            case 90:
                {
                    GoTo(new Vector3(1, 0, 0));
                    break;
                }
            case 180:
                {
                    GoTo(new Vector3(0, 0, -1));
                    break;
                }
            case 270:
                {
                    GoTo(new Vector3(-1, 0, 0));
                    break;
                }
            default: break;
        }

    }

    public void Rot(bool right)
    {
        float ang;
        Vector3 vec;
        transform.rotation.ToAngleAxis(out ang, out vec);

        bool flag = false;
        for (int i = -5; i < 5; i++)
        {
            if (i*90 == ang)
            {
                flag = true;
                break;
            }
        }

        if (flag == false)
        {
            float teemp=0;
            for (var i = -4; i <= 4; i++)
                if (ang > i * 90 - 3 && ang < i * 90 + 3)
                     teemp= i * 90;
            _trans.rotation.SetEulerRotation(0, teemp, 0);
        }
      //  print(ang.ToString() + "= angle   ");
        
        var y = (int)ang;
        if (y < -88 && y > -92)
            y = -90;

       /* for (var i = -4; i <= 4; i++)
            if (y > i * 90 - 3 && y < i * 90 + 3)
                y = i * 90;*/

     /*   if (y < 1)
            y += 360;

        if (y > 100)
        {
            print("shitty angle");
            y = -180;
           // right = !right;
        }*/

        print(y.ToString() + "= angle   ");

        switch (y)
        {
            case 360:
            case 0:
                {
                    if (right)
                        GoTo(new Vector3(1, 0, 0));
                    else
                        GoTo(new Vector3(-1, 0, 0));
                    break;
                }
            case -270:
            case 90:
                {
                    if (right)
                        GoTo(new Vector3(0, 0, -1));
                    else
                        GoTo(new Vector3(0, 0, 1));
                    break;
                }

            case -180:
            case 180:
                {
                    if (!right)
                        GoTo(new Vector3(1, 0, 0));
                    else
                        GoTo(new Vector3(-1, 0, 0));
                    break;
                }
            case -90:
                {
                    print("SHIIIT");
                    if (right)
                        GoTo(new Vector3(0, 0, -1));
                    else
                        GoTo(new Vector3(0, 0, 1));
                    break;
                }
            case 270:
                {

                    if (!right)
                        GoTo(new Vector3(0, 0, -1));
                    else
                        GoTo(new Vector3(0, 0, 1));
                    break;
                }

            default: {  } break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Rob":
                {
                    print("dead");
                    Destroy(gameObject);
                    break;
                }
            case "Button":
                {
                    other.gameObject.GetComponent<ButtonScr>().Activate();
                    break;
                }

            default: break;
        }
    }
}
