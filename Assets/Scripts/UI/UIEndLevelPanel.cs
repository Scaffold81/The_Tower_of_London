using Game.UI;
using TMPro;
using TowerOfLondon.Audio;
using UnityEngine;

namespace TowerOfLondon.UI
{
    public class UIEndLevelPanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _headerText; 
        [SerializeField]
        private TMP_Text _infoText;
        
        private UIPanelStateController _panelStateController;

        public SoundManager Sound { get; private set; }

        private void Awake()
        {
            Sound = GetComponent<SoundManager>();
            gameObject.AddComponent<CanvasGroup>();
            _panelStateController= gameObject.AddComponent<UIPanelStateController>();
            _panelStateController.Hide();
        }

        public void PanelOn(string header,string info)
        {
             Sound.PlaySound();
            _headerText.text = header;
            _infoText.text = info;
            _panelStateController.Show();
        }

        public void PanelOff()
        {
            _panelStateController.Hide();
        }
    }
}
