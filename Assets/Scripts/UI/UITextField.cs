using Game.UI;
using TMPro;
using UnityEngine;

namespace TowerOfLondon.UI
{
    public class UITextField : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void UpdateText(string text)
        {
            _text.text = text;
        }
    }
}
