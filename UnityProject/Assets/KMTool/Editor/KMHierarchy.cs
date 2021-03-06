/****************************************************************************** 
 * 
 * Maintaince Logs: 
 * 2016-03-30       WP      Initial version
 * 2016-11-02       WP      加入菜单开关
 * 
 * *****************************************************************************/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


namespace KMTool
{
    
    /// <summary>
    /// Hierarchy 外观控制显示
    /// </summary>
    [InitializeOnLoad]
    public partial class KMHierarchy
    {
        const string KEY_NAME = "KMHierarchy";
        static bool isOpen = false;

        /// <summary>
        /// 已知BUG，在Unity长时间的脚本导入与编辑中，Unity将变慢，类似于内存泄漏，3小时以上出现过
        /// </summary>
        static KMHierarchy()
        {
            isOpen = EditorPrefs.GetBool(KEY_NAME, false);
            EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
        }

        private static void HierarchyItemCB(int instanceID, Rect selectionRect)
        {
            if (!isOpen)
                return;
            
            var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (go == null) return;

            DrawActive(go, selectionRect);

            DrawCombine(go, selectionRect);

            DrawMenu(go, selectionRect);
        }

        public static Texture2D GetTexture2D(string id)
        {
            Texture2D result;

            string path = "Assets/KMTool/Editor/Images/" + id + ".png";

            var ba = File.ReadAllBytes(path);
            result = new Texture2D(4, 4, TextureFormat.ARGB32, false) { hideFlags = HideFlags.HideAndDontSave };
            result.LoadImage(ba);

            return result;
        }

        /// <summary>
        /// 游戏对象Active相关信息
        /// </summary>
        private static void DrawActive(GameObject go, Rect SelectRect)
        {
            //此对象是否显示中
            bool isActiveInHierarchy = go.activeInHierarchy;

            string texName = isActiveInHierarchy ? "eye" : "eye_dis";

            Rect eyeRect = SelectRect.H_CR();

            GUI.DrawTexture(eyeRect, GetTexture2D(texName));
            if (GUI.Button(eyeRect, "", EditorStyles.label))
            {
                SetActiveInHierarchy(go, !isActiveInHierarchy);
            }
        }

        /// <summary>
        /// 设置对象显示状态
        /// </summary>
        private static void SetActiveInHierarchy(GameObject go, bool active)
        {
            if (go.activeSelf != active)
                go.SetActive(active);

            if (go.activeInHierarchy != active)
                SetActiveInHierarchy(go.transform.parent.gameObject, active);
        }

        /// <summary>
        /// 子对象相关信息
        /// </summary>
        private static void DrawCombine(GameObject go, Rect selectRect)
        {
            int childCount = go.transform.childCount;

            if (childCount == 0)
                return;

            Rect childRect = selectRect.H_CR(2, 6).H_Size(24);

            string text = childCount > 99 ? "99+" : childCount.ToString();

            //取子对象是否隐藏
            bool isHide = HasFlag(go.transform.GetChild(0), HideFlags.HideInHierarchy);

            if (isHide)
                childRect = selectRect.H_CR(2, 11).H_Size(22);

            if (GUI.Button(childRect, text, isHide ? EditorStyles.miniButton : EditorStyles.label))
            {
                bool isAdd = !isHide;
                SetChildrenFlag(go, HideFlags.HideInHierarchy, isAdd);
            }
        }

        private static void SetChildrenFlag(GameObject go, HideFlags flag, bool isAdd)
        {
            foreach (Transform t in go.transform)
            {
                if (isAdd)
                    t.hideFlags |= flag;
                else
                    t.hideFlags &= ~flag;
            }

            //Work on 
            bool old = go.activeSelf;
            go.SetActive(!old);
            go.SetActive(old);
        }

        private static bool HasFlag(Transform t, HideFlags flag)
        {
            return (t.hideFlags & flag) > 0;
        }

        /// <summary>
        /// 设置开关
        /// </summary>
        [MenuItem(KMMenu.MENU_SETTING_HEAD + "层级开关-开启")]
        static private void SetTurnOn()
        {
            EditorPrefs.SetBool(KEY_NAME, true);
            isOpen = true;
        }

        [MenuItem(KMMenu.MENU_SETTING_HEAD + "层级开关-关闭")]
        static private void SetTurnOff()
        {
            EditorPrefs.SetBool(KEY_NAME, false);
            isOpen = false;
        }
    }
}