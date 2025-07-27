using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Native
{
    public class UINativeLevelSelector : MonoBehaviour
    {
        [SerializeField] private Transform _levelsContainer;
        [SerializeField] private ToggleGroup _toggleGroup;
        [SerializeField] private UINativeLevel _levelPrefab;
        [SerializeField] private int _levelCount = 10;

        private void Start()
        {
            for (int i = 0; i < _levelsContainer.childCount; i++)
            {
                Destroy(_levelsContainer.GetChild(i).gameObject);
            }
            
            for (int i = 0; i < _levelCount; i++)
            {
                CreateLevel(i + 1);
            }
        }

        private void CreateLevel(int i)
        {
            var levelComponent = Instantiate(_levelPrefab, _levelsContainer);
            levelComponent.SetLevel(i);
            levelComponent.SetToToggleGroup(_toggleGroup);
        }
    }
}