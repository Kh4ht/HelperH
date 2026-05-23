// using KH;
// using UnityEditor;
// using UnityEngine;

// [CustomEditor(typeof(KHUIAnimator))]
// public class KHUIAnimatorEditor : Editor
// {
//     private static Texture2D icon;

//     private bool frameFoldout = true;
//     private bool buttonFoldout = true;
//     private bool animationFoldout = true;
//     private bool soundFoldout = true;

//     private SerializedProperty enableFrameAnimationSettingsProp;
//     private SerializedProperty framesProp;
//     private SerializedProperty frameRateProp;
//     private SerializedProperty loopProp;
//     private SerializedProperty playOnStartProp;

//     private SerializedProperty enableButtonSettingsProp;
//     private SerializedProperty onClickProp;

//     private SerializedProperty enableAnimationSettingsProp;
//     private SerializedProperty openOrCloseAnimationProp;
//     private SerializedProperty transitionProp;
//     private SerializedProperty highlightedColorProp;
//     private SerializedProperty pressedColorProp;
//     private SerializedProperty fadeDurationProp;

//     private SerializedProperty animateOnClickProp;
//     private SerializedProperty clickScaleAmountProp;
//     private SerializedProperty clickTweenDurationProp;
//     private SerializedProperty clickTweenEaseProp;

//     private SerializedProperty animateOnHoverProp;
//     private SerializedProperty hoverScaleAmountProp;
//     private SerializedProperty hoverTweenDurationProp;
//     private SerializedProperty hoverTweenEaseProp;

//     private SerializedProperty enableSoundSettingsProp;
//     private SerializedProperty clickSoundProp;
//     private SerializedProperty soundVolumeProp;

//     private readonly System.Collections.Generic.HashSet<string> _missingLogged = new();

//     private void TryFind(ref SerializedProperty prop, string name)
//     {
//         prop = serializedObject.FindProperty(name);
//         if (prop == null && !_missingLogged.Contains(name))
//         {
//             Debug.LogWarning($"[UIAnimatorEditor] Missing SerializedProperty: {name}");
//             _missingLogged.Add(name);
//         }
//     }

//     private void OnEnable()
//     {
//         if (icon == null)
//         {
//             // Loads icon relative to this editor script
//             string scriptPath = AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(this));
//             string folderPath = System.IO.Path.GetDirectoryName(scriptPath);
//             icon = AssetDatabase.LoadAssetAtPath<Texture2D>(folderPath + "/KHUIAnimator.png");
//         }

//         if (icon != null)
//             EditorGUIUtility.SetIconForObject(target, icon);
//         _missingLogged.Clear();

//         TryFind(ref enableFrameAnimationSettingsProp, "enableFrameAnimationSettings");
//         TryFind(ref framesProp, "frames");
//         TryFind(ref frameRateProp, "frameRate");
//         TryFind(ref loopProp, "loop");
//         TryFind(ref playOnStartProp, "playOnStart");

//         TryFind(ref enableButtonSettingsProp, "enableButtonSettings");
//         TryFind(ref onClickProp, "onClick");

//         TryFind(ref enableAnimationSettingsProp, "enableAnimationSettings");
//         TryFind(ref openOrCloseAnimationProp, "openOrCloseAnimation");
//         TryFind(ref transitionProp, "transition");
//         TryFind(ref highlightedColorProp, "highlightedColor");
//         TryFind(ref pressedColorProp, "pressedColor");
//         TryFind(ref fadeDurationProp, "fadeDuration");

//         TryFind(ref animateOnClickProp, "animateOnClick");
//         TryFind(ref clickScaleAmountProp, "clickScaleAmount");
//         TryFind(ref clickTweenDurationProp, "clickTweenDuration");
//         TryFind(ref clickTweenEaseProp, "clickTweenEase");

//         TryFind(ref animateOnHoverProp, "animateOnHover");
//         TryFind(ref hoverScaleAmountProp, "hoverScaleAmount");
//         TryFind(ref hoverTweenDurationProp, "hovertweenDuration");
//         TryFind(ref hoverTweenEaseProp, "hoverTweenEase");

//         TryFind(ref enableSoundSettingsProp, "enableSoundSettings");
//         TryFind(ref clickSoundProp, "clickSound");
//         TryFind(ref soundVolumeProp, "soundVolume");
//     }

//     public override void OnInspectorGUI()
//     {
//         serializedObject.Update();

//         DrawFrameAnimationSection();
//         EditorGUILayout.Space(6);
//         DrawButtonSection();
//         EditorGUILayout.Space(6);
//         DrawAnimationSection();
//         EditorGUILayout.Space(6);
//         DrawSoundSection();

//         serializedObject.ApplyModifiedProperties();
//     }

//     private void DrawFrameAnimationSection()
//     {
//         bool frameEnabled = enableFrameAnimationSettingsProp?.boolValue ?? false;
//         EditorGUILayout.BeginVertical("box");
//         frameEnabled = EditorGUILayout.BeginToggleGroup("🎞 Sprite Frame Animation", frameEnabled);
//         if (enableFrameAnimationSettingsProp != null)
//             enableFrameAnimationSettingsProp.boolValue = frameEnabled;

//         if (frameEnabled)
//         {
//             frameFoldout = EditorGUILayout.Foldout(frameFoldout, "Frame Animation Settings", true);
//             if (frameFoldout)
//             {
//                 EditorGUI.indentLevel++;
//                 EditorGUILayout.PropertyField(framesProp);
//                 EditorGUILayout.PropertyField(frameRateProp);
//                 EditorGUILayout.PropertyField(loopProp);
//                 EditorGUILayout.PropertyField(playOnStartProp);
//                 EditorGUI.indentLevel--;
//             }
//         }

