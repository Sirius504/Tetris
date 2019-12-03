using System;
using System.Collections.Generic;
using Tetris.Model.Enumerators;
using Tetris.Model.Settings;
using UnityEditor;

namespace Tetris.CustomInspectors
{
    [CustomEditor(typeof(ColorMaterials))]
    public class ColorMaterialsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var colorMaterials = (ColorMaterials)target;
            List<CellColorsEnum> colorsWithMissingMaterials = null;
            try
            {
                colorsWithMissingMaterials = colorMaterials.GetColorsWithMissingMaterials();
            }
            catch (ArgumentException e)
            {
                EditorGUILayout.HelpBox(e.Message, MessageType.Error);
            }
            if (colorsWithMissingMaterials != null && colorsWithMissingMaterials.Count > 0)
                EditorGUILayout.HelpBox($"Missing materials for colors: {string.Join(", ", colorsWithMissingMaterials)}", MessageType.Error);
        }
    } 
}
