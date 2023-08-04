namespace Lifetimes
{
    public interface IOperation
    {
        public string LifetimeId { get; }
    }

    public interface ITransientOperation : IOperation
    {

    }

    public interface IScopedOperation : IOperation
    {
    }

    public interface ISingletonOperation : IOperation
    {
    }


    public class MyServiceLifetime : ITransientOperation, IScopedOperation, ISingletonOperation
    {
        public MyServiceLifetime()
        {
            LifetimeId = Guid.NewGuid().ToString()[^4..];
        }

        public string LifetimeId { get; }
    }


}
