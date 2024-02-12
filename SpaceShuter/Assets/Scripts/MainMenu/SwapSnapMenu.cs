using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LiveToday
{
    public class SwapSnapMenu : ITickable
    {
        private readonly CalculateSwapMenu _calculate;
        private readonly SwapMenuView _view;
        private readonly Scrollbar _scrollBar;
        private bool _isDragging;
        private bool _isSnapping;
        private readonly List<float> _itemPositionsNormalized;
        private float _targetScrollBarValueNormalized;
        private int _selectedTabIndex;
        private const float SnapSpeed = 10f;

        private SwapSnapMenu(CalculateSwapMenu calculate, Scrollbar scrollbar, SwapMenuView view)
        {
            _calculate = calculate;
            _scrollBar = scrollbar;
            _view = view;
            _itemPositionsNormalized = _calculate._itemPositionsNormalized;
            SelectTab(_selectedTabIndex);
            _view.OnEndDragEvent += Drag;
        }
        public void Drag(bool drag, bool endDrag)
        {
            _targetScrollBarValueNormalized = _scrollBar.value;

            _isDragging = drag;
            _isSnapping = endDrag;
            FindSnapTabAndStartSnapping();
        }
        public void Tick()
        {
            if (_isDragging)
                return;

            if (_isSnapping)
                SnapContent();
        }
        public void SelectTab(int tabIndex)
        {
            if (tabIndex < 0 || tabIndex >= _itemPositionsNormalized.Count)
                return;

            _selectedTabIndex = tabIndex;
            _targetScrollBarValueNormalized = _itemPositionsNormalized[tabIndex];
            _isSnapping = true;
        }
        private void FindSnapTabAndStartSnapping()
        {
            var itemSizeNormalized = _calculate.ItemSizeNormalized;

            for (var i = 0; i < _itemPositionsNormalized.Count; i++)
            {
                var itemPositionNormalized = _itemPositionsNormalized[i];

                if (_targetScrollBarValueNormalized < itemPositionNormalized + itemSizeNormalized / 2f &&
                    _targetScrollBarValueNormalized > itemPositionNormalized - itemSizeNormalized / 2f)
                {
                    SelectTab(i);
                    break;
                }
            }
        }
        private void SnapContent()
        {
            if (_itemPositionsNormalized.Count < 2)
            {
                _isSnapping = false;
                return;
            }
            var targetPosition = _itemPositionsNormalized[_selectedTabIndex];
            _scrollBar.value = Mathf.Lerp(_scrollBar.value, targetPosition, Time.deltaTime * SnapSpeed);

            if (Mathf.Abs(_scrollBar.value - targetPosition) <= 0.0001f)
                _isSnapping = false;
        }
    }
}
