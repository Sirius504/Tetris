using System;
using Tetris.Model.Enumerators;
using UnityEngine;
using Zenject;




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
    private ColorMaterialPair[] colorMaterials;

    //public Material GetMaterial(CellColorsEnum)
}
