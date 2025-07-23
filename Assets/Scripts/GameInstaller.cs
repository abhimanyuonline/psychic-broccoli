using broccoli.Controller;
using broccoli.Manager;
using UnityEngine;
using Zenject;

public class GameInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManger>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<CardController>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}