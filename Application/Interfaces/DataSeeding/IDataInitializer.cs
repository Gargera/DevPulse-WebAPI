namespace Application.Interfaces.DataSeeding
{
    public interface IDataInitializer
    {
        public Task InitializeIdentityDataAsync();
    }
}
