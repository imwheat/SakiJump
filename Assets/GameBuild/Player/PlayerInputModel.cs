//****************** 代码文件申明 ***********************
//* 文件：PlayerInputModel
//* 作者：wheat
//* 创建时间：2025/02/09 13:13:18 星期日
//* 描述：玩家输入模块
//*******************************************************

using UnityEngine;

namespace GameBuild.Player
{
    [System.Serializable]
    public class PlayerInputModel
    {
        /// <summary>
        /// 移动输入
        /// </summary>
        public Vector2 MoveInput;
        /// <summary>
        /// 跳跃输入
        /// </summary>
        public bool JumpInput;

        public void ResetPlayerInput()
        {
            MoveInput = Vector2.zero;
            JumpInput = false;
        }
    }
}

