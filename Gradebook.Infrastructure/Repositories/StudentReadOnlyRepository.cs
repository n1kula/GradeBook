﻿using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Gradebook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Infrastructure.Repositories
{
    internal class StudentReadOnlyRepository : IStudentReadOnlyRepository
    {
        private readonly GradebookDbContext _dbContext;
        public StudentReadOnlyRepository(GradebookDbContext _dbContext)
        {
            _dbContext = _dbContext;
        }
        public async Task<IEnumerable<Student>> GetAllAsync(CancellationToken cancellation = default)
        {
            return await _dbContext.Students.AsNoTracking().ToListAsync(cancellation);
        }
    }
}
