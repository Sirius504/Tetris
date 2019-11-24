using UnityEngine;
using Zenject;

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
