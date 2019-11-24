using Tetris.Installers;
using UnityEditor;
using UnityEngine;

namespace Tetris.Inspector
{
    [CustomEditor(typeof(GridInstaller))]
    public class GridInspector : Editor
    {
        private GridInstaller gridInstaller;
        private Transform transform;
        private const string CELL_SIZE_PREFS_KEY = "cell_size";
        private float cellSize;

        private const float CELL_SIZE_MIN = 0.01f;

        private void OnEnable()
        {
            gridInstaller = (GridInstaller)target;
            transform = gridInstaller.transform;
            gridInstaller.GridSize = RecalculateGridSize();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            float cellMaxSize = Mathf.Min(transform.lossyScale.x, transform.lossyScale.y);
            EditorGUILayout.LabelField("Cell Size:");
            cellSize = EditorPrefs.GetFloat(CELL_SIZE_PREFS_KEY, 0.1f);
            cellSize = EditorGUILayout.Slider(cellSize, CELL_SIZE_MIN, cellMaxSize);
            EditorPrefs.SetFloat(CELL_SIZE_PREFS_KEY, cellSize);
            gridInstaller.GridSize = RecalculateGridSize();
            EditorGUILayout.LabelField("Grid Size:", gridInstaller.GridSize.ToString());
        }

        private Vector2Int RecalculateGridSize()
        {
            var scale = transform.lossyScale;
            int gridSizeX = Mathf.FloorToInt(scale.x / cellSize);
            int gridSizeY = Mathf.FloorToInt(scale.y / cellSize);
            return new Vector2Int(gridSizeX, gridSizeY);
        }
    }
}
