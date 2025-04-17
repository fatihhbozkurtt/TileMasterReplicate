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
            Container.Bind<CanvasManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<CameraManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<GridManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}