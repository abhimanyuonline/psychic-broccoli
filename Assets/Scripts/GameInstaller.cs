using System.ComponentModel;
using broccoli.Controller;
using broccoli.Manager;
using broccoli.Manager.Audio;
using broccoli.Presenster;
using UnityEngine;
using Zenject;

public class GameInstaller: MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManger>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<CardController>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<LevelManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SoundManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<GamePresenter>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}