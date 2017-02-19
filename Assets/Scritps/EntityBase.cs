using UnityEngine;
using System.Collections;

public abstract class EntityBase : MonoBehaviour
{
    public int AntiSpeedWalking;
    public int AntiSpeedRotation;

    public bool isFree;
    protected Transform _trans;
    public MatrixController matrixController;
    public Vector3 index;

    public int MyValue;

    protected int progressWalk;
    private Vector3 _goingTo;   
    private Vector3 _finalTo;

    protected int progressRot;
    public bool _rotateRight;
    private Quaternion _finalRotate;

    protected virtual void StartEntity()
    {

        AntiSpeedWalking = 30;
        AntiSpeedRotation = 40;
        isFree = true;

        var tt = GameObject.Find("_CONTROLLERS_");
        if (tt != null)
            matrixController = tt.GetComponent<MatrixController>();

        index = new Vector3();
        _trans = gameObject.transform;
    }

    public virtual void GoTo(Vector3 dir)
    {
        if (isFree)
        {
            var newIndex = dir.normalized + index;

            var startRot = _trans.rotation;
            _trans.LookAt(_trans.position + dir);

            if (startRot == _trans.rotation)
            {   //walk
                if (newIndex.x < matrixController.I && newIndex.z < matrixController.J && newIndex.x >= 0 && newIndex.z >= 0 && matrixController.GetValue(newIndex) != 1)
                {// matrix cheks
                    Vector3 fwd = transform.TransformDirection(Vector3.forward);

                    Ray ray = new Ray(_trans.position, fwd);
                    RaycastHit hit;

                    DoorScr door = null;
                    if (Physics.Raycast(ray, out hit, 1))
                    {
                        print(hit.transform.gameObject.name + "is near");
                        door = hit.transform.gameObject.GetComponent<DoorScr>();
                    }

                    if (door ==null)//door check
                    {
                        matrixController.SetValueWithIndex(0, index);
                        index = newIndex;
                        matrixController.SetValueWithIndex(MyValue, index);

                        progressWalk = AntiSpeedWalking;
                        _goingTo = dir;
                        _finalTo = _trans.position + dir;
                        isFree = false;

                        var anim = gameObject.GetComponent<Animator>();
                        if (anim != null)
                        {
                            anim.SetInteger("State", 1);
                        }
                    }

                }
            }
            else
            {///rotate

                if (Mathf.Abs(_trans.rotation.eulerAngles.y - startRot.eulerAngles.y) == 180)
                {
                    _rotateRight = false;
                    //  print("around");

                    _trans.RotateAround(_trans.position, new Vector3(0, 1, 0), 90);
                    _finalRotate = _trans.rotation;
                }
                else
                {
                    var temp = startRot.eulerAngles.y + 90;
                    if (temp >= 360)
                        temp -= 360;
                    if (_trans.rotation.eulerAngles.y == temp)
                    {
                        _rotateRight = true;
                    }
                    else
                    {
                        _rotateRight = false;
                    }

                    _finalRotate = _trans.rotation;
                }



                _trans.rotation = startRot;
                progressRot = AntiSpeedRotation;
                isFree = false;
            }            
        }
    }

    public virtual void UpdateEntity()
    {
        if (progressWalk > 0)
        {
            var deltaPos = _goingTo * 1f / (float)AntiSpeedWalking;

            _trans.position += deltaPos;

            progressWalk--;
            if (progressWalk == 0)
            {
                isFree = true;
                _trans.position = _finalTo;

                var anim = gameObject.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.SetInteger("State", 0);
                }
            }
        }
        if (progressRot > 0)
        {
            if (_rotateRight)
                _trans.RotateAround(_trans.position, new Vector3(0,1,0),90 / AntiSpeedRotation);
            else
                _trans.RotateAround(_trans.position, new Vector3(0, -1, 0), 90 / AntiSpeedRotation);

            progressRot--;
            if (progressRot == 0)
            {
                isFree = true;
                _trans.rotation = _finalRotate;
            }
        }
    }
}