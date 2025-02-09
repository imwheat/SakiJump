//****************** 代码文件申明 ***********************
//* 文件：SettingsPanel
//* 作者：wheat
//* 创建时间：2024/10/01 10:16:18 星期二
//* 描述：游戏的设置面板
//*******************************************************

using UnityEngine;

namespace KFrame.UI
{
    [UIData(typeof(SettingsPanel), "SettingsPanel", true, 3)]
    public class SettingsPanel : UIPanelBase
    {
        #region UI配置

        /// <summary>
        /// 音效设置按钮
        /// </summary>
        [SerializeField] 
        private KButton audioSettingsBtn;
        /// <summary>
        /// 画面设置按钮
        /// </summary>
        [SerializeField] 
        private KButton graphicSettingsBtn;
        /// <summary>
        /// 返回按钮
        /// </summary>
        [SerializeField] 
        private KButton returnBtn;

        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            //按键事件注册
            audioSettingsBtn.OnClick.AddListener(OnClickAudioSettingsBtn);
            graphicSettingsBtn.OnClick.AddListener(OnClickGraphicSettingsBtn);
            returnBtn.OnClick.AddListener(OnPressESC);
        }

        #region UI事件

        /// <summary>
        /// 点击游戏设置按钮
        /// </summary>
        private void OnClickAudioSettingsBtn()
        {
            SwitchToThisPanel<AudioSettingsPanel>(true);
        }
        /// <summary>
        /// 点击游戏设置按钮
        /// </summary>
        private void OnClickGraphicSettingsBtn()
        {
            SwitchToThisPanel<GraphicSettingsPanel>(true);
        }

        #endregion

    }
}

