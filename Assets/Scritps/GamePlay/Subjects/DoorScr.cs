using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScr : MonoBehaviour {
    private bool _open;
    public Vector3 index;
    private MatrixController _mc;
    private void Start()
    {
        index = new Vector3
        {
            x = (int)transform.position.x,
            y = (int)transform.position.y-1,
            z = (int)transform.position.z
        };
        _mc = GameObject.Find("_CONTROLLERS_").GetComponent<MatrixController>();
   
        _open = false;
    }

    public void ChangeState(bool state)
    {
        if (state)
        {
            _open = true;

            _mc.SetValueWithIndex(999, index);//delete from Matrix
            //temporaryAction
            Destroy(gameObject);
            //action _open
        }
        else
        {
            _open = false;
            //action Close;
        }
    }
}
