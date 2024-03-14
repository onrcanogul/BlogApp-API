using BlogApp.Core.Dtos;
using System.Linq.Expressions;

namespace BlogApp.Core.Services
{
    public interface IService<T,TDto> where T : class where TDto: class
    {
        public Task<CustomResponseDto<TDto>> CreateAsync(TDto entity);
        public Task<CustomResponseDto<TDto>> UpdateAsync(TDto entity, int id);
        public Task<CustomResponseDto<NoContentDto>> DeleteAsync(TDto entity);
        public Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync();
        public Task<CustomResponseDto<TDto>> GetByIdAsync(int id);
        public Task<CustomResponseDto<IEnumerable<TDto>>> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    }
}
