//****************** 代码文件申明 ***********************
//* 文件：GameControlPanel
//* 作者：wheat
//* 创建时间：2025/02/10 20:25:21 星期一
//* 描述：手机端使用的游戏控制面板
//*******************************************************

using GameBuild.Player;
using KFrame.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameBuild
{
    [UIData(typeof(GameControlPanel), "GameControlPanel", true, 2)]
    public class GameControlPanel : UIBase
    {
        #region UI配置

        [SerializeField]
        private HoldButton leftButton;
        [SerializeField]
        private HoldButton rightButton;
        [SerializeField]
        private HoldButton jumpButton;
        [SerializeField]
        private KButton escButton;

        #endregion
        private PlayerInputModel InputModel
        {
            get
            {
                if (PlayerController.Instance != null)
                {
                    return PlayerController.Instance.InputModel;
                }

                return null;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            escButton.OnClick.AddListener(OnPressEsc);
            leftButton.OnClick.AddListener(OnClickLeft);
            rightButton.OnClick.AddListener(OnClickRight);
            jumpButton.OnClick.AddListener(OnClickJump);
            
            leftButton.OnRelease.AddListener(OnReleaseMove);
            rightButton.OnRelease.AddListener(OnReleaseMove);
            jumpButton.OnRelease.AddListener(OnReleaseJump);
        }

        public override void ResetRect()
        {
            base.ResetRect();
            
            RectTransform rectTransform = transform as RectTransform;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }

        #region 按钮事件

        private void OnPressEsc()
        {
            UISelectSystem.PressEsc();
        }
        private void OnClickLeft()
        {
            if(InputModel == null) return;

            InputModel.MoveInput = new Vector2(-1, 0);
        }
        private void OnReleaseMove()
        {
            if(InputModel == null) return;

            InputModel.MoveInput = new Vector2(0, 0);
        }
        private void OnClickRight()
        {
            if(InputModel == null) return;

            InputModel.MoveInput = new Vector2(1, 0);
        }
        private void OnClickJump()
        {
            if(InputModel == null) return;

            InputModel.JumpInput = true;
        }
        private void OnReleaseJump()
        {
            if(InputModel == null) return;

            InputModel.JumpInput = false;
        }

        #endregion


    }
}

