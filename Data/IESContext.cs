using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modelo.Cadastros;
using Microsoft.EntityFrameworkCore;
using Modelo.Discente;
using Capitulo01.Models.Infra;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Capitulo01.Data
{
    public class IESContext : IdentityDbContext<UsuarioDaAplicacao>
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options){}

        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<Academico> Academicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CursoDisciplina>()
                .HasKey(cd => new { cd.CursoID, cd.DisciplinaID });

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(c => c.Curso)
                .WithMany(c => c.CursosDisciplinas)
                .HasForeignKey(c => c.CursoID);

            modelBuilder.Entity<CursoDisciplina>()
                .HasOne(c => c.Disciplina)
                .WithMany(c => c.CursosDisciplinas)
                .HasForeignKey(c => c.DisciplinaID);
        }

        public class IESDbInitializer
        {
            public static void Initialize(IESContext context)
            {
                context.Database.EnsureCreated();

                if (context.Departamentos.Any())
                {
                    return;
                }

                var instituicoes = new Instituicao[]
                {
                    new Instituicao { Nome = "UniParaná", Endereco = "Paraná"},
                    new Instituicao { Nome = "UniCarioca", Endereco = "Rio de Janeiro"}
                };

                foreach (Instituicao i in instituicoes)
                {
                    context.Instituicoes.Add(i);
                }
                context.SaveChanges();

                var departamentos = new Departamento[]
                {
                    new Departamento { Nome = "Ciência de Computação", InstituicaoID = 1 },
                    new Departamento { Nome = "Ciência de Alimentos", InstituicaoID = 2 }
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
