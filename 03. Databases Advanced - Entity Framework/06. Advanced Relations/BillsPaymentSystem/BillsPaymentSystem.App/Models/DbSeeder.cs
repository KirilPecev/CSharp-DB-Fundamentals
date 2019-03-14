﻿namespace BillsPaymentSystem.App.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BillsPaymentSystem.Models;
    using BillsPaymentSystem.Models.Enums;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class DbSeeder
    {
        private readonly BillsPaymentSystemContext _context;

        public DbSeeder(BillsPaymentSystemContext context)
        {
            this._context = context;
        }

        public void Seed()
        {
            this._context.Database.EnsureDeleted();
            this._context.Database.Migrate();

            User[] users = new[]
            {
                new User
                {
                    FirstName = "Pesho",
                    LastName = "Petrov",
                    Email = "pesho@abv.bg",
                    Password = "qweadsaf"
                },

                new User
                {
                    FirstName = "Gosho",
                    LastName = "Ivanov",
                    Email = "gosho@gmail.com",
                    Password = "asdasda"
                },

                new User
                {
                    FirstName = "Ivan",
                    LastName = "Georgiev",
                    Email = "Georgiev@maimunka.com",
                    Password = "asdasczx"
                },

                new User
                {
                    FirstName = "Milena",
                    LastName = "Krasimirova",
                    Email = "milche@mail.bg",
                    Password = "asdaqwe123"
                }
            };

            foreach (var user in users)
            {
                if (IsValid(user))
                {
                    this._context.Users.Add(user);
                }
            }

            CreditCard[] creditCards = new[]
            {
                new CreditCard()
                {
                    ExpirationDate = DateTime.Now.Date.AddMonths(2),
                    Limit = 15000.00m,
                    MoneyOwned = 1500.00m
                },

                new CreditCard()
                {
                    ExpirationDate = DateTime.Now.Date.AddMonths(7),
                    Limit = 20000m,
                    MoneyOwned = 1800m,
                },

                new CreditCard()
                {
                    ExpirationDate = DateTime.Now.Date.AddMonths(5),
                    Limit = 15000m,
                    MoneyOwned = 14000m,

                },

                new CreditCard()
                {
                    ExpirationDate = DateTime.Now.Date,
                    Limit = 16000m,
                    MoneyOwned = 4500m
                }
            };

            foreach (var card in creditCards)
            {
                if (IsValid(card))
                {
                    this._context.CreditCards.Add(card);
                }
            }

            BankAccount[] bankAccounts = new[]
            {
                new BankAccount
                {
                    Balance = 2455m,
                    BankName = "SG Expresbank",
                    SwiftCode = "BVDSGWS",

                },
                new BankAccount
                {
                    Balance = 12000m,
                    BankName = "Investbank",
                    SwiftCode = "BGHFSER",

                },

                new BankAccount
                {
                    Balance = 104000m,
                    BankName = "DSK",
                    SwiftCode = "BGBGBGBG",
                },

                new BankAccount
                {
                    Balance = 8500m,
                    BankName = "OBB bank",
                    SwiftCode = "BGSDGF"

                }
            };

            foreach (var acc in bankAccounts)
            {
                if (IsValid(acc))
                {
                    this._context.BankAccounts.Add(acc);
                }
            }

            var paymentMethods = new[]
            {
                new PaymentMethod
                {
                    User = users[0],
                    Type = PaymentType.BankAccount,
                    BankAccount = bankAccounts[0]
                },

                new PaymentMethod
                {
                    User = users[0],
                    Type = PaymentType.BankAccount,
                    BankAccount = bankAccounts[1]
                },

                new PaymentMethod
                {
                    User = users[0],
                    Type = PaymentType.CreditCard,
                    CreditCard = creditCards[0]
                },

                new PaymentMethod
                {
                    User = users[1],
                    Type = PaymentType.CreditCard,
                    CreditCard = creditCards[1]
                },

                new PaymentMethod
                {
                    User = users[2],
                    Type = PaymentType.BankAccount,
                    BankAccount = bankAccounts[2]
                },

                new PaymentMethod
                {
                    User = users[2],
                    Type = PaymentType.CreditCard,
                    CreditCard = creditCards[2]
                },

                new PaymentMethod
                {
                    User = users[2],
                    Type = PaymentType.CreditCard,
                    CreditCard = creditCards[3]
                },

                new PaymentMethod
                {
                    User = users[3],
                    Type = PaymentType.BankAccount,
                    BankAccount = bankAccounts[3]
                }
            };

            foreach (var payment in paymentMethods)
            {
                if (IsValid(payment))
                {
                    this._context.PaymentMethods.Add(payment);
                }
            }

            this._context.SaveChanges();

            Console.WriteLine($"Database was filled with data. :)");
        }

        private bool IsValid(object entity)
        {
            ValidationContext context = new ValidationContext(entity);
            List<ValidationResult> results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, context, results, true);

            return isValid;
        }
    }
}
