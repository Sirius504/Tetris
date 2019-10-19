using System.Collections;
using Tetris.Model;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class GridRenderer : MonoBehaviour
    {
        [SerializeField]
        private TetrisGrid grid;

        [SerializeField]
        private Transform cellPrefab = default;

        private Vector2 size;


        [Inject]
        public void Construct(TetrisGrid grid)
        {
            this.grid = grid;
        }

        // Use this for initialization
        private void Start()
        {
            size = transform.lossyScale;
            StartCoroutine(DrawGrid());
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawCube();
        }

        private IEnumerator DrawGrid()
        {
            float cellSize = Mathf.Min(size.x / grid.Size.x, size.y / grid.Size.y);
            cellPrefab.transform.localScale = new Vector3(cellSize / size.x, cellSize / size.y, 1f);
            float offset = cellSize / 2;               

            for (int i = 0; i < grid.Size.x; i++)
                for (int j = 0; j < grid.Size.y; j++)
                {
                    float positionX = ((-1 * (size.x / 2)) + offset + i * cellSize) / size.x;
                    float positionY = (size.y / 2 + offset + j * cellSize) / size.y;
                    var cell = Instantiate(cellPrefab, transform);
                    cell.localPosition = new Vector3(positionX, -positionY, 0f);
                    yield return new WaitForSeconds(.2f);
                }
        }

        // Update is called once per frame
        private void Update()
        {

        }
    }
}