//****************** 代码文件申明 ***********************
//* 文件：BuildInit
//* 作者：wheat
//* 创建时间：2025/02/10 14:27:17 星期一
//* 描述：打包场景的Init
//*******************************************************

using KFrame.Systems;
using UnityEngine;

namespace GameBuild
{
    public class BuildInit : MonoBehaviour
    {
        private void Start()
        {
            ResSystem.LoadScene(GameManager.MainMenuScenePath);
        }
    }
}

