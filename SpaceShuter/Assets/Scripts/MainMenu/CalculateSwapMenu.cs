using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LiveToday
{
    public class CalculateSwapMenu
    {
        private readonly RectTransform _container;

        public readonly List<float> _itemPositionsNormalized = new();
        public float ItemSizeNormalized { get; private set; }

        private CalculateSwapMenu(RectTransform container)
        {
            _container = container;
        }

        public void CalculateContainer(int itemsCount)
        {
            var layoutGroup = _container.GetComponent<GridLayoutGroup>();

            var itemHeight = layoutGroup.cellSize.y;
            var paddingTop = layoutGroup.padding.top;
            var paddingBottom = layoutGroup.padding.bottom;
            var spacing = layoutGroup.spacing.y;
            var columnCount = layoutGroup.constraintCount;
            var itemsCountInContainer = itemsCount / columnCount;

            
            var contentHeight = itemHeight * (itemsCountInContainer) * 1f + paddingTop + paddingBottom +
                                (itemsCount == 0 ? 0 : (itemsCountInContainer - 1f) * spacing);
            
            ItemSizeNormalized = (itemHeight + spacing + 1.25f) / contentHeight ;
            
           SetContentSizeDelta(ref contentHeight); 
           SetItemPositionsNormalized(ref itemsCountInContainer);
        }

        private void SetContentSizeDelta(ref float contentHeight)
        {
            _container.sizeDelta = new Vector2(_container.sizeDelta.x, contentHeight);
        }

        private void SetItemPositionsNormalized(ref int itemsCount)
        {
            for (var i = 0; i < itemsCount; i++)
            {
                var itemPositionNormalized = ItemSizeNormalized * i;
                _itemPositionsNormalized.Add(itemPositionNormalized);
            }
        }
    }
}
