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
            public MinoColorsEnum color;
            public Material material;
        }

        [SerializeField]
        private ColorMaterialPair[] colorMaterialsArray;
        private Dictionary<MinoColorsEnum, Material> colorMaterials;

        public void OnEnable()
        {
            InitializeMaterialsDictionary();
            var colorsWithMissingMaterials = GetColorsWithMissingMaterials();
            if (colorsWithMissingMaterials.Count > 0)
                throw new MissingMemberException($"Missing materials for colors: {string.Join(", ", colorsWithMissingMaterials)}");
        }

        public Material GetMaterial(MinoColorsEnum color)
        {
            if (colorMaterials == null)
                InitializeMaterialsDictionary();

            return colorMaterials[color];
        }

        public void InitializeMaterialsDictionary()
        {
            colorMaterials = new Dictionary<MinoColorsEnum, Material>();
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

        public List<MinoColorsEnum> GetColorsWithMissingMaterials()
        {
            InitializeMaterialsDictionary();
            var result = new List<MinoColorsEnum>();
            foreach (var color in (MinoColorsEnum[])Enum.GetValues(typeof(MinoColorsEnum)))
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