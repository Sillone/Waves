using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScr : MonoBehaviour {
    private bool open;
    public Vector3 index;
    private MatrixController mc;
    private void Start()
    {
        mc = GameObject.Find("_CONTROLLERS_").GetComponent<MatrixController>();
   
        open = false;
    }

    public void ChangeState(bool state)
    {
        if (state)
        {
            open = true;

            mc.SetValueWithIndex(0, index);//delete from matrix
            //temporaryAction
            Destroy(gameObject);
            //action open
        }
        else
        {
            open = false;
            //action Close;
        }
    }
}
