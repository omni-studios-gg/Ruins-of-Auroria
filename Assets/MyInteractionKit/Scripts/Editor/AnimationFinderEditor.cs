using UnityEngine;
using UnityEditor;

namespace MyInteractionKit
{
    [CustomEditor(typeof(AnimationFinder))]
    public class AnimationFinderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            AnimationFinder script = (AnimationFinder)target;

            if (script.animationClips != null && script.animationClips.Length > 0)
            {
                EditorGUILayout.LabelField("Animation Clips Found:", EditorStyles.boldLabel);

                foreach (AnimationClip clip in script.animationClips)
                {
                    EditorGUILayout.LabelField(clip.name);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No Animation Clips found.", MessageType.Info);
            }
        }
    }

}

