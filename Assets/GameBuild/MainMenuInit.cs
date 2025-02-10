//****************** 代码文件申明 ***********************
//* 文件：MainMenuInit
//* 作者：wheat
//* 创建时间：2025/02/10 14:31:29 星期一
//* 描述：主菜单初始化
//*******************************************************

using KFrame.UI;
using UnityEngine;

namespace GameBuild
{
    public class MainMenuInit : MonoBehaviour
    {
        private void Start()
        {
            Time.timeScale = 1f;
            UISystem.Show<MainMenuPanel>();
        }
    }
}

