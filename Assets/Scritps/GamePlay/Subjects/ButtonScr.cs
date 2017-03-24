using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScr : MonoBehaviour {
    public DoorScr DorScr;
    public bool Off;
    public Material ActiveMat;
    public GameObject truba;
    public void Activate()
    {
    //    Door.GetComponent<DoorScr>().ChangeState(true);

        gameObject.GetComponent<MeshRenderer>().materials = new Material[] { ActiveMat };
        truba.GetComponent<MeshRenderer>().materials = new Material[] { ActiveMat };

        DorScr.ChangeState(true);
      
       // Destroy(gameObject);
    }
}
