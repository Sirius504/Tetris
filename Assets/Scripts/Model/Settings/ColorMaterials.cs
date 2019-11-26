using System;
using System.Collections.Generic;
using Tetris.Model.Enumerators;
using UnityEngine;


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
        
    public Material GetMaterial(CellColorsEnum color)
    {
        if (colorMaterials == null)
            InitializeMaterialsDictionary();

        return colorMaterials[color];
    }

    private void InitializeMaterialsDictionary()
    {
        colorMaterials = new Dictionary<CellColorsEnum, Material>();
        foreach(var kvp in colorMaterialsArray)        
            colorMaterials.Add(kvp.color, kvp.material);        
    }
}
