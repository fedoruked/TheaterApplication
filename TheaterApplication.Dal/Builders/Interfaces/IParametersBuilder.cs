namespace TheaterApplication.Dal.Builders.Interfaces
{
    public interface IParametersBuilder
    {
        IParametersBuilder Add(string name, object value);
        object[] Build();
    }
}
