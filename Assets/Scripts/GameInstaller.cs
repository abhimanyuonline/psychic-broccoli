using broccoli.Controller;
using broccoli.Manager;
using broccoli.Manager.Audio;
using broccoli.Presenter;
using Zenject;

public class GameInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<CardController>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SoundManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<GamePresenter>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}