using UnityEngine;

namespace Tetris.View
{
    [ExecuteInEditMode]
    public class GridDebug : MonoBehaviour
    {
        public Grid grid;
        public Vector2Int size;
        // Update is called once per frame
        void Update()
        {
            Vector3 origin = grid.CellToWorld(new Vector3Int(0, 0, 0));
            Vector3 topRight = grid.CellToWorld(new Vector3Int(size.x, size.y, 0)) - grid.cellGap;
            Vector3 downRight = new Vector3(topRight.x, origin.y, 0f);
            Vector3 topLeft = new Vector3(origin.x, topRight.y, 0f);
            Debug.DrawLine(origin, topRight, Color.white);
            Debug.DrawLine(origin, topLeft, Color.green);
            Debug.DrawLine(origin, downRight, Color.red);
            Debug.DrawLine(topLeft, topRight, Color.white);
            Debug.DrawLine(topRight, downRight, Color.white);
        }
    } 
}
