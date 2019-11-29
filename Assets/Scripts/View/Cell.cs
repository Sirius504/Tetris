using Tetris.Model.Enumerators;
using Tetris.Model.Settings;
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

        public void SetColor(CellColorsEnum color)
        {
            spriteRenderer.material = colorMaterials.GetMaterial(color);
        }

        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        public void SetLocalScale(Vector3 newScale)
        {
            transform.localScale = newScale;
        }


        public class Pool : MemoryPool<Cell>
        {

        }
    }
}