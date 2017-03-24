using System;
using UnityEngine;
using System.Collections;
using UnityEditor.VersionControl;
using Assets.Scritps;
using Assets.Scritps.Common;

public class Hero : EntityBase, IHero
{
    public GameObject Wave;

    void Start()
    {
        StartEntity();
        Index = mc.GetIndex(-1);

    }

    private void Update()
    {
        UpdateEntity();
        if (_progressWalk == 2)
        {
            Instantiate(Wave, _trans.position, Quaternion.identity);
            Ray ray = new Ray(_trans.position, _trans.position - new Vector3(0, -3f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 4))
            {
                var hittedGo =hit.collider.gameObject;
                print(hittedGo.tag);
                if (hittedGo.tag == "Button")
                    this.OnHitButton(hittedGo);
            }
        }
        
    }
    /*
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

        var flag = false;
        for (var i = -5; i < 5; i++)
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
        
        var y = (int)ang;
        if (y < -88 && y > -92)
            y = -90;

      
        print(y + "= angle   ");

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

  
    */
    private void OnHitButton(GameObject button)
    {
        button.GetComponent<ButtonScr>().Activate();
    }

    void OnTriggerEnter(Collider other)
    {
        print("some trigger enter");
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
