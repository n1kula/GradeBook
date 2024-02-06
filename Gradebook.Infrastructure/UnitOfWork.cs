using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Gradebook.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly GradebookDbContext _dbContext;
        public UnitOfWork(GradebookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableEntities();
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities()
        {
            var entires = _dbContext
                .ChangeTracker
                .Entries<Entity>();
            foreach (var entry in entires)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = entry.Entity.UpdatedAt = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                }
            }
        }
    }
}
