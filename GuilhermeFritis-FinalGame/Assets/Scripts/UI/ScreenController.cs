using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

namespace Screens
{
    public enum GameplayScreenType
    {
        PLAYER_HUD,
        MENU,
        GAME_OVER
    }

    public class ScreenController : Singleton<ScreenController>
    {    
        public List<ScreenSetup> screens = new List<ScreenSetup>();

        public void ShowScreen(GameplayScreenType screenType, bool active = true)
        {
            var setup = screens.Find(i => i.screenType == screenType);
            if(setup != null)
            {
                setup.screen.SetActive(active);
            }
        }

        public void HideAllScreens()
        {
            foreach (var item in screens)
            {
                item.screen.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class ScreenSetup
    {
        public GameObject screen;
        public GameplayScreenType screenType;
    }
}