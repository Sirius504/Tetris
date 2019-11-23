using System.Collections;
using Tetris.Model;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class GridRenderer : MonoBehaviour
    {
        private TetrisGrid grid;

        [SerializeField]
        private Transform cellPrefab = default;
        

        [Inject]
        public void Construct(TetrisGrid grid)
        {
            this.grid = grid;
        }

        // Use this for initialization
        private void Start()
        {
            StartCoroutine(DrawGrid());
        }

        private IEnumerator DrawGrid()
        {
            Vector2 cellSizeNormilized = CalculateCellSizeNormilized();
            Vector3 newCellScale = new Vector3(cellSizeNormilized.x, cellSizeNormilized.y, 1f);

            Vector2 firstCellPosition = GetFirstCellPosition(cellSizeNormilized);

            for (int j = 0; j < grid.Size.y; j++)
                for (int i = 0; i < grid.Size.x; i++)
                {
                    Vector3 newPosition = firstCellPosition + new Vector2(i * cellSizeNormilized.x, -j * cellSizeNormilized.y);
                    var cell = Instantiate(cellPrefab, transform);
                    cell.localPosition = newPosition;
                    cell.localScale = newCellScale;
                    cell.gameObject.SetActive(true);
                    yield return new WaitForSeconds(.01f);
                }
        }

        private Vector2 CalculateCellSizeNormilized()
        {
            return new Vector3(1f / grid.Size.x, 1f / grid.Size.y);
        }

        private static Vector2 GetFirstCellPosition(Vector2 cellSizeNormilized)
        {
            var topLeftCorner = new Vector2(-0.5f, 0.5f);
            var offset = new Vector2(cellSizeNormilized.x / 2, -cellSizeNormilized.y / 2);
            var firstCellPosition = topLeftCorner + offset;
            return firstCellPosition;
        }
    }
}