using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;

namespace CDB.Input
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Animator _animatorHand;
        public override void InstallBindings()
        {
            Container.Bind<PlayerInput>().FromInstance(_playerInput).AsSingle().NonLazy();
            Container.Bind<Animator>().FromInstance(_animatorHand).AsSingle().NonLazy();
        }
    }
}