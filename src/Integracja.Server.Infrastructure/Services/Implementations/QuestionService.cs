using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Integracja.Server.Core.Models.Base;
using Integracja.Server.Core.Repositories;
using Integracja.Server.Infrastructure.Exceptions;
using Integracja.Server.Infrastructure.Models;
using Integracja.Server.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Integracja.Server.Infrastructure.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IConfigurationProvider _configuration;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IConfigurationProvider configuration)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<T> Get<T>(int id, int userId, bool skipUserVerification = false)
        {
            var dto = await _questionRepository.Get(id)
                .Where(q => (q.IsPublic || q.OwnerId == userId || skipUserVerification) && !q.IsDeleted &&
                    (q.Category.IsPublic || q.Category.OwnerId == userId || skipUserVerification) && !q.Category.IsDeleted)
                .ProjectTo<T>(_configuration)
                .FirstOrDefaultAsync();

            if (dto == null)
            {
                throw new NotFoundException();
            }

            return dto;
        }

        public async Task<IEnumerable<T>> GetAll<T>(int userId, bool skipUserVerification = false)
        {
            return await _questionRepository.GetAll()
                .Where(q => (q.IsPublic || q.OwnerId == userId || skipUserVerification) && !q.IsDeleted &&
                    (q.Category.IsPublic || q.Category.OwnerId == userId || skipUserVerification) && !q.Category.IsDeleted)
                .ProjectTo<T>(_configuration)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetOwned<T>(int userId)
        {
            return await _questionRepository.GetAll()
                .Where(q => q.OwnerId == userId &&
                    !q.IsDeleted &&
                    !q.Category.IsDeleted)
                .ProjectTo<T>(_configuration)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetOwned<T>(int categoryId, int userId)
        {
            return await _questionRepository.GetAll()
                .Where(q =>
                q.OwnerId == userId
                && !q.IsDeleted
                && q.Category.OwnerId == userId
                && q.CategoryId == categoryId
                && !q.Category.IsDeleted)
                .ProjectTo<T>(_configuration)
                .ToListAsync();
        }

        public async Task<int> Add(CreateQuestionDto createQuestionDto, int userId, bool skipUserVerification = false)
        {
            var question = _mapper.Map<Question>(createQuestionDto);
            question.OwnerId = userId;

            return await _questionRepository.Add(question, skipUserVerification);
        }

        public async Task Delete(int id, int userId, bool skipUserVerification = false)
        {
            await _questionRepository.Delete(new Question
            {
                Id = id,
                OwnerId = userId
            }, skipUserVerification);
        }

        public async Task<int> Update(int id, EditQuestionDto editQuestionDto, int userId, bool skipUserVerification = false)
        {
            var question = _mapper.Map<Question>(editQuestionDto);
            question.Id = id;
            question.OwnerId = userId;

            return await _questionRepository.Update(question, skipUserVerification);
        }
    }
}
