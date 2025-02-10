//****************** 代码文件申明 ***********************
//* 文件：GameManager
//* 作者：wheat
//* 创建时间：2025/02/10 14:15:24 星期一
//* 描述：游戏管理器
//*******************************************************


using GameBuild.Player;
using KFrame.Systems;
using KFrame.UI;
using KFrame.Utilities;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace GameBuild
{
    [DefaultExecutionOrder(-1)]
    public class GameManager
    {
        #region 属性

        /// <summary>
        /// 主菜单场景路径
        /// </summary>
        public const string MainMenuScenePath = "MainMenu";
        /// <summary>
        /// 游玩场景路径
        /// </summary>
        public const string GamePlayScenePath = "GamePlay";

        #endregion

        #region 游戏进度管理

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Init()
        {
            //加载存档
            GameSaveManager.LoadGame();
            Application.quitting += SavePlayerData;
        }
        /// <summary>
        /// 开始游戏
        /// </summary>
        public static void StartGame()
        {
            ResSystem.LoadScene(GamePlayScenePath);
        }
        /// <summary>
        /// 开始新游戏
        /// </summary>
        public static void StartNewGame()
        {
            GameSaveManager.CurPlayData = null;
            StartGame();
        }
        /// <summary>
        /// 游戏通关
        /// </summary>
        public static void GameClear()
        {
            Time.timeScale = 0f;
            UISystem.Show<GameEndPanel>();
        }
        /// <summary>
        /// 保存玩家信息
        /// </summary>
        private static void SavePlayerData()
        {
            //如果玩家不为空那就存档
            if (PlayerController.Instance != null && GameSaveManager.CurPlayData != null)
            {
                //保存玩家位置
                GameSaveManager.CurPlayData.playerPos = PlayerController.Instance.transform.position;
            }
            
            //保存数据
            GameSaveManager.SaveGame();

        }
        /// <summary>
        /// 返回主菜单
        /// </summary>
        public static void ReturnMainMenu()
        {
            //保存玩家信息
            SavePlayerData();

            //将玩家推进对象池
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.gameObject.GameObjectPushPool();
            }
            
            //加载主菜单场景
            ResSystem.LoadScene(MainMenuScenePath);
        }
        /// <summary>
        /// 退出游戏
        /// </summary>
        public static void QuitGame()
        {
            
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                EditorApplication.ExitPlaymode();
            }
#else
            Application.Quit();
#endif

        }

        #endregion
        
    }
}

