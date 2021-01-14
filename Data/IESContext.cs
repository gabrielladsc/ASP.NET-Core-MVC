﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capitulo01.Models;
using Microsoft.EntityFrameworkCore;

namespace Capitulo01.Data
{
    public class IESContext : DbContext
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options){}

        public DbSet<Departamento> Departamentos { get; set; }

        public DbSet<Instituicao> Instituicoes { get; set; }

        public class IESDbInitializer
        {
            public static void Initialize(IESContext context)
            {
                context.Database.EnsureCreated();

                if (context.Departamentos.Any())
                {
                    return;
                }

                var departamentos = new Departamento[]
                {
                    new Departamento { Nome = "Ciência de Computação" },
                    new Departamento { Nome = "Ciência de Alimentos" }
                };

                foreach (Departamento d in departamentos)
                {
                    context.Departamentos.Add(d);
                }

                context.SaveChanges();
            }
        }
    }
}
