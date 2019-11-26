using System.Collections;
using Tetris.Model;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class GridRenderer : MonoBehaviour
    {
        private TetrisGrid gridModel;
        private Cell.Pool cellPool;
        private Grid grid;

        [SerializeField]
        private Transform cellPrefab = default;
        private Vector3 cellSpriteSize;

        [Inject]
        public void Construct(TetrisGrid gridModel, Cell.Pool cellPool, Grid grid)
        {
            this.gridModel = gridModel;
            this.cellPool = cellPool;
            this.grid = grid;
        }

        private void Start()
        {
            cellSpriteSize = cellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
            StartCoroutine(DrawGrid());
        }

        private IEnumerator DrawGrid()
        {
            for (int j = 0; j < gridModel.Size.y; j++)
                for (int i = 0; i < gridModel.Size.x; i++)
                {
                    var cell = cellPool.Spawn();
                    cell.transform.SetParent(transform);
                    cell.SetLocalPosition(grid.CellToLocal(new Vector3Int(i, j, 0)));
                    cell.SetLocalScale(grid.cellSize);
                    cell.gameObject.SetActive(true);
                    yield return new WaitForSeconds(.01f);
                }
        }
    }
}