﻿using AppGmz.DAL;
using AppGmz.Models.DomainModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Linq;

namespace AppGmz.Services.PrepDb
{
    public static class PrepDb
    {
        public static void SetDate(IApplicationBuilder app)
        {
            using (var service = app.ApplicationServices.CreateScope())
            {
                SeedData(service.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            Console.WriteLine("Application migration ...");
            context.Database.Migrate();

            if (!context.RecordNewses.Any())
            {
                Console.WriteLine("Adding data ....");
                context.RecordNewses.AddRange(new RecordNews()
                {
                    Body = "First text",
                    DateTime = DateTime.Now,
                    Header = "First Header",
                    ShortDescription = "Description first record"
                }, 
                    new RecordNews()
                    {
                        Body = "Lorem ipsum dolor sit amet consectetur adipisicing elit." +
                               " Quo, inventore! Ipsa, deserunt. Magni ducimus, ratione aspernatur sunt dicta itaque, accusamus harum odio",
                               DateTime = DateTime.Now,
                        Header = "Second Header Test Header",
                        ShortDescription = "Description second record"
                    });

                Log.Information(nameof(SeedData), "add data is completed");
                context.SaveChanges();
            }
            else
            {
                Log.Information(nameof(SeedData), "Data already add");
                Console.WriteLine("Data already add");
            }
        }
    }
}