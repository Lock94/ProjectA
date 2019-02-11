using RootMotion.Demos;
using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BanTiger.Battle
{
    public class PlayerController : MonoBehaviour
    {
        public int currentHealth = 80;
        public float speed = 1f;
        private Animator animator;
        private float rotationY = 0;
        public KeyCode crouchKey = KeyCode.LeftControl;
        public KeyCode runKey = KeyCode.LeftShift;
        public KeyCode blockKey = KeyCode.LeftAlt;
        private bool weakAttack = true, strongAttack = true, shootAttack = true, reloadEffect = true, blockAttack = true, isBlock = false, isMoving = false, isStrafing = false, isCrouch = false, hitRecoil = true;
        public bool ShooterWeapon = false;
        private SimpleAimingSystem sas;
        private AimIK aik;
        private LookAtIK laik;
        // Use this for initialization

        //-------------Test----------
        int id = 0;
        void Start()
        {
            animator = transform.GetComponent<Animator>();
            sas = transform.GetComponent<SimpleAimingSystem>();
            aik = transform.GetComponent<AimIK>();
            laik = transform.GetComponent<LookAtIK>();
        }

        // Update is called once per frame
        void Update()
        {
            //未收到攻击时候才能执行行为
            if (hitRecoil)
            {
                AttackControl();
                MoveControl();
                AimControl();
            }
            else
            {
                transform.Translate(Vector3.back * Time.deltaTime);
            }
            Test_ChangeWeapon();
        }

        void AimControl()
        {
            if (isStrafing)
            {
                sas.enabled = true;
                aik.enabled = true;
                laik.enabled = true;
            }
            else
            {
                sas.enabled = false;
                aik.enabled = false;
                laik.enabled = false;
            }
        }

        void Test_ChangeWeapon()
        {
            if (id == 1 || id == 2)
            {
                ShooterWeapon = true;
                animator.SetInteger("ShooterID", id);
                animator.SetBool("HasAmmo", true);
            }
            else
            {
                ShooterWeapon = false;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                id += 1;
                id = Mathf.Min(4, id);
                id = Mathf.Max(0, id);
                animator.SetInteger("BodyID", id);
                animator.SetTrigger("WeaponChange");
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                id -= 1;
                id = Mathf.Min(4, id);
                id = Mathf.Max(0, id);
                animator.SetInteger("BodyID", id);
                animator.SetTrigger("WeaponChange");
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                StartCoroutine(HitRecoil(1));
            }
        }

        void MoveControl()
        {
            float deltaX = Input.GetAxis("Horizontal"); //Debug.Log(deltaX);
            float deltaZ = Input.GetAxis("Vertical");   //Debug.Log(deltaZ);
            float InputMagnitude = Mathf.Max(Mathf.Abs(deltaX), Mathf.Abs(deltaZ)) / 2;
            if (deltaX != 0 && deltaZ != 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }
            if (Input.GetKey(crouchKey) && !isBlock)
            {
                deltaX = deltaX / 2;
                deltaZ = deltaZ / 2;
                isCrouch = true;
                animator.SetBool("IsCrouch", true);
            }
            else if (Input.GetKey(runKey) && !isBlock && !isStrafing && canRun())
            {
                InputMagnitude = 2 * InputMagnitude;
                deltaX = 2 * deltaX;
                deltaZ = 2 * deltaZ;
            }
            if (Input.GetKeyUp(crouchKey))
            {
                animator.SetBool("IsCrouch", false);
                isCrouch = false;
            }

            if (Input.GetMouseButtonDown(1))
            {
                isStrafing = true;
                animator.SetBool("IsStrafe", true);
                if (!isCrouch && !isMoving)
                    animator.SetTrigger("WeaponChange");
            }

            if (Input.GetMouseButtonUp(1))
            {
                isStrafing = false;
                animator.SetBool("IsStrafe", false);
                if (!isCrouch && !isMoving)
                    animator.SetTrigger("WeaponChange");
            }
            if (isStrafing)
            {
                transform.LookAt(new Vector3(BattleController.Instance.worldPos.x, transform.position.y, BattleController.Instance.worldPos.z));
                Vector3 v3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                Vector3 vl3 = V3RotateAround(v3, Vector3.zero, -Vector3.up, transform.eulerAngles.y);
                rotationY = transform.eulerAngles.y;
                animator.SetFloat("InputHorizontal", vl3.x);
                animator.SetFloat("InputVertical", vl3.z);
                deltaX /= 2;
                deltaZ /= 2;
            }
            else
            {
                if (Mathf.Abs(deltaZ) >= 0.2 | Mathf.Abs(deltaX) >= 0.2)
                {
                    if (deltaZ > 0 && deltaX >= 0)
                    {
                        rotationY = Mathf.Atan(Mathf.Abs(deltaX) / Mathf.Abs(deltaZ)) * (180 / Mathf.PI);
                    }
                    if (deltaZ <= 0 && deltaX > 0)
                    {
                        rotationY = Mathf.Atan(Mathf.Abs(deltaZ) / Mathf.Abs(deltaX)) * (180 / Mathf.PI) + 90;
                    }
                    if (deltaZ < 0 && deltaX <= 0)
                    {
                        rotationY = Mathf.Atan(Mathf.Abs(deltaX) / Mathf.Abs(deltaZ)) * (180 / Mathf.PI) + 180;
                    }
                    if (deltaZ >= 0 && deltaX < 0)
                    {
                        rotationY = Mathf.Atan(Mathf.Abs(deltaZ) / Mathf.Abs(deltaX)) * (180 / Mathf.PI) + 270;
                    }
                }
                Vector3 rotation = transform.localEulerAngles;
                rotation.y = rotationY;
                transform.localEulerAngles = rotation;
            }
            animator.SetFloat("InputMagnitude", InputMagnitude);
            transform.Translate(deltaX * speed * Time.deltaTime, transform.position.y, deltaZ * speed * Time.deltaTime, Space.World);
        }

        void AttackControl()
        {
            //格挡
            CheckBlock();
            if (!isBlock)
            {
                //轻攻击（左键1次攻击）
                StartCoroutine(WeakAttack(1));
                //重攻击（右键之后按左键攻击）
                StartCoroutine(StrongAttack(1));
                //远程武器射击
                if (ShooterWeapon)
                {
                    StartCoroutine(ShootAttack(1));
                    StartCoroutine(ReloadEffect(1));
                }
            }
            else
            {
                StartCoroutine(BlockAttack(1));
            }
        }

        void CheckBlock()
        {
            if (!isCrouch)
            {
                if (Input.GetKeyDown(blockKey))
                {
                    isBlock = true;
                    animator.SetBool("IsBlock", true);
                }
                if (Input.GetKeyUp(blockKey))
                {
                    isBlock = false;
                    animator.SetBool("IsBlock", false);
                }
            } 
        }

        public IEnumerator HitRecoil(float interval)
        {
            if (hitRecoil)
            {
                hitRecoil = false;
                animator.SetTrigger("Recoil");
                transform.Translate(0, 0, -10 * Time.deltaTime);
                yield return new WaitForSeconds(interval);      //收到攻击的硬直时间
                hitRecoil = true;
            }
        }


        IEnumerator BlockAttack(float interval)
        {
            if (Input.GetMouseButtonDown(0) && blockAttack)
            {
                blockAttack = false;
                animator.SetTrigger("BlockAttack");
                yield return new WaitForSeconds(interval);
                blockAttack = true;
            }
        }
        IEnumerator WeakAttack(float interval)
        {
            if (Input.GetMouseButtonDown(0) && weakAttack && !isStrafing && reloadEffect)
            {
                weakAttack = false;
                animator.SetTrigger("WeakAttack");
                yield return new WaitForSeconds(interval);
                weakAttack = true;
            }
        }
        IEnumerator StrongAttack(float interval)
        {
            if (isStrafing && !ShooterWeapon)
            {
                if (Input.GetMouseButtonDown(0) && strongAttack)
                {
                    strongAttack = false;
                    animator.SetTrigger("StrongAttack");
                    yield return new WaitForSeconds(interval);
                    strongAttack = true;
                }
            }
        }
        IEnumerator ShootAttack(float interval)
        {
            if (isStrafing)
            {
                if (Input.GetMouseButtonDown(0) && shootAttack && canShoot())
                {
                    shootAttack = false;
                    animator.SetTrigger("Shoot");
                    ShootAction();
                    yield return new WaitForSeconds(interval);
                    shootAttack = true;
                }
            }
        }
        IEnumerator ReloadEffect(float interval)
        {
            if (Input.GetKeyDown(KeyCode.R) && canReload() && reloadEffect)
            {
                reloadEffect = false;
                animator.SetTrigger("Reload");
                yield return new WaitForSeconds(interval);
                reloadEffect = true;
            }
        }

        void ShootAction()
        {
            LayerMask layer = 1 << 8 | 1 << 0;
            Ray ray = new Ray(transform.position + Vector3.up, BattleController.Instance.worldPos - transform.position);
            Debug.DrawRay(transform.position + Vector3.up, BattleController.Instance.worldPos - transform.position, Color.blue);
            var hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, layer))
            {
                if (hit.collider.gameObject.layer==8)
                {
                    if (hit.collider.GetComponent<NpcController>())
                    {
                        hit.collider.GetComponent<NpcController>().ReciveDamage(11, transform);
                    }
                    else if(hit.collider.GetComponentInParent<NpcController>())
                    {
                        hit.collider.GetComponentInParent<NpcController>().ReciveDamage(11, transform);
                    }
                }  
            }
        }

        //判断能否射击的条件，弹夹内是否有子弹
        bool canShoot()
        {
            if (!reloadEffect)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        //判断能否换子弹，背包里是否有足够的子弹
        bool canReload()
        {
            return true;
        }
        //判断能跑步，耐力没有就不能跑
        bool canRun()
        {
            return true;
        }

        /// <summary>
        /// 使Vector3绕旋转中心点在旋转轴上旋转。
        /// </summary>
        /// <param name="origin">旋转前的源Vector3</param>
        /// <param name="point">旋转中心</param>
        /// <param name="axis">旋转轴</param>
        /// <param name="angle">旋转角度</param>
        /// <returns>旋转后的目标Vector3</returns>
        public static Vector3 V3RotateAround(Vector3 origin, Vector3 point, Vector3 axis, float angle)
        {
            Quaternion q = Quaternion.AngleAxis(angle, axis);// 旋转系数

            Vector3 o = origin - point;// 旋转中心到源点的偏移向量

            o = q * o;// 旋转偏移向量，得到旋转中心到目标点的偏移向量

            return point + o;// 返回目标点
        }
    }
}