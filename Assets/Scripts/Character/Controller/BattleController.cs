using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BanTiger.Battle
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance;
        public Vector3 worldPos;
        public Vector3 screenPos;
        public Transform aimPos;
        public bool isStarf;

        public GameObject NpcPrafab;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            BuildNpcs();
        }

        // Update is called once per frame
        void Update()
        {
            GetMousePos();
            aimPos.position = worldPos;
        }
        /// <summary>
        /// 鼠标点击位置获取
        /// </summary>
        void GetMousePos()
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);                  //鼠标屏幕点从摄像机Z轴方向发出射线的对象
            RaycastHit hit;                                                         //射线检测的输出对象
            if (Physics.Raycast(r, out hit, 200))                             //射线检测，是否检测到物体
            {
                worldPos = hit.point;
                switch (hit.transform.tag)
                {
                    default:
                        break;
                }
            }
            screenPos = Camera.main.WorldToScreenPoint(worldPos);
        }

        void BuildNpcs()
        {
            GameObject team = new GameObject();
            team.name = "Team1";

            TeamController tc = team.AddComponent<TeamController>();
            //tc.BuildSoliders(NpcPrafab, new HeroPropertyMelee(1, "队长", CharacterPropertyBase.CharacterType.Enemy, 10000, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            //tc.BuildSoliders(NpcPrafab, new CharacterPropertyMelee(2, "队员1", CharacterPropertyBase.CharacterType.Enemy, 100, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            //tc.BuildSoliders(NpcPrafab, new CharacterPropertyMelee(3, "队员2", CharacterPropertyBase.CharacterType.Enemy, 100, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            //tc.BuildSoliders(NpcPrafab, new CharacterPropertyMelee(4, "队员3", CharacterPropertyBase.CharacterType.Enemy, 100, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
            //tc.BuildSoliders(NpcPrafab, new CharacterPropertyMelee(5, "队员4", CharacterPropertyBase.CharacterType.Enemy, 100, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
        }
    }
}