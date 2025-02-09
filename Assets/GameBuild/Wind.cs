//****************** 代码文件申明 ***********************
//* 文件：Wind
//* 作者：wheat
//* 创建时间：2025/02/09 19:57:31 星期日
//* 描述：风，可以吹动玩家，影响玩家移动
//*******************************************************

using System;
using GameBuild.Player;
using KFrame.Utilities;
using UnityEngine;

namespace GameBuild
{
    public class Wind : MonoBehaviour
    {
        /// <summary>
        /// 风力
        /// </summary>
        [SerializeField]
        private Vector2 windForce = new Vector2(1f, 0);
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                PlayerController player = other.GetComponentInParent<PlayerController>();

                //如果获取到了玩家，并且玩家不在地面上，那么就会被风吹走
                if (player != null && !player.Model.OnGround)
                {
                    player.Rb.AddForce(windForce, ForceMode2D.Force);
                }
            }
        }
    }
}

