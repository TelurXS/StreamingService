#nullable enable
using System;
using VideoDemos.Views.Auth.Registration;

namespace Metflix.Core.Models;

public class AccountModel
{
    private string _email;
    private string _password;
    
    private string _nickname;
    private string _name;
    private string _surname;
    private DateTime _birthdate;
    
    private GenreArray _genres = new GenreArray();

    private PlanDBModel _plan;
    
    private string _paymentmethod;
    
    private string? _cardNumber;
    private string? _cardDate;
    private string? _cvv;

    public string Email
    {
        get => _email;
        set => _email = value;
    }

    public string Password
    {
        get => _password;
        set => _password = value;
    }

    public string Nickname
    {
        get => _nickname;
        set => _nickname = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string Surname
    {
        get => _surname;
        set => _surname = value;
    }

    public DateTime Birthdate
    {
        get => _birthdate;
        set => _birthdate = value;
    }

    public GenreArray Genres
    {
        get => _genres;
        set => _genres = value;
    }

    public PlanDBModel Plan
    {
        get => _plan;
        set => _plan = value;
    }

    public string Paymentmethod
    {
        get => _paymentmethod;
        set => _paymentmethod = value;
    }

    public string CardNumber
    {
        get => _cardNumber;
        set => _cardNumber = value;
    }

    public string CardDate
    {
        get => _cardDate;
        set => _cardDate = value;
    }

    public string Cvv
    {
        get => _cvv;
        set => _cvv = value;
    }

    public string Result { get; set; }
}