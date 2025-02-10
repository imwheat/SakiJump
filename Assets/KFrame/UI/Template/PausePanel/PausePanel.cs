//****************** 代码文件申明 ***********************
//* 文件：PausePanel
//* 作者：wheat
//* 创建时间：2024/10/01 10:16:18 星期二
//* 描述：游戏的暂停面板
//*******************************************************

#if UNITY_EDITOR

using UnityEditor;

#endif

using GameBuild;
using GameBuild.Player;
using TMPro;
using UnityEngine;


namespace KFrame.UI
{
    [UIData(typeof(PausePanel), "PausePanel", true, 3)]
    public class PausePanel : UIPanelBase
    {
        #region UI配置

        /// <summary>
        /// 返回按钮
        /// </summary>
        [SerializeField] 
        private KButton returnBtn;
        /// <summary>
        /// 设置按钮
        /// </summary>
        [SerializeField] 
        private KButton settingsBtn;
        /// <summary>
        /// 返回菜单按钮
        /// </summary>
        [SerializeField] 
        private KButton menuBtn;
        /// <summary>
        /// 退出按钮
        /// </summary>
        [SerializeField] 
        private KButton exitBtn;
        /// <summary>
        /// 信息面板
        /// </summary>
        [SerializeField]
        private GameObject infoPanel;
        /// <summary>
        /// 游玩时间
        /// </summary>
        [SerializeField]
        private TMP_Text playTime;
        /// <summary>
        /// 跳跃次数
        /// </summary>
        [SerializeField]
        private TMP_Text jumpCount;
        /// <summary>
        /// 倒地次数
        /// </summary>
        [SerializeField]
        private TMP_Text fallCount; 

        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            returnBtn.OnClick.AddListener(OnClickReturnBtn);
            settingsBtn.OnClick.AddListener(OnClickSettingsBtn);
            menuBtn.OnClick.AddListener(OnClickMainMenuBtn);
            exitBtn.OnClick.AddListener(OnClickExitBtn);
        }

        protected override void OnShow()
        {
            base.OnShow();
            
            UpdateInfoPanel();
        }

        protected override void ClosePanel()
        {
            if (string.IsNullOrEmpty(switchPanelKey))
            {
                Time.timeScale = 1f;
            }
            
            base.ClosePanel();

        }

        #region UI相关

        /// <summary>
        /// 更新信息面板
        /// </summary>
        private void UpdateInfoPanel()
        {
            //如果没有玩家那就隐藏信息面板
            if (PlayerController.Instance == null)
            {
                infoPanel.SetActive(false);
                return;
            }
            infoPanel.SetActive(true);
            
            //获取玩家信息
            PlayData data = PlayerController.Instance.PlayData;

            //计算游玩时间
            int hour = (int)data.playTime / 3600;
            int minutes = (int)(data.playTime - hour * 60) / 60;
            int seconds = (int)data.playTime % 60;
            
            //更新游玩数据UI
            playTime.text = hour + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            jumpCount.text = data.jumpCount.ToString();
            fallCount.text = data.fallCount.ToString();
        }

        #endregion
        
        #region 按键事件

        private void OnClickReturnBtn()
        {
            Time.timeScale = 1f;
            Close();
        }
        /// <summary>
        /// 按下设置按钮
        /// </summary>
        private void OnClickSettingsBtn()
        {
            SwitchToThisPanel<SettingsPanel>(true);
        }
        /// <summary>
        /// 关闭窗口然后返回主菜单
        /// </summary>
        private void OnClickMainMenuBtn()
        {
            Close();
            GameManager.ReturnMainMenu();
        }
        /// <summary>
        /// 按下退出按钮
        /// </summary>
        private void OnClickExitBtn()
        {
            GameManager.QuitGame();
        }

        #endregion
    }
}

