using AutoMapper;
using BlogApp.Core.Dtos;
using BlogApp.Core.Repositories;
using BlogApp.Core.Services;
using BlogApp.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.Service.Services
{
    public class Service<T,TDto> : IService<T,TDto> where T : class where TDto : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<CustomResponseDto<TDto>> CreateAsync(TDto entityDto)
        {
            var entity = _mapper.Map<T>(entityDto);
            await _repository.CreateAsync(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TDto>.Success(200,entityDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> DeleteAsync(TDto entityDto)
        {
            var entity = _mapper.Map<T>(entityDto);
            _repository.Delete(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(204);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            var entitiesDto = _mapper.Map<List<TDto>>(entities);
            return CustomResponseDto<IEnumerable<TDto>>.Success(200, entitiesDto);
        }

        public async Task<CustomResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var entityDto = _mapper.Map<TDto>(entity);
            return CustomResponseDto<TDto>.Success(200,entityDto);
        }

        public async Task<CustomResponseDto<TDto>> UpdateAsync(TDto entityDto, int id)
        {
            var entity = _mapper.Map<T>(entityDto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<TDto>.Success(200,entityDto);
        }

        public async Task<CustomResponseDto<IEnumerable<TDto>>> Where(Expression<Func<T, bool>> expression)
        {
            var list = await _repository.Where(expression).ToListAsync();
            var listDto = _mapper.Map<IEnumerable<TDto>>(list);
            return CustomResponseDto<IEnumerable<TDto>>.Success(200,listDto);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }
    }
}