//         EditorGUILayout.EndToggleGroup();
//         EditorGUILayout.EndVertical();
//     }

//     private void DrawButtonSection()
//     {
//         bool buttonEnabled = enableButtonSettingsProp?.boolValue ?? false;
//         EditorGUILayout.BeginVertical("box");
//         buttonEnabled = EditorGUILayout.BeginToggleGroup("⚙️ Button", buttonEnabled);
//         if (enableButtonSettingsProp != null)
//             enableButtonSettingsProp.boolValue = buttonEnabled;

//         if (buttonEnabled)
//         {
//             buttonFoldout = EditorGUILayout.Foldout(buttonFoldout, "Button Settings", true);
//             if (buttonFoldout)
//             {
//                 EditorGUI.indentLevel++;
//                 EditorGUILayout.PropertyField(onClickProp);
//                 EditorGUI.indentLevel--;
//             }
//         }

//         EditorGUILayout.EndToggleGroup();
//         EditorGUILayout.EndVertical();
//     }

//     private void DrawAnimationSection()
//     {
//         bool animEnabled = enableAnimationSettingsProp?.boolValue ?? false;
//         EditorGUILayout.BeginVertical("box");
//         animEnabled = EditorGUILayout.BeginToggleGroup("🎨 Animation Settings", animEnabled);
//         if (enableAnimationSettingsProp != null)
//             enableAnimationSettingsProp.boolValue = animEnabled;

//         if (animEnabled)
//         {
//             animationFoldout = EditorGUILayout.Foldout(animationFoldout, "Animation Settings", true);
//             if (animationFoldout)
//             {
//                 EditorGUI.indentLevel++;

//                 // ✅ Added openOrCloseAnimation field
//                 if (openOrCloseAnimationProp != null)
//                     EditorGUILayout.PropertyField(openOrCloseAnimationProp, new GUIContent("Open/Close Animation"));

//                 EditorGUILayout.Space(6);

//                 if (transitionProp != null)
//                     EditorGUILayout.PropertyField(transitionProp);

//                 bool showColorFields = transitionProp != null &&
//                     (KHUIAnimator.TransitionType)transitionProp.enumValueIndex == KHUIAnimator.TransitionType.ColorTint;

//                 if (showColorFields)
//                 {
//                     EditorGUI.indentLevel++;
//                     EditorGUILayout.PropertyField(highlightedColorProp);
//                     EditorGUILayout.PropertyField(pressedColorProp);
//                     EditorGUILayout.PropertyField(fadeDurationProp);
//                     EditorGUI.indentLevel--;
//                 }

//                 EditorGUILayout.Space(6);

//                 bool clickEnabled = animateOnClickProp?.boolValue ?? false;
//                 clickEnabled = EditorGUILayout.ToggleLeft("🖱 Enable Click Animation", clickEnabled, EditorStyles.boldLabel);
//                 if (animateOnClickProp != null) animateOnClickProp.boolValue = clickEnabled;
//                 if (clickEnabled)
//                 {
//                     EditorGUI.indentLevel++;
//                     EditorGUILayout.PropertyField(clickScaleAmountProp);
//                     EditorGUILayout.PropertyField(clickTweenDurationProp);
//                     EditorGUILayout.PropertyField(clickTweenEaseProp);
//                     EditorGUI.indentLevel--;
//                 }

//                 EditorGUILayout.Space(6);

//                 bool hoverEnabled = animateOnHoverProp?.boolValue ?? false;
//                 hoverEnabled = EditorGUILayout.ToggleLeft("🖐 Enable Hover Animation", hoverEnabled, EditorStyles.boldLabel);
//                 if (animateOnHoverProp != null) animateOnHoverProp.boolValue = hoverEnabled;
//                 if (hoverEnabled)
//                 {
//                     EditorGUI.indentLevel++;
//                     EditorGUILayout.PropertyField(hoverScaleAmountProp);
//                     EditorGUILayout.PropertyField(hoverTweenDurationProp);
//                     EditorGUILayout.PropertyField(hoverTweenEaseProp);
//                     EditorGUI.indentLevel--;
//                 }

//                 EditorGUI.indentLevel--;
//             }
//         }

//         EditorGUILayout.EndToggleGroup();
//         EditorGUILayout.EndVertical();
//     }

//     private void DrawSoundSection()
//     {
//         bool soundEnabled = enableSoundSettingsProp?.boolValue ?? false;
//         EditorGUILayout.BeginVertical("box");
//         soundEnabled = EditorGUILayout.BeginToggleGroup("🎵 Sound", soundEnabled);
//         if (enableSoundSettingsProp != null)
//             enableSoundSettingsProp.boolValue = soundEnabled;

//         if (soundEnabled)
//         {
//             soundFoldout = EditorGUILayout.Foldout(soundFoldout, "Sound Settings", true);
//             if (soundFoldout)
//             {
//                 EditorGUI.indentLevel++;
//                 EditorGUILayout.PropertyField(clickSoundProp);
//                 EditorGUILayout.PropertyField(soundVolumeProp);
//                 EditorGUI.indentLevel--;
//             }
//         }

//         EditorGUILayout.EndToggleGroup();
//         EditorGUILayout.EndVertical();
//     }
// }
