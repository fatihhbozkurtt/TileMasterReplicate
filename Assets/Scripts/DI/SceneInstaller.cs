using EssentialManagers.Scripts;
using Zenject;

namespace DI
{
    public class SceneInstaller : MonoInstaller
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.Bind<InputManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}