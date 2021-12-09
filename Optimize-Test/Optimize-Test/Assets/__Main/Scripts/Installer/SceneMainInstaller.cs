using UnityEngine;
using Zenject;

public class SceneMainInstaller : MonoInstaller
{
    [SerializeField] private GeneralSettings _generalSettings;
    [SerializeField] private InventoryItem _inventoryItem;
    
    public override void InstallBindings()
    {
        Container.Bind<IGeneralSettings>().To<GeneralSettings>().FromScriptableObject(_generalSettings).AsSingle();
        Container.BindFactory<InventoryItem, InventoryItem.Factory>().FromInstance(_inventoryItem);
        Container.Bind<IInventoryPool>().To<InventoryPool>().FromComponentsInHierarchy().AsSingle();
    }
}