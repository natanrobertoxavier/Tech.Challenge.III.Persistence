using Microsoft.EntityFrameworkCore;
using Tech.Challenge.Persistence.Domain.Entities;
using Tech.Challenge.Persistence.Domain.Repositories.Contact;

namespace Tech.Challenge.Persistence.Infrasctructure.RepositoryAccess.Repository;
public class ContactDeleteOnlyRepository(TechChallengeContext context) : IContactDeleteOnlyRepository
{
    private readonly TechChallengeContext _context = context;

    public void Remove(Contact contact) =>
        _context.Contacts.Remove(contact);
}
