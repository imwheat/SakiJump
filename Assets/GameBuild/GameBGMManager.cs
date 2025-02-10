//****************** 代码文件申明 ***********************
//* 文件：GameBGMManager
//* 作者：wheat
//* 创建时间：2025/02/10 19:38:18 星期一
//* 描述：
//*******************************************************

using System;
using KFrame.Systems;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameBuild
{
    [DefaultExecutionOrder(10)]
    public class GameBGMManager : MonoBehaviour
    {
        private int preBGMIndex = -1;
        private AudioPlay bgmPlay;

        private void Update()
        {
            if (bgmPlay == null || bgmPlay.gameObject.activeSelf == false)
            {
                PlayRandomBGM();
            }
        }

        /// <summary>
        /// 随机播放BGM
        /// </summary>
        public void PlayRandomBGM()
        {
            
            //随机获取一个不重复的BGM
            int k = Random.Range(0, 5);
            while (k == preBGMIndex)
            {
                k = Random.Range(0, 5);
            }
            preBGMIndex = k;
            
            //然后开始播放
            bgmPlay = AudioDic.PlayAudio(k + 200005, Vector3.zero, transform);
        }
    }
}

