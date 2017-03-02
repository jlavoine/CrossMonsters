using UnityEngine;
using Zenject;
using MyLibrary;

public class ProjectInstaller : MonoInstaller{
    public override void InstallBindings() {
        Container.Bind<IMessageService>().To<MyMessenger>().AsSingle();
    }
}