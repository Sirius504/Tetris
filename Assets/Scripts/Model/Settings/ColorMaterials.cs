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
            var colorsWithMissingMaterials = GetColorsWithMissingMaterials();
            if (colorsWithMissingMaterials.Count > 0)
                throw new MissingMemberException($"Missing materials for colors: {string.Join(", ", colorsWithMissingMaterials)}");
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
            try
            {
                foreach (var kvp in colorMaterialsArray)
                    colorMaterials.Add(kvp.color, kvp.material);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException($"Duplicate color in Color Materials", e);
            }
        }

        public List<CellColorsEnum> GetColorsWithMissingMaterials()
        {
            InitializeMaterialsDictionary();
            var result = new List<CellColorsEnum>();
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
                    result.Add(color);
                }
                catch (MissingMemberException)
                {
                    result.Add(color);
                }
            }
            return result;
        }
    }
}