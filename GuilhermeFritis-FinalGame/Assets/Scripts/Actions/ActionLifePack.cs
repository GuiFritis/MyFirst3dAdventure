using Items;
using UnityEngine;
using Sounds;

namespace Actions{
    public class ActionLifePack : ActionBase
    {
        public SOInt soInt;
        public sfxType sfxType = sfxType.LIFE_RESTORED;

        void Start()
        {
            Init();
            soInt = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt;
        }

        public override void CallAction()
        {
            base.CallAction();
            UseLifePack();
        }

        private void UseLifePack()
        {
            if(soInt.Value > 0 && Player.Instance.health.GetCurHealth() != Player.Instance.health.baseHealth)
            {
                ItemManager.Instance.RemoveByType(ItemType.LIFE_PACK);
                Player.Instance.health.ResetLife();
                SFX_Pool.Instance.Play(sfxType);
            }
        }
    }
}
