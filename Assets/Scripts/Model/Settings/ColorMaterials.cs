using System;
using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;

namespace Tetris.Model.Settings
{
    [CreateAssetMenu(fileName = "ColorSettings", menuName = "Tetris/ColorSettings")]
    public class ColorMaterials : ScriptableObject
    {
        [Serializable]
        public class ColorMaterialPair
        {
            public CellColorsEnum color;
            public Material material;
        }

        [SerializeField]
        private ColorMaterialPair[] colorMaterialsArray;
        private Dictionary<CellColorsEnum, Material> colorMaterials;

        public void OnEnable()
        {
            InitializeMaterialsDictionary();
            Validate();
        }

        public Material GetMaterial(CellColorsEnum color)
        {
            if (colorMaterials == null)
                InitializeMaterialsDictionary();

            return colorMaterials[color];
        }

        public void InitializeMaterialsDictionary()
        {
            colorMaterials = new Dictionary<CellColorsEnum, Material>();
            foreach (var kvp in colorMaterialsArray)
                colorMaterials.Add(kvp.color, kvp.material);            
        }

        private void Validate()
        {
            List<string> colorsWithoutMaterials = new List<string>();
            foreach (var color in (CellColorsEnum[])Enum.GetValues(typeof(CellColorsEnum)))
            {
                try
                {
                    var material = colorMaterials[color];
                    if (material == null)
                        throw new MissingMemberException();
                }
                catch (KeyNotFoundException)
                {
                    colorsWithoutMaterials.Add(color.ToString());
                }
                catch (MissingMemberException)
                {
                    colorsWithoutMaterials.Add(color.ToString());
                }
            }
            if (colorsWithoutMaterials.Count > 0)
                throw new MissingMemberException($"Missing materials for colors: {string.Join(", ", colorsWithoutMaterials)}");
        }
    }
}