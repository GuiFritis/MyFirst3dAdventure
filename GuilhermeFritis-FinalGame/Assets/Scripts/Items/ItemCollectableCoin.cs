using DG.Tweening;

namespace Items
{
    public class ItemCollectableCoin : ItemCollectableBase
    {
        public SOAnimation collectingMoveY;

        public SOAnimation coinFloat;

        private void Start() 
        {
            hideDelay = collectingMoveY.duration + collectingMoveY.delay;
            if(coinFloat != null)
            {
                coinFloat.DGAnimate(transform.DOMoveY(coinFloat.value, coinFloat.duration));
            }
        }

        protected override void Collect()
        {
            base.Collect();
            transform.DOKill();
            if(audioSorce != null){
                audioSorce.Play();
            }
            collectingMoveY.DGAnimate(transform.DOMoveY(collectingMoveY.value , collectingMoveY.duration));
        }
        protected override void OnCollect()
        {
            base.OnCollect();
        }
    }
}
