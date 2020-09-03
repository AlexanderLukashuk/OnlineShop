using System;
using System.Collections.Generic;
using OnlineShop.Services;
using Microsoft.Extensions.Configuration;

namespace OnlineShop
{
    class Program
    {
        //*
        //* Онлайн магазин ноутбуков
        //* 1) Регистрация и вход на основе email и телефона (на выбор юзера)
        //* 2) Изменение профиля
        //* 3) Категории товаров
        //* 4) Товары с сортировкой (цены, количество, лайки и прочее)
        //* 5) Покупка (+2 балла тому, кто реализует с помощью платежной системы)
        //* 6) Лайки и комменты (цепочки комментариев)
        //* 7) Доставка товаров
        //* 8) Бонус. Бот на телергамме для отслеживания товаров (+1 балл на экзамен)
        //* 
        //* Во всех указанных способах взаимодействия с БД (подключенный уровень, EF, Dapper)
        //* 
        static void Main(string[] args)
        {
            /*List<User> users = new List<User>();
            User sanya = new User("XxX_666_SANYA_xXx", "1234");
            User john = new User("John", "nhoJ");
            AuthorizationService service = new AuthorizationService();
            //service.Authentication(user, users);
            if (service.Authentication(sanya, users))
            {
                Console.WriteLine("Registration was succesful");
                //service.Authentication(sanya, users);
            }
            else
            {
                Console.WriteLine("registrtion failed");
            }
            service.Authentication(john, users);

            foreach (var user in users)
            {
                Console.WriteLine($"Login: {user.GetLogin()}");
            }*/

            /*
                Пока не выбран флаг выхода
                вывести на экран меню
                при выботе регистрации выполнить
                код связанный с базой данных
            */
            // CRUD - действия
            // Create Read Update Delete
            
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();

            var isAdmin = bool.Parse(configuration["isAdmin"]);
            
            var connectionString = configuration.GetSection("connectionStrings")["testDb"];
            Console.WriteLine(isAdmin);
            //Console.WriteLine(connectionString);
            //Console.Read();
        }
    }
}
