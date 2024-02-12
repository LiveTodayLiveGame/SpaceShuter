using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LiveToday
{
    public class MapInstaller : MonoInstaller
    {
        [SerializeField] private SwapMenuView _view;
        [SerializeField] private RectTransform _container;
        [SerializeField] private Scrollbar _scrollBar;
        public override void InstallBindings()
        {
            InitStorageService();
            InitSwapSnapMenu();
        }

        private void InitStorageService()
        {
            Container.Bind<LevelProgress>().AsSingle();
            Container.BindInterfacesAndSelfTo<BinaryStorageService>().AsSingle();
            Container.Bind<SavedLevelData>().AsSingle().NonLazy();
        }

        private void InitSwapSnapMenu()
        {
            Container.Bind<RectTransform>().FromInstance(_container).AsSingle();
            Container.Bind<Scrollbar>().FromInstance(_scrollBar).AsSingle();
            Container.Bind<CalculateSwapMenu>().AsSingle();
            Container.BindInterfacesAndSelfTo<SwapSnapMenu>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SwapMenuView>().FromInstance(_view).AsSingle();
        }
    }
}