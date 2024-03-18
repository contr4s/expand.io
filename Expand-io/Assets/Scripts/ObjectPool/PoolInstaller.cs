using ObjectPool.PoolExpansionStrategies;
using UnityEngine;
using Zenject;

namespace ObjectPool
{
    public class PoolInstaller : MonoInstaller
    {
        [SerializeField] private PoolContainersHolder _containersHolder;
        
        public override void InstallBindings()
        {
            Container.Bind<PoolContainersHolder>().FromInstance(_containersHolder).AsSingle();
            Container.Bind<IPoolExpansionStrategy>().FromInstance(new AggressivePoolExpansion())
                     .WhenInjectedInto<PoolableObjectProvider>();
            Container.BindInterfacesTo<PoolableObjectProvider>().AsSingle();
        }
    }
}