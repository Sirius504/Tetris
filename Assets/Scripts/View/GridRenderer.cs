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

            gridModel.OnCellCreated += SpawnCell;
            gridModel.OnCellDeleted += DespawnCell;
        }

        private void SpawnCell(Vector2Int position, Model.Cell cellData)
        {
            var cell = cellPool.Spawn();
            cells[position.x, position.y] = cell;
            cell.SetColor(cellData.Color);
            var newPosition = gridComponent.CellToLocal(new Vector3Int(position.x, position.y, 0)) + gridCornerOffset;
            cell.SetLocalPosition(newPosition);
            cell.SetLocalScale(gridComponent.cellSize);
            cell.gameObject.SetActive(true);
        }

        private void DespawnCell(Vector2Int position)
        {
            if (cells[position.x, position.y] != null)
            {
                cellPool.Despawn(cells[position.x, position.y]);
                cells[position.x, position.y] = null;
            }
        }
    }
}