
namespace EventsHandler
{
    [System.Serializable]
    public class CallbackEvent
    {
        public object target = null;
        public object[] Params = null;

        public CallbackEvent(object inTarget, params object[] inParams)
        {
            target = inTarget;
            Params = inParams;
        }
    }
}
