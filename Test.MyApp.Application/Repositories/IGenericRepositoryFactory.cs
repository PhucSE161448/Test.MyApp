namespace Test.MyApp.Application.Repositories
{
    public interface IGenericRepositoryFactory
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}
