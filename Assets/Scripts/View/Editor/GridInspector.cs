using Tetris.View;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridRenderer))]
public class GridInspector : Editor
{
    private GridRenderer gridRenderer;
    private Transform transform;
    private Vector2Int gridSize;
    private float cellSize = 0.1f;

    private const float CELL_SIZE_MIN = 0.01f;

    private void OnEnable()
    {
        gridRenderer = (GridRenderer)target;
        transform = gridRenderer.transform;
        gridSize = RecalculateGridSize();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        float cellMaxSize = Mathf.Min(transform.lossyScale.x, transform.lossyScale.y);
        Debug.Log(cellMaxSize);
        cellSize = EditorGUILayout.Slider(cellSize, CELL_SIZE_MIN, cellMaxSize);
        gridSize = RecalculateGridSize();
        EditorGUILayout.LabelField("Grid Size:", gridSize.ToString());
    }   

    private Vector2Int RecalculateGridSize()
    {
        var scale = transform.lossyScale;
        int gridSizeX = Mathf.CeilToInt(scale.x / cellSize);
        int gridSizeY = Mathf.CeilToInt(scale.y / cellSize);
        return new Vector2Int(gridSizeX, gridSizeY);
    }
}
