
namespace CommandAPI.MiddleWares
{
    public interface ILogStorage
    {
        public void Store(LogModel log);
    }
}