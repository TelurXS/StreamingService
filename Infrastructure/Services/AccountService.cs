﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models.Results;
using Domain.Models.Results.Unions;

namespace Infrastructure.Services;

public sealed class AccountService : IAccountService
{
    public AccountService(IAccountRepository repository)
    {
        Repository = repository;
    }
    
    private IAccountRepository Repository { get; }

    public GetResult<Account> FindById(Guid id)
    {
        var result = Repository.FindById(id);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetAllResult<Account> FindAll()
    {
        return Repository.FindAll();
    }

    public CreateResult<Account> Create(Account value)
    {
        return Repository.Insert(value);
    }

    public UpdateResult<Account> Update(Guid id, Account value)
    {
        if (Repository.FindById(id) is null)
            return new NotFound();
        
        if (Repository.Update(id, value) is false)
            return new Failed();

        var entity = Repository.FindById(id);

        if (entity is null)
            return new Failed();

        return entity;
    }

    public DeleteResult Delete(Account value)
    {
        var result = Repository.Delete(value);

        if (result is false)
            return new Failed();

        return new Success();
    }

    public DeleteResult DeleteById(Guid id)
    {
        var account = Repository.FindById(id);

        if (account is null)
            return new NotFound();

        return Delete(account);
    }

    public GetResult<Account> FindByLogin(string login)
    {
        var result = Repository.FindByLogin(login);

        if (result is null)
            return new NotFound();

        return result;
    }

    public GetResult<Account> FindByEmail(string email)
    {
        var result = Repository.FindByEmail(email);

        if (result is null)
            return new NotFound();

        return result;
    }
}