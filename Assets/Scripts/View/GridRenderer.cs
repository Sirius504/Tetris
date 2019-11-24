using System;
using System.Collections;
using Tetris.Model;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class GridRenderer : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private TetrisGrid grid;
        private Cell.Pool cellPool;

        [SerializeField]
        private Transform cellPrefab = default;
        private Vector3 cellSpriteSize;

        [Inject]
        public void Construct(TetrisGrid grid, Cell.Pool cellFactory, SpriteRenderer spriteRenderer)
        {
            this.spriteRenderer = spriteRenderer;
            this.grid = grid;
            cellPool = cellFactory;
        }

        // Use this for initialization
        private void Start()
        {
            cellSpriteSize = cellPrefab.GetComponent<SpriteRenderer>().sprite.bounds.size;
            StartCoroutine(DrawGrid());
        }

        private IEnumerator DrawGrid()
        {
            Vector3 cellSizeNormilized = CalculateNormilizedCellSize();
            Vector3 cellScale = CalculateCellScale(cellSizeNormilized);
            Vector2 firstCellPosition = GetFirstCellPosition(cellSizeNormilized);

            for (int j = 0; j < grid.Size.y; j++)
                for (int i = 0; i < grid.Size.x; i++)
                {
                    Vector3 newPosition = firstCellPosition + new Vector2(i * cellSizeNormilized.x, -j * cellSizeNormilized.y);
                    var cell = cellPool.Spawn();
                    cell.transform.SetParent(transform);
                    cell.SetLocalPosition(newPosition);
                    cell.SetLocalScale(cellScale);
                    cell.gameObject.SetActive(true);
                    yield return new WaitForSeconds(.01f);
                }            
        }

        private Vector3 CalculateCellScale(Vector3 desiredSize)
        {
            float xScale = desiredSize.x / cellSpriteSize.x;
            float yScale = desiredSize.y / cellSpriteSize.y;
            float zScale = desiredSize.z / cellSpriteSize.z;
            return new Vector3(xScale, yScale, zScale);
        }

        private Vector3 CalculateNormilizedCellSize()
        {
            Vector3 fieldUnitSize = spriteRenderer.bounds.size;
            float xSize = fieldUnitSize.x / grid.Size.x;
            float ySize = fieldUnitSize.y / grid.Size.y;
            float sizeValue = Mathf.Min(xSize, ySize);
            Vector3 newSize = new Vector3(sizeValue, sizeValue, 1f);
            return new Vector3(newSize.x / transform.lossyScale.x, newSize.y / transform.lossyScale.y, 1f);
        }

        private Vector2 GetFirstCellPosition(Vector2 cellSize)
        {
            Vector3 extents = spriteRenderer.bounds.extents;
            var topLeftCorner = new Vector2(-extents.x / transform.lossyScale.x, extents.y / transform.lossyScale.y);
            var offset = new Vector2(cellSize.x / 2, -cellSize.y / 2);
            var firstCellPosition = topLeftCorner + offset;
            return firstCellPosition;
        }
    }
}