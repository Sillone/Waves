using Assets.Scritps.Common.Interfaces;
using UnityEngine;

namespace Assets.Scritps.Common
{
    public abstract class EntityBase : MonoBehaviour, IEntityBase
    {
        public int AntiSpeedWalking;
        public int AntiSpeedRotation;

        public bool IsFree;
        protected Transform _trans;
        protected IMatrixController mc;
        public Vector3 Index;

        public int MyValue;

        protected int _progressWalk;
        private Vector3 _goingTo;
        private Vector3 _finalTo;

        protected int _progressRot;
        private bool _rotateRight;
        private Quaternion _finalRotate;

        public bool Lifting;

        public enum Values : int { HeroVal = -1, RobotVal = -2, EmptyVal = 0, WallVal = 1, Lift = -8 }

        protected virtual void StartEntity()
        {
            Lifting = false;

            AntiSpeedWalking = 30;
            AntiSpeedRotation = 15;
            IsFree = true;

            var tt = GameObject.Find("_CONTROLLERS_");
            if (tt != null)
                mc = tt.GetComponent<MatrixController>();


            Index = new Vector3();
            _trans = gameObject.transform;
        }

        public virtual void GoTo(Vector3 dir)
        {
            if (!IsFree)
                return;

            var newIndex = dir.normalized + Index;

            var startRot = _trans.rotation;
            _trans.LookAt(_trans.position + dir);


            if (startRot == _trans.rotation)
            {
                //walk
                //walk
                var wasValue = mc.GetValue(newIndex);
                if (newIndex.x < mc.I && newIndex.z < mc.J && newIndex.x >= 0 &&
                    newIndex.z >= 0 && wasValue != 1)
                {
                    if (wasValue == 999)
                        GameObject.Find("_CONTROLLERS_").GetComponent<LevelsController>().Finish = true;


                    var fwd = transform.TransformDirection(Vector3.forward);

                    var ray = new Ray(_trans.position, fwd);
                    RaycastHit hit;

                    DoorScr door = null;
                    if (Physics.Raycast(ray, out hit, 1))
                    { 
                        door = hit.transform.gameObject.GetComponent<DoorScr>();
                    }

                    if (door != null) return;
                    mc.SetValueWithIndex(0, Index);
                    Index = newIndex;
                    mc.SetValueWithIndex(MyValue, Index);

                    _progressWalk = AntiSpeedWalking;
                    _goingTo = dir;
                    _finalTo = _trans.position + dir;
                    IsFree = false;

                    var anim = gameObject.GetComponent<Animator>();
                    if (anim != null)
                        anim.SetBool("Walking", true);
                    //anim.SetInteger("State", 1);
                }
            }
            else
            {

                ///rotate
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

                    _rotateRight = _trans.rotation.eulerAngles.y == temp;

                    _finalRotate = _trans.rotation;
                }

                _trans.rotation = startRot;
                _progressRot = AntiSpeedRotation;
                IsFree = false;

                var anim = gameObject.GetComponent<Animator>();
                if (anim != null)
                {
                    //anim.SetBool("Rotating", true);
                    //anim.SetBool("Right", _rotateRight);
                }
            }
        }

        public virtual void UpdateEntity()
        {
            if (_progressWalk > 0)
            {
                var deltaPos = _goingTo / AntiSpeedWalking;

                _trans.position += deltaPos;

                _progressWalk--;
                if (_progressWalk == 0)
                {
                    if (!Lifting)
                        IsFree = true;
                    _trans.position = _finalTo;

                    var anim = gameObject.GetComponent<Animator>();
                    if (anim != null)
                    {
                        anim.SetBool("Walking", false);
                    }
                }
            }
            if (_progressRot > 0)
            {
                _trans.RotateAround(_trans.position, _rotateRight ? new Vector3(0, 1, 0) : new Vector3(0, -1, 0),
                    90f / AntiSpeedRotation);
                _progressRot--;


                if (_progressRot == 0)
                {
                    var anim = gameObject.GetComponent<Animator>();
                    if (anim != null)
                    {
                        anim.SetBool("Rotating", false);
                    }
                    IsFree = true;

                    _trans.rotation = _finalRotate;
                }
            }
        }
    }
}
