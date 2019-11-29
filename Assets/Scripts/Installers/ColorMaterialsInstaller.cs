using Tetris.Model.Settings;
using UnityEngine;
using Zenject;
namespace Tetris.Installers
{
    [CreateAssetMenu(fileName = "ColorsInstaller", menuName = "Installers/ColorsInstaller")]
    public class ColorMaterialsInstaller : ScriptableObjectInstaller<ColorMaterialsInstaller>
    {
        [SerializeField]
        private ColorMaterials colorSettings;

        public override void InstallBindings()
        {
            Container.BindInstance(colorSettings).AsSingle();
        }
    } 
}
