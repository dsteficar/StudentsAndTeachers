namespace Business.Interfaces
{
    public interface IService<TEntity, TModel> where TEntity : class where TModel : class
    {
        Task<List<TModel>> GetAll();
        Task<TModel> GetById(int id);
        Task Update(int id, TModel entity);
        Task Delete(int id);
        Task<TModel> Insert(TModel entity);
    }
}
