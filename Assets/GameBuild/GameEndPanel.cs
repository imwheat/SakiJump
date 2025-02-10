//****************** 代码文件申明 ***********************
//* 文件：GameEndPanel
//* 作者：wheat
//* 创建时间：2025/02/10 18:57:32 星期一
//* 描述：游戏结束面板
//*******************************************************

using GameBuild.Player;
using KFrame.Systems;
using KFrame.UI;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace GameBuild
{
    [UIData(typeof(GameEndPanel), "GameEndPanel", true, 3)]
    public class GameEndPanel : UIPanelBase
    {
        #region UI配置

        /// <summary>
        /// 开始新游戏按钮
        /// </summary>
        [SerializeField, LabelText("开始新游戏按钮")]
        private KButton startNewGameButton;
        /// <summary>
        /// 返回主菜单按钮
        /// </summary>
        [SerializeField, LabelText("返回主菜单按钮")]
        private KButton returnMenuButton;
        /// <summary>
        /// 退出游戏按钮
        /// </summary>
        [SerializeField, LabelText("退出游戏按钮")]
        private KButton quitGameButton;
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
        
        #region 生命周期

        protected override void Awake()
        {
            base.Awake();
            
            startNewGameButton.OnClick.AddListener(OnClickStartNewGame);
            returnMenuButton.OnClick.AddListener(OnClickReturnMenu);
            quitGameButton.OnClick.AddListener(OnClickQuitGame);
        }

        protected override void OnShow()
        {
            base.OnShow();
            //更新游戏信息
            UpdateInfo();
            
            //删除清空存档
            GameSaveManager.CurPlayData = null;
            SaveSystem.DeleteGlobalSave(GameSaveManager.SAVE_FILENAME);
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

        #region UI相关

        /// <summary>
        /// 更新游戏信息
        /// </summary>
        private void UpdateInfo()
        {
            //如果没有信息那就返回
            if (GameSaveManager.CurPlayData == null)
            {
                return;
            }
            
            //获取玩家信息
            PlayData data = GameSaveManager.CurPlayData;

            //计算游玩时间
            int hour = (int)data.playTime / 3600;
            int minutes = (int)(data.playTime - hour * 60) / 60;
            int seconds = (int)data.playTime % 60;
            
            //更新游玩数据UI
            playTime.text = hour + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            jumpCount.text = data.jumpCount.ToString();
            fallCount.text = data.fallCount.ToString();
        }

        /// <summary>
        /// 开始新游戏
        /// </summary>
        private void OnClickStartNewGame()
        {
            GameManager.StartNewGame();
            Close();
        }
        /// <summary>
        /// 返回游戏
        /// </summary>
        private void OnClickReturnMenu()
        {
            GameManager.ReturnMainMenu();
            Close();
        }
        /// <summary>
        /// 退出游戏
        /// </summary>
        private void OnClickQuitGame()
        {
            GameManager.QuitGame();
            Close();
        }

        #endregion
    }
}

