//****************** 代码文件申明 ***********************
//* 文件：PlayData
//* 作者：wheat
//* 创建时间：2025/02/09 20:07:05 星期日
//* 描述：玩家游玩数据
//*******************************************************

namespace GameBuild.Player
{
    [System.Serializable]
    public class PlayData
    {
        /// <summary>
        /// 游玩时间
        /// </summary>
        public float playTime;
        /// <summary>
        /// 跳跃次数
        /// </summary>
        public int jumpCount;
        /// <summary>
        /// 倒地次数
        /// </summary>
        public int fallCount;
    }
}

