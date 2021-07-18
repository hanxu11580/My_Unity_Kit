
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace XhO_OKit
{
    /// <summary>
    /// 批量替换预支体
    /// </summary>
    public class BatchReplaceWindow : EditorWindow
    {
        ReplaceData data;
        SerializedObject serializedData;
        SerializedProperty replaceObjectField;

        Vector2 selectObjectScrollPosition;

        [MenuItem("XhO_OKit/Tools/BatchReplacePrefab")]
        public static void ShowWnd()
        {
            var wnd = GetWindow<BatchReplaceWindow>("BatchReplacePrefab");
            wnd.Show();
        }

        private void InitDataIfNeeded()
        {
            if (!data)
            {
                data = ScriptableObject.CreateInstance<ReplaceData>();
                serializedData = null;
            }
            if (serializedData == null)
            {
                serializedData = new SerializedObject(data);
                replaceObjectField = null;
            }
            if (replaceObjectField == null)
            {
                replaceObjectField = serializedData.FindProperty("replaceObject");
            }
        }


        private void OnGUI()
        {
            InitDataIfNeeded();
            //标题
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(new GUIContent(""));
            EditorGUILayout.Separator();

            EditorGUI.indentLevel += 2;
            EditorGUILayout.PropertyField(replaceObjectField, new GUIContent("替换预制体"));
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("选择替换掉的对象", EditorStyles.boldLabel);
            EditorGUILayout.Separator();

            GUI.enabled = false;
            //以下开始无法编辑
            int objectToReplaceCount = data.objectsToReplace != null ? data.objectsToReplace.Length : 0;
            EditorGUILayout.IntField("选择数量", objectToReplaceCount);
            EditorGUI.indentLevel++;
            if (objectToReplaceCount == 0)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("无", EditorStyles.wordWrappedLabel);
            }
            selectObjectScrollPosition = EditorGUILayout.BeginScrollView(selectObjectScrollPosition);
            if (data && data.objectsToReplace != null)
            {
                foreach (var go in data.objectsToReplace)
                {
                    EditorGUILayout.ObjectField(go, typeof(GameObject), true);
                }
            }
            GUI.enabled = true;
            //无法编辑结束
            EditorGUILayout.EndScrollView();
            EditorGUI.indentLevel--;
            EditorGUILayout.Separator();

            if (GUILayout.Button("Replace", GUILayout.Height(100)))
            {
                if (!replaceObjectField.objectReferenceValue)
                {
                    KitLog.Error("[替换预制体未设置]");
                    return;
                }
                if (data.objectsToReplace.Length == 0)
                {
                    KitLog.Error("[未选择被替换对象]");
                    return;
                }
                ReplaceSelectedObjects(data.objectsToReplace, data.replaceObject);
            }
            EditorGUILayout.Separator();
            serializedData.ApplyModifiedProperties();
        }

        private void OnSelectionChange()
        {
            //选择对象改变
            InitDataIfNeeded();
            SelectionMode objectFilter = SelectionMode.Unfiltered ^ ~(SelectionMode.Assets | SelectionMode.DeepAssets | SelectionMode.Deep);
            Transform[] selection = Selection.GetTransforms(objectFilter);

            data.objectsToReplace = selection.Select(s => s.gameObject).ToArray();
            if (serializedData.UpdateIfRequiredOrScript())
            {
                this.Repaint();
            }
        }

        private void OnInspectorUpdate()
        {
            //预制体改变
            if (serializedData != null && serializedData.UpdateIfRequiredOrScript())
            {
                this.Repaint();
            }
        }

        private void ReplaceSelectedObjects(GameObject[] objectToReplace, GameObject replaceObject)
        {
            for (int i = 0; i < objectToReplace.Length; i++)
            {
                var go = objectToReplace[i]; //原对象
                // 可撤销操作
                Undo.RegisterCompleteObjectUndo(go, "Saving game object state"); //记录对象完整状态的拷贝
                //这样实例化 相当于将预制体拖入Hierarchy 这样更改预制体 这个替换的预制体也会反应
                var inst = PrefabUtility.InstantiatePrefab(replaceObject) as GameObject;
                inst.transform.SetParent(go.transform.parent);
                inst.transform.localScale = go.transform.localScale;
                inst.transform.rotation = go.transform.rotation;
                inst.transform.position = go.transform.position;
                //标记可撤销操作
                Undo.RegisterCreatedObjectUndo(inst, "Replacement creation.");
                foreach (Transform child in go.transform)
                {
                    Undo.SetTransformParent(child, inst.transform, "Parent Change");
                }
                Undo.DestroyObjectImmediate(go);
            }
        }
    }
}