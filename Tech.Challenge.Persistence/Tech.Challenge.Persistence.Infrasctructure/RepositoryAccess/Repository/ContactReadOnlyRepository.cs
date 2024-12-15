using Microsoft.EntityFrameworkCore;
using Tech.Challenge.Persistence.Domain.Entities;
using Tech.Challenge.Persistence.Domain.Repositories.Contact;

namespace Tech.Challenge.Persistence.Infrasctructure.RepositoryAccess.Repository;
public class ContactReadOnlyRepository(TechChallengeContext context) : IContactReadOnlyRepository
{
    private readonly TechChallengeContext _context = context;

    public async Task<Contact> RecoverByContactIdAsync(Guid id) =>
        await _context.Contacts.Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
}
