using UnityEditor;
using UnityEngine;

namespace MyInteractionKit
{
    [CustomEditor(typeof(SimpleTween))]
    public class SimpleTweenEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SimpleTween tweenScript = (SimpleTween)target;

            if (GUILayout.Button("Set Start Values"))
            {
                tweenScript.startPosition = tweenScript.transform.localPosition;
                tweenScript.startRotation = UnityEditor.TransformUtils.GetInspectorRotation(tweenScript.transform);
                tweenScript.startScale = tweenScript.transform.localScale;
            }

            if (GUILayout.Button("Set Destination Values"))
            {
                tweenScript.finishPosition = tweenScript.transform.localPosition;
                tweenScript.finishRotation = UnityEditor.TransformUtils.GetInspectorRotation(tweenScript.transform);
                tweenScript.finishScale = tweenScript.transform.localScale;
            }

            if (GUILayout.Button("Go to Start Position"))
            {
                tweenScript.GoToStartPosition();
            }

            if (GUILayout.Button("Go to End Position"))
            {
                tweenScript.GoToEndPosition();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }
}
