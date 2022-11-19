using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uiIcon;
        public TextMeshProUGUI uiText;

        private ItemSetup _curSetup;

        public void Load(ItemSetup setup)
        {
            _curSetup = setup;
            _curSetup.soInt.OnValueChanged += UpdateText;
            UpdateUI();
            UpdateText(setup.soInt.Value);
        }

        public void UpdateUI()
        {
            uiIcon.sprite = _curSetup.icon;
        }

        void UpdateText(int value)
        {
            uiText.text = value.ToString();
        }
    }
}
