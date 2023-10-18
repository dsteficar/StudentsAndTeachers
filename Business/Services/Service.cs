using AutoMapper;
using Business.CustomExceptions;
using Business.Interfaces;
using Data.Interfaces;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class Service<TEntity, TModel> : IService<TEntity, TModel> where TEntity : BaseEntity where TModel : class
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public Service(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public virtual async Task<List<TModel>> GetAll()
        {
            var entities = await _repository.GetAll();
            return entities.Select(x => _mapper.Map<TModel>(x)).ToList();
        }
        public virtual async Task<TModel> GetById(int id)
        {
            var entity = await _repository.GetById(id);
            return _mapper.Map<TModel>(entity);
        }
        public virtual async Task<TModel> Insert(TModel entityDTO)
        {
            var entityDb = _mapper.Map<TEntity>(entityDTO);

            await _repository.Insert(entityDb);
            return entityDTO;
        }

        public virtual async Task Update(int id, TModel entityDTO)
        {

            var entityDb = await _repository.GetById(id);

            if (entityDb == null)
            {
                throw new UserNotFoundException();
            }

            _mapper.Map(entityDTO, entityDb);

            try
            {
                await _repository.Update(entityDb);
            }
            catch (DbUpdateException)
            {
                throw new DbUpdateException();
            }

        }
        public virtual async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
