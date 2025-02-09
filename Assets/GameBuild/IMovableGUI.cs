//****************** 代码文件申明 ************************
//* 文件：IMovableGUI                                       
//* 作者：wheat
//* 创建时间：2023/11/20 18:58:51 星期一
//* 描述：在Scene上显示巡逻物体的UI方便编辑
//*****************************************************
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameBuild
{
    [ExecuteAlways]
    public class IMovableGUI : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField,LabelText("显示对象")] public IMovable Movable;

        private void Awake()
        {
            if (Movable == null) Movable = GetComponent<IMovable>();
        }
        private void OnEnable()
        {
            if (Movable == null) Movable = GetComponent<IMovable>();
        }

        #region 设置方法

        /// <summary>
        /// 添加新的巡逻位点
        /// </summary>
        [Button("添加新的移动位点", 30)]
        public void AddNewPartolSet()
        {
            if (Application.isPlaying)
            {
                Debug.Log("请在编辑器模式下更新设置");
                return;
            }

            //防空
            if (Movable == null) Movable = GetComponent<IMovable>();

            //生成新的位点
            GameObject p = new GameObject("P" + Movable.PartolSets.Count);
            p.transform.SetParent(transform);

            //设置位点位置
            if (Movable.PartolSets.Count == 0)
            {
                p.transform.position = Movable.transform.position;
            }
            else
            {
                p.transform.position = Movable.PartolSets[Movable.PartolSets.Count - 1].transform.position + new Vector3(3, 0);
            }
            PartolSet ps = new PartolSet(p);
            if (Movable.PartolSets.Count > 1)
            {
                Movable.PartolSets[^1].StopPos = false;
            }
            ps.StopPos = true;
            //添加入列表
            Movable.PartolSets.Add(ps);
            //选中新的位点
            Selection.activeGameObject = p;

            //保存
            EditorUtility.SetDirty((UnityEngine.Object)Movable);
        }
        /// <summary>
        /// 清空所有的移动位点
        /// </summary>
        [Button("清空所有的移动位点", 30)]
        public void ClearAllPartolSet()
        {
            if (Application.isPlaying)
            {
                Debug.Log("请在编辑器模式下更新设置");
                return;
            }

            //防空
            if (Movable == null) Movable = gameObject.GetComponent<IMovable>();

            for (int i = 0; i < Movable.PartolSets.Count; i++)
            {
                DestroyImmediate(Movable.PartolSets[i].obj);
            }
            Movable.PartolSets.Clear();

            //保存
            EditorUtility.SetDirty((UnityEngine.Object)Movable);
        }


        #endregion


        private void OnDrawGizmos()
        {
            //只有选择该物体或者子集的时候显示
            if (!(Selection.activeGameObject == gameObject || (Selection.activeGameObject != null && Selection.activeGameObject.GetComponentInParent<IMovableGUI>() == this))) return;

            //先获取脚本
            IMovableGUI gui = this;
            if (gui.Movable == null) gui.Movable = gui.gameObject.GetComponent<IMovable>();
            IMovable moveable = gui.Movable;
            //获取不到就返回
            if (moveable == null) return;

            if (Application.isPlaying)
            {
                //绘制线段
                for (int i = 0; i < moveable.partolPoses.Count; i++)
                {
                    int link = (i + 1) % moveable.partolPoses.Count;

                    Handles.color = Color.red;

                    Handles.DrawLine(moveable.partolPoses[i] + moveable.offset, moveable.partolPoses[link] + moveable.offset);

                    Handles.Label(moveable.partolPoses[i], i.ToString(), new GUIStyle() { normal = new GUIStyleState() { textColor = Color.white } });
                }
            }
            else
            {
                //防空
                for (int i = 0; i < moveable.PartolSets.Count; i++)
                {
                    if (moveable.PartolSets[i] == null)
                    {
                        moveable.PartolSets.RemoveAt(i);
                        i--;
                    }
                }

                //绘制线段
                for (int i = 0; i < moveable.PartolSets.Count; i++)
                {
                    int link = (i + 1) % moveable.PartolSets.Count;

                    Handles.color = Color.red;

                    Debug.DrawLine(moveable.PartolSets[i].transform.position, moveable.PartolSets[link].transform.position, Color.red);

                    DrawBox(moveable.PartolSets[i].transform.position, Color.yellow);

                    Handles.Label(moveable.PartolSets[i].transform.position, i.ToString(), new GUIStyle() { normal = new GUIStyleState() { textColor = Color.white } });
                }
            }

        }
        private void DrawBox(Vector3 pos, Color color)
        {
            Handles.color = color;
            pos.x = Mathf.FloorToInt(pos.x) + 0.5f;
            pos.y = Mathf.FloorToInt(pos.y) + 0.5f;

            Handles.DrawWireCube(pos, new Vector3(1f, 1f));
        }




#endif


    }
    /// <summary>
    /// 停靠点
    /// </summary>
    [System.Serializable]
    public class PartolSet
    {
        [LabelText("GameObject")] public GameObject obj;
        [LabelText("是否永久停靠")] public bool StopPos;
        [LabelText("停靠时间")] public float StopTime;
        public Transform transform { get { return obj == null ? null : obj.transform; } }
        public PartolSet(GameObject obj)
        {
            this.obj = obj;
        }
    }
}

