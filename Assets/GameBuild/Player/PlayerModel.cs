//****************** 代码文件申明 ***********************
//* 文件：PlayerModel
//* 作者：wheat
//* 创建时间：2025/02/09 13:13:34 星期日
//* 描述：玩家属性模块
//*******************************************************

using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameBuild.Player
{
    [System.Serializable]
    public class PlayerModel
    {
        #region 移动相关配置

        /// <summary>
        /// 移动速度
        /// </summary>
        [LabelText("移动速度"), TabGroup("移动")]
        public float MoveSpeed = 3f;
        /// <summary>
        /// 跳跃移动速度
        /// </summary>
        [LabelText("跳跃移动速度"), TabGroup("移动")]
        public float JumpMoveSpeed = 4.2f;
        /// <summary>
        /// 轻跳力度
        /// </summary>
        [LabelText("轻跳力度"), TabGroup("移动")]
        public float JumpForceLight = 3f;
        /// <summary>
        /// 大跳力度
        /// </summary>
        [LabelText("大跳力度"), TabGroup("移动")]
        public float JumpForceLarge = 14f;
        /// <summary>
        /// 最小跳力度阈值
        /// </summary>
        [LabelText("最小跳力度阈值"), TabGroup("移动")]
        public float JumpForceMinThreshold = 0f;
        /// <summary>
        /// 最大跳力度阈值
        /// </summary>
        [LabelText("最大跳力度阈值"), TabGroup("移动")]
        public float JumpForceMaxThreshold = 0.7f;
        /// <summary>
        /// 砸地时间阈值
        /// </summary>
        [LabelText("砸地时间阈值"), TabGroup("移动")]
        public float SplatTimeThreshold = 0.75f;
        /// <summary>
        /// 砸地不能移动的时间
        /// </summary>
        [LabelText("砸地不能移动的时间"), TabGroup("移动")]
        public float SplatFreezeTime = 0.5f;
        /// <summary>
        /// 砸地不能移动的计时器
        /// </summary>
        [LabelText("砸地不能移动的计时器"), TabGroup("移动")]
        public float SplatFreezeTimer;
        /// <summary>
        /// 参考系速度
        /// </summary>
        [LabelText("参考系速度"), TabGroup("移动")]
        public Vector2 RefVelocity = Vector2.zero;
        /// <summary>
        /// 自身当前速度
        /// </summary>
        [LabelText("自身当前速度"), TabGroup("移动")]
        public Vector2 SelfVelocity = Vector2.zero;
        /// <summary>
        /// 朝向正反
        /// </summary>
        [LabelText("朝向正反"), TabGroup("移动")]
        public bool rightDir = true;
        
        #endregion

        #region 输入控制

        /// <summary>
        /// 开始跳跃
        /// </summary>
        [LabelText("开始跳跃"), TabGroup("输入")]
        public bool StartJump;
        /// <summary>
        /// 按下跳跃键的时间
        /// </summary>
        [LabelText("按下跳跃键的时间"), TabGroup("输入")]
        public float JumpPressTime;

        #endregion

        #region 检测

        /// <summary>
        /// 在地面上
        /// </summary>
        [LabelText("在地面上"), TabGroup("检测")]
        public bool OnGround;
        /// <summary>
        /// 跳跃中
        /// </summary>
        [LabelText("跳跃中"), TabGroup("检测")]
        public bool OnJump;
        /// <summary>
        /// 起跳速度
        /// </summary>
        [LabelText("起跳速度"), TabGroup("检测")]
        public float JumpUpSpeed;
        /// <summary>
        /// 掉落计时器
        /// </summary>
        [LabelText("掉落计时器"), TabGroup("检测")]
        public float FallingTimer;

        #endregion

    }
}

