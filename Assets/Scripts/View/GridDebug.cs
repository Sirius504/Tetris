using UnityEngine;

namespace Tetris.View
{
    [ExecuteInEditMode]
    public class GridDebug : MonoBehaviour
    {
        public Grid grid;
               
        // Update is called once per frame
        void Update()
        {
            Vector3 origin = grid.CellToWorld(new Vector3Int(0, 0, 0)) - grid.cellSize / 2;
            Vector3 topRight = grid.CellToWorld(new Vector3Int(9, 19, 0)) + grid.cellSize / 2;
            Debug.DrawLine(origin, topRight, Color.red);
        }
    } 
}
