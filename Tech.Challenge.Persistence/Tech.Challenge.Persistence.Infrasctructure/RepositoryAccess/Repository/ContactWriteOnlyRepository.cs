﻿using Tech.Challenge.Persistence.Domain.Entities;
using Tech.Challenge.Persistence.Domain.Repositories.Contact;

namespace Tech.Challenge.Persistence.Infrasctructure.RepositoryAccess.Repository;
public class ContactWriteOnlyRepository(TechChallengeContext context) : IContactWriteOnlyRepository
{
    private readonly TechChallengeContext _context = context;

    public async Task Add(Contact contact) =>
        await _context.Contacts.AddAsync(contact);
}