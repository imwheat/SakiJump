//****************** 代码文件申明 ***********************
//* 文件：MainMenuPanel
//* 作者：wheat
//* 创建时间：2025/02/10 14:32:16 星期一
//* 描述：主菜单面板UI
//*******************************************************

using KFrame.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameBuild
{
    [UIData(typeof(MainMenuPanel), "MainMenuPanel", true, 3)]
    public class MainMenuPanel : UIPanelBase
    {
        #region UI配置

        /// <summary>
        /// 继续游戏按钮
        /// </summary>
        [SerializeField, LabelText("继续游戏按钮")]
        private KButton continueGameButton;
        /// <summary>
        /// 开始新游戏按钮
        /// </summary>
        [SerializeField, LabelText("开始新游戏按钮")]
        private KButton startNewGameButton;
        /// <summary>
        /// 游戏设置按钮
        /// </summary>
        [SerializeField, LabelText("游戏设置按钮")]
        private KButton settingsButton;
        /// <summary>
        /// 退出游戏按钮
        /// </summary>
        [SerializeField, LabelText("退出游戏按钮")]
        private KButton quitGameButton;

        #endregion

        #region 生命周期

        protected override void Awake()
        {
            base.Awake();
            
            //注册按钮事件
            continueGameButton.OnClick.AddListener(OnClickContinueGame);
            startNewGameButton.OnClick.AddListener(OnClickStartNewGame);
            settingsButton.OnClick.AddListener(OnClickSettings);
            quitGameButton.OnClick.AddListener(OnClickQuitGame);
        }

        protected override void OnShow()
        {
            base.OnShow();

            //如果当前游戏存档为空，那么继续游戏按钮不显示
            continueGameButton.gameObject.SetActive(GameSaveManager.CurPlayData != null);
        }

        #endregion
        
        #region 方法重写

        /// <summary>
        /// 主菜单面板点击ESC没有用
        /// </summary>
        public override void OnPressESC()
        {

        }

        #endregion

        #region UI事件方法

        /// <summary>
        /// 继续游戏
        /// </summary>
        private void OnClickContinueGame()
        {
            Close();
            GameManager.StartGame();
        }
        /// <summary>
        /// 新游戏
        /// </summary>
        private void OnClickStartNewGame()
        {
            Close();
            GameManager.StartNewGame();
        }
        /// <summary>
        /// 游戏设置
        /// </summary>
        private void OnClickSettings()
        {
            SwitchToThisPanel<SettingsPanel>(true);
        }
        /// <summary>
        /// 退出
        /// </summary>
        private void OnClickQuitGame()
        {
            GameManager.QuitGame();
        }

        #endregion

    }
}

