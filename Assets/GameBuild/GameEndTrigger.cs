//****************** 代码文件申明 ***********************
//* 文件：GameEndTrigger
//* 作者：wheat
//* 创建时间：2025/02/10 18:57:16 星期一
//* 描述：
//*******************************************************

using System;
using GameBuild.Player;
using KFrame.Utilities;
using UnityEngine;

namespace GameBuild
{
    public class GameEndTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Player))
            {
                PlayerController player = other.GetComponentInParent<PlayerController>();

                //如果获取到了玩家
                if (player != null)
                {
                    GameManager.GameClear();
                }
            }
        }
    }
}

