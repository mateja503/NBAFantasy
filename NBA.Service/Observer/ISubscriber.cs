
namespace NBA.Service.Observer
{
    public interface ISubscriber
    {
        public void HandleMessage(object message);
    }
}
