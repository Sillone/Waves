using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScr : MonoBehaviour {
    public GameObject ButtonOther;
    public DoorScr DorScr;
    public bool Off;
    
    public void Activate()
    {
    //    Door.GetComponent<DoorScr>().ChangeState(true);

        print("Activateed");
        DorScr.ChangeState(true);
        var go = Instantiate(ButtonOther, transform.position, transform.rotation);
        go.transform.localScale = transform.localScale;

        Destroy(gameObject);
    }
}
