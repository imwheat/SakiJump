//****************** 代码文件申明 ***********************
//* 文件：PlayerInputModule
//* 作者：wheat
//* 创建时间：2024/09/29 14:18:03 星期日
//* 描述：玩家的输入Module
//*******************************************************

using KFrame.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameBuild.Player
{
    [DefaultExecutionOrder(-11)]
    public class PlayerInputModule : InputModule
    {
        /// <summary>
        /// 玩家输入数据Model
        /// </summary>
        public PlayerInputModel InputModel;
        
        protected override void Init()
        {
            base.Init();

            InputModel = new PlayerInputModel();
        }

        #region 按键输入注册

        /// <summary>
        /// 注册按键输入
        /// </summary>
        protected override void RegisterInput()
        {
            base.RegisterInput();
            
            RegisterInputEvent(InputAction.Player.Move, OnMove, true, true, true);
            RegisterInputEvent(InputAction.Player.Jump, OnJump, true, true, true);
            RegisterInputEvent(InputAction.Player.Esc, OnPressESC, true, true, true);
        }
        /// <summary>
        /// 注销按键输入
        /// </summary>
        protected override void UnRegisterInput()
        {
            base.UnRegisterInput();
            
            //注销事件
            UnRegisterInputEvent(InputAction.Player.Move, OnMove);
            UnRegisterInputEvent(InputAction.Player.Jump, OnJump);
            UnRegisterInputEvent(InputAction.Player.Esc, OnPressESC);
            
            //重置玩家输入
            InputModel.ResetPlayerInput();
        }

        #endregion

        #region 按键事件处理

        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="context">输入</param>
        private void OnMove(InputAction.CallbackContext context)
        {
            InputModel.MoveInput = context.ReadValue<Vector2>();
        }
        /// <summary>
        /// 跳跃
        /// </summary>
        /// <param name="context">输入</param>
        private void OnJump(InputAction.CallbackContext context)
        {
            InputModel.JumpInput = context.ReadValueAsButton();
        }
        /// <summary>
        /// 按下ESC
        /// </summary>
        /// <param name="context">输入</param>
        private void OnPressESC(InputAction.CallbackContext context)
        {
            bool v = context.ReadValueAsButton();
        }
        
        #endregion
        
    }
}

