using Tetris.Model.Enumerators;
using UnityEngine;
using Zenject;

namespace Tetris.View
{
    public class Cell : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private ColorMaterials colorMaterials;

        [Inject]
        public void Construct(SpriteRenderer spriteRenderer, ColorMaterials colorMaterials)
        {
            this.spriteRenderer = spriteRenderer;
            this.colorMaterials = colorMaterials;
        }

        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        public void SetLocalScale(Vector3 newScale)
        {
            transform.localScale = newScale;
        }

        public void SetColor(CellColorsEnum newColor)
        {
            //spriteRenderer.material = 
        }


        public class Pool : MemoryPool<Cell>
        {

        }
    }
}