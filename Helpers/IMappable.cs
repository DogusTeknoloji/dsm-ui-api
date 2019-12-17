namespace DSM.UI.Api.Helpers
{
    public interface IMappable<T> where T : class
    {
        IMappable<T> Map(T item);
    }
}
