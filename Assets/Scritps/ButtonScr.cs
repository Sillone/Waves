using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScr : MonoBehaviour {
    public GameObject ButtonOther;
    public DoorScr dorScr;
    public bool off;
    
    public void Activate()
    {
    //    Door.GetComponent<DoorScr>().ChangeState(true);

        print("Activateed");
        dorScr.ChangeState(true);
        var go = Instantiate(ButtonOther, transform.position, transform.rotation);
        go.transform.localScale = transform.localScale;

        Destroy(gameObject);
    }
}
