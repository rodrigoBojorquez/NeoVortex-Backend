using NeoVortex.Application.Interfaces.Repositories;
using NeoVortex.Domain.Entities;
using NeoVortex.Infrastructure.Data;

namespace NeoVortex.Infrastructure.Repositories;

public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
{
    public DocumentRepository(NeoVortexDbContext context) : base(context)
    {
    }
}