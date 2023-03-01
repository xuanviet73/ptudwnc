﻿using System;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;

namespace TatBlog.WinApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BlogDbContext();

            var seeder = new DataSeeder(context);

            var authors = context.Author.ToList();

            Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12}"
                "ID", "Full Name", "Email", "Joined Date");
            foreach(var author in authors)
            {
                Console.WriteLine("{0,-4}{1,-30}{2,-30}{3,12:MM/dd/yyy}",
                    author.Id, author.FullName, author.Email, author.JoinedDate);
            }

        }
    }
}
