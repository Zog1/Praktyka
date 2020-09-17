using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.Services.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRental.Services
{
    /// <summary>
    ///     The responsibility of this class is adding Data Seed to new database.
    /// </summary>
    public class ApplicationDbContextDataSeed
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            InitializeUsers(context);
            InitializeCars(context);
            await context.SaveChangesAsync();
        }

        private static void InitializeUsers(ApplicationDbContext context)
        {
            var passwordHasher = new PasswordHasher();
            var admin = GetAdminAccount(passwordHasher);
            var worker = GetWorkerAccount(passwordHasher);
            List<User> users = new List<User>() { admin, worker };
            foreach (var user in users)
            {
                if (!context.Users.Any(u => u.IdentificationNumber == user.IdentificationNumber))
                {
                    context.Users.Add(user);
                }
            }
        }

        private static void InitializeCars(ApplicationDbContext context)
        {
            var cars = GetCars();
            foreach (var car in cars)
            {
                if(!context.Cars.Any(u => u.RegistrationNumber == car.RegistrationNumber))
                {
                    context.Cars.Add(car);
                }
            }
        }

        private static User GetAdminAccount(PasswordHasher passwordHasher)
        {
            var saltedHash = passwordHasher.GenerateSaltedHash(16, "admin");
            var admin = new User
            {
                FirstName = "Bohdan",
                LastName = "Doe",
                IdentificationNumber = "000000",
                Email = "admin@admin.com",
                MobileNumber = "458963254",
                RoleOfUser = RoleOfWorker.Admin,
                CodeOfVerification = "7py2",
                HashPassword = saltedHash.Hash,
                Salt = saltedHash.Salt,
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            return admin;
        }
        private static User GetWorkerAccount(PasswordHasher passwordHasher)
        {
            var saltedHash = passwordHasher.GenerateSaltedHash(16, "worker");
            var worker = new User
            {
                FirstName = "John",
                LastName = "Brandon",
                IdentificationNumber = "000111",
                Email = "worker1@worker.com",
                MobileNumber = "44443254",
                RoleOfUser = RoleOfWorker.Worker,
                CodeOfVerification = "89x2",
                HashPassword = saltedHash.Hash,
                Salt = saltedHash.Salt,
                DateCreated = DateTime.Now,
                IsDeleted = false
            };
            return worker;
        }

        private static IEnumerable<Car> GetCars()
        {
            return new List<Car>(){
                new Car
                {
                    Brand = "Audi",
                    Model = "Q5",
                    YearOfProduction = 2019,
                    ImagePath = "https://pngimg.com/uploads/audi/audi_PNG1737.png",
                    RegistrationNumber = "SZE4562",
                    NumberOfDoor = 5,
                    NumberOfSits = 5,
                    TypeOfCar = CarType.Classic,
                    DateCreated = DateTime.Now,
                    IsDeleted = false
                },
                new Car
                {
                    Brand = "Mazda",
                    Model = "Mx-5",
                    YearOfProduction = 2018,
                    ImagePath = "https://pngimg.com/uploads/mazda/mazda_PNG133.png",
                    RegistrationNumber = "SZE4558",
                    NumberOfDoor = 3,
                    NumberOfSits = 2,
                    TypeOfCar = CarType.Sport,
                    DateCreated = DateTime.Now,
                    IsDeleted = false
                },
                new Car
                {
                    Brand = "Ford",
                    Model = "Mustang",
                    YearOfProduction = 2014,
                    ImagePath = "https://pngimg.com/uploads/mustang/mustang_PNG40656.png",
                    RegistrationNumber = "SZE4578",
                    NumberOfDoor = 3,
                    NumberOfSits = 2,
                    TypeOfCar = CarType.Sport,
                    DateCreated = DateTime.Now,
                    IsDeleted = false
                },
                new Car
                {
                    Brand = "Opel",
                    Model = "Corsa",
                    YearOfProduction = 2015,
                    ImagePath = "https://pngimg.com/uploads/opel/opel_PNG40.png",
                    RegistrationNumber = "SG54571",
                    NumberOfDoor = 5,
                    NumberOfSits = 5,
                    TypeOfCar = CarType.Classic,
                    DateCreated = DateTime.Now,
                    IsDeleted = false
                },
                new Car
                {
                    Brand = "Opel",
                    Model = "Crossland X",
                    YearOfProduction = 2018,
                    ImagePath = "https://pngimg.com/uploads/opel/opel_PNG44.png",
                    RegistrationNumber = "SG99571",
                    NumberOfDoor = 5,
                    NumberOfSits = 5,
                    TypeOfCar = CarType.Classic,
                    DateCreated = DateTime.Now,
                    IsDeleted = false
                }
            };
        }
    }
}
