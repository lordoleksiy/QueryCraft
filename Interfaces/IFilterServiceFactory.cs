namespace QueryCraft.Intrefaces
{
    public interface IFilterServiceFactory
    {
        IFilterService<T> CreateFilterService<T>() where T : class;
    }
}
