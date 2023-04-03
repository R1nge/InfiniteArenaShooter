namespace Player
{
    public class PlayerHealth : Health
    {
        protected override void Awake()
        {
            base.Awake();
            OnDiedEvent += OnOnDiedEvent;
        }

        private void OnOnDiedEvent()
        {
            //TODO: show game over UI
        }

        private void OnDestroy() => OnDiedEvent -= OnOnDiedEvent;
    }
}