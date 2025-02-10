//****************** 代码文件申明 ***********************
//* 文件：GameSaveManager
//* 作者：wheat
//* 创建时间：2025/02/10 14:00:44 星期一
//* 描述：游戏存档管理器
//*******************************************************

using GameBuild.Player;
using KFrame.Systems;

namespace GameBuild
{
    public static class GameSaveManager
    {
        /// <summary>
        /// 保存存档路径
        /// </summary>
        public const string SAVE_FILENAME = "PlayData.sav";
        /// <summary>
        /// 当前的存档
        /// </summary>
        public static PlayData CurPlayData;

        /// <summary>
        /// 保存游戏
        /// </summary>
        public static void SaveGame()
        {
            //如果当前存档为空那就返回
            if(CurPlayData == null) return;
            
            //保存
            SaveSystem.SaveGlobalSave(CurPlayData, SAVE_FILENAME);
        }
        /// <summary>
        /// 加载存档
        /// </summary>
        public static void LoadGame()
        {
            CurPlayData = SaveSystem.LoadGlobalSave<PlayData>(SAVE_FILENAME);
        }
    }
}

