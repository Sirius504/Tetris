using Tetris.Model;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class GridRenderer : MonoBehaviour
    {
        private GameGrid gridModel;
        private Cell.Pool cellPool;
        private Cell[,] cells;
        private Grid gridComponent;
        // As Cells in Unity Grid component have (0, 0) as their down left corner, we
        // need this offset, so objects spawn at centers of grid cells instead of
        // corners.
        private Vector3 gridCornerOffset;

        [SerializeField]
        private Transform cellPrefab = default;
        private Vector3 cellSpriteSize;

        [Inject]
        public void Construct(GameGrid gridModel, Cell.Pool cellPool, Grid gridComponent)
        {
            this.gridModel = gridModel;
            this.cellPool = cellPool;
            this.gridComponent = gridComponent;
            gridCornerOffset = gridComponent.cellSize / 2f;
            cells = new Cell[gridModel.Size.x, gridModel.Size.y];
        }

        private void LateUpdate()
        {
            CleanGrid();
            DrawGrid();
        }

        private void CleanGrid()
        {
            for (int j = 0; j < gridModel.Size.y; j++)
                for (int i = 0; i < gridModel.Size.x; i++)
                {
                    if (cells[i, j] != null)
                    {
                        cellPool.Despawn(cells[i, j]);
                        cells[i, j] = null;
                    }
                }
        }

        private void DrawGrid()
        {
            for (int j = 0; j < gridModel.Size.y; j++)
                for (int i = 0; i < gridModel.Size.x; i++)
                {
                    var cellData = gridModel.Cells[i, j];
                    if (cellData != null)
                        cells[i, j] = SpawnCell(i, j, cellData);
                }
        }

        private Cell SpawnCell(int x, int y, Model.Cell cellData)
        {
            var cell = cellPool.Spawn();
            cell.SetColor(cellData.Color);
            var newPosition = gridComponent.CellToLocal(new Vector3Int(x, y, 0)) + gridCornerOffset;
            cell.SetLocalPosition(newPosition);
            cell.SetLocalScale(gridComponent.cellSize);
            cell.gameObject.SetActive(true);
            return cell;
        }
    }
}