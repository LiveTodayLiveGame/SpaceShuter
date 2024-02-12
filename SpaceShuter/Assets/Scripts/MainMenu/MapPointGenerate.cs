using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LiveToday
{
    public class MapPointGenerate : MonoBehaviour
    {
        [SerializeField] private int _poolCount;
        [SerializeField] private MapPoint _prefab;

        private PoolObject<MapPoint> _pool;
        private SavedLevelData _levelData;
        private LevelProgress _levelProgress;
        private CalculateSwapMenu _calculate;
        private MapPoint _prefabMapPoint;

        [Inject]
        private void Construct(SavedLevelData levelData, CalculateSwapMenu calculate)
        {
            _levelData = levelData;
            _calculate = calculate;
        }

        private void Awake()
        {
            _levelProgress = _levelData.GetLevelProgress();
            _calculate.CalculateContainer(_levelProgress.Levels.Length);
            Generate();
        }

        private void Generate()
        {
            var openedIndex = GetLostUnlockedLevelIndex();

            _pool = new PoolObject<MapPoint>(_prefab, this._poolCount, this.transform);

            for (int i = openedIndex; i < openedIndex + _poolCount; i++)
            {
                _pool.GetFreeElement();
                transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = (i + 1).ToString();
                transform.GetChild(i).GetComponent<Button>().interactable = _levelProgress.Levels[i].IsOpened;
            }
        }

        private int GetLostUnlockedLevelIndex()
        {
            var index = 0;

            for (int i = 0; i < _levelProgress.Levels.Length; i++)
            {
                if (_levelProgress.Levels[i].IsOpened == false)
                {
                    index = i - 1;
                    break;
                }
            }

            return index;
        }

    }
}