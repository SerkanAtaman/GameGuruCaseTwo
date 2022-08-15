namespace Seroley.DelayHandling
{
    public static class DelayHandler
    {
        public static System.Action<Delay> OnDelayCreated;

        private static readonly DelayerObject _delayer;

        static DelayHandler()
        {
            UnityEngine.GameObject delayer = new UnityEngine.GameObject();
            delayer.name = "DelayerObject";
            delayer.AddComponent(typeof(DelayerObject));
            UnityEngine.Object.DontDestroyOnLoad(delayer);

            _delayer = delayer.GetComponent<DelayerObject>();

            OnDelayCreated = DelayCreated;
        }

        private static void DelayCreated(Delay delay)
        {
            _delayer.StartDelay(delay);
        }
    }
}