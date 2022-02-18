using AutoMapper;
using JiraManagement.Bl.Dto;
using JiraManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JiraManagement.Services.Services
{
    public interface IBaseService<T, TDto, TContext> 
        where T : IBaseEntity 
        where TDto : BaseDto 
        where TContext : DbContext
    {
        IQueryable<T> Get();

        Task<TDto> GetById(string id);

        Task<TDto> Create(TDto dto);

        Task<TDto> Update(string id, TDto dto);

        Task<bool> Delete(string id);
    }

    public class BaseService<T, TDto, TContext> : IBaseService<T, TDto, TContext>
        where T : BaseEntity
        where TDto : BaseDto
        where TContext : DbContext
    {
        protected readonly TContext _context;

        protected readonly DbSet<T> _dbSet;

        protected readonly IMapper _mapper;

        public BaseService(TContext context, IMapper mapper)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _mapper = mapper;
        }

        public virtual async Task<TDto> Create(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);
            
            await _context.AddAsync<T>(entity);

            await _context.SaveChangesAsync();
                        
            return _mapper.Map<TDto>(entity);
        }

        public async Task<bool> Delete(string id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is null) return false;

            entity.IsDeleted = true;

            _dbSet.Update(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public virtual IQueryable<T> Get()
        {
            return _dbSet.Where(x => x.IsDeleted == false).AsQueryable();
        }

        public virtual async Task<TDto> GetById(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity is null || entity.IsDeleted) return null;

            return _mapper.Map<TDto>(entity);
        }

        public virtual async Task<TDto> Update(string id, TDto dto)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is null) return null;

            var update = _mapper.Map(dto, entity);

            _dbSet.Update(update);

            await _context.SaveChangesAsync();

            return _mapper.Map<TDto>(update);
        }
    }
}
