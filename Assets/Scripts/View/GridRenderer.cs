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
        //private Cell[,] cells;
        private Grid gridComponent;

        [SerializeField]
        private Transform cellPrefab = default;
        private Vector3 cellSpriteSize;

        [Inject]
        public void Construct(TetrisGrid gridModel, Cell.Pool cellPool, Grid gridComponent)
        {
            this.gridModel = gridModel;
            this.cellPool = cellPool;
            this.gridComponent = gridComponent;
            //cells = new Cell[gridModel.Size.x, gridModel.Size.y];
        }

        private void Start()
        {
            StartCoroutine(DrawGrid());
        }

        private IEnumerator DrawGrid()
        {
            for (int j = 0; j < gridModel.Size.y; j++)
                for (int i = 0; i < gridModel.Size.x; i++)
                {
                    var cellData = gridModel.Cells[i, j];
                    if (cellData != null)                    
                        SpawnCell(i, j, cellData);                    
                    yield return new WaitForSeconds(.01f);
                }
        }

        private Cell SpawnCell(int x, int y, Model.Cell cellData)
        {
            var cell = cellPool.Spawn();
            cell.transform.SetParent(transform);
            cell.SetColor(cellData.Color);
            cell.SetLocalPosition(gridComponent.CellToLocal(new Vector3Int(x, y, 0)));
            cell.SetLocalScale(gridComponent.cellSize);
            cell.gameObject.SetActive(true);
            return cell;
        }

        //private void DespawnCell(int x, int y)
        //{
        //    cellPool.Despawn(cell);
        //}
    }
}