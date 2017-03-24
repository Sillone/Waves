using Assets.Scritps.Common.Interfaces;
using UnityEngine;

public class DoorScr : MonoBehaviour {
    private bool _open;
    public Vector3 index;
    private IMatrixController _mc;
    private int Steps;
    private void Start()
    {
    //    index = new Vector3
    //    {
    //        x = (int)transform.position.x,
    //        y = (int)transform.position.y-2,
    //        z = (int)transform.position.z
    //    };
        _mc = GameObject.Find("_CONTROLLERS_").GetComponent<MatrixController>();
   
        _open = false;
    }

    public void ChangeState(bool state)
    {
        if (state)
        {
            _open = true;
            Steps = 50;

            _mc.SetValueWithIndex(999, index);//delete from Matrix
            //temporaryAction
          //  Destroy(gameObject);
            //action _open
        }
        else
        {
            _open = false;
            //action Close;
        }
    }

    private void Update()
    {
        if (_open)
        {
            if (Steps > 0)
            {
                print("even steps");
                transform.Translate(0, -0.04f, 0);
                Steps--;
            }
            else
                Destroy(gameObject);

        }
    }
}
