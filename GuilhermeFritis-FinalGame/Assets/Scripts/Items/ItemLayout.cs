using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Actions;

namespace Items
{
    public class ItemLayout : MonoBehaviour
    {
        public Image uiIcon;
        public TextMeshProUGUI uiText;
        public TextMeshProUGUI uiActionKey;

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
            if(_curSetup.action != null)
            {
                uiActionKey.text = _curSetup.action.inputAction.action.bindings[0].ToDisplayString();
            }
        }

        void UpdateText(int value)
        {
            uiText.text = value.ToString();
        }
    }
}
