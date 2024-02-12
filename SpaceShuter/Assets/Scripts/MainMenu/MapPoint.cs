using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LiveToday
{
    [RequireComponent(typeof(Button))]
    public class MapPoint : MonoBehaviour
    {
        private string _pointIndex;
        private bool _pointInteractable;
        private void Start()
        {
           _pointIndex = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
           _pointInteractable = GetComponent<Button>().interactable;
        }

        public void SetData(int index, bool interactable)
        {
            _pointIndex = index.ToString();
            _pointInteractable = interactable;
        }
    }
}
