using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Native
{
    public class UINativeLevel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private Toggle _toggle;
        
        public void SetLevel(int level)
        {
            _levelText.text = level.ToString();
        }

        public void SetToToggleGroup(ToggleGroup group)
        {
            _toggle.group = group;
        }
    }
}