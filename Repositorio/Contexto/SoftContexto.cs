using Microsoft.EntityFrameworkCore;
using Repositorio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio.Contexto
{
    public class SoftContexto : DbContext

    {
        public SoftContexto()
        {
            //Criar ou atualizar o banco de dados
            this.Database.EnsureCreated();
        }

        public DbSet<Plataforma> plataformas { get; set; }

        public DbSet<Login> login { get; set; }

        public DbSet<CategoriaProblema> categoriasproblemas { get; set; }

        public DbSet<Filme> filme { get; set; }

        public DbSet<Cargo> cargo { get; set; }

        public DbSet<Empresa> empresa { get; set; }

        public DbSet<Venda> venda { get; set; }

        public DbSet<Status> status { get; set; }


        //Não sei o que é isso:
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //var stringConexao = @"Server=JULIA;Database=Atendimento9;Integrated Security=true;Trust Server Certificate=true;";



            var stringConexao = @"Server=FELIPE_SANCHES;DataBase=_compusoftatendimento;integrated security=true;Trust Server Certificate=true";
            //var stringConexao = @"Server=sql8005.site4now.net;DataBase=db_a98978_felipesanches;user id=db_a98978_felipesanches_admin;password=felipe98767";

            if (!optionsBuilder.IsConfigured)
          
            {
                optionsBuilder.UseSqlServer(stringConexao);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plataforma>(entidade =>
            {
                entidade.HasKey(e => e.id);// definindo: chave primaria
                entidade.Property(e => e.descricao).HasMaxLength(30);//qtd max caracteres
            });

            modelBuilder.Entity<Filme>(entidade => 
            {
                entidade.HasKey(e => e.id);
                entidade.Property(e => e.descricao).HasMaxLength(150);
                //precisao do decimal:
                entidade.Property(e => e.valorInteira).HasPrecision(8, 2);
                entidade.Property(e => e.valorMeia).HasPrecision(8, 2);
                entidade.Property(e => e.duracao).HasPrecision(8, 2);
                //tentando bool 
                entidade.Property(e => e.estaDisponivel); 

                /*criando uma relação: (filme só tem 1 categoria:
                entidade.HasOne(e => e.categoria) //o lado da rel. que tem Um
                .WithMany(c => c.filmes) //o lado da rel. que tem Muitos
                .HasForeignKey(e => e.idCategoria) //prop chave estrangeira
                .HasConstraintName("FK_Filme_Categoria") //nome do relacionamento
                .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao
                */

            });
           
            modelBuilder.Entity<CategoriaProblema>(entidade =>
            {
                entidade.HasKey(e => e.id);
                entidade.Property(e => e.descricao).HasMaxLength(30);

            });

            //VendaFilme
            modelBuilder.Entity<VendaIngresso>(entidade =>
            {
                entidade.HasKey(e => e.id);
                entidade.Property(e => e.qtde).HasPrecision(8, 2);
                entidade.Property(e => e.valor).HasPrecision(8, 2);
                entidade.Property(e => e.tipoIngresso).HasMaxLength(150);
                //criando uma relação:
                entidade.HasOne(e => e.vendas) //o lado da rel. que tem Um
                .WithMany(c => c.vendasingressos) //o lado da rel. que tem Muitos
                .HasForeignKey(e => e.idVenda) //prop chave estrangeira
                .HasConstraintName("FK_Venda_IngressoVenda") //nome do relacionamento

                .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao
                entidade.HasOne(e => e.filmes) //o lado da rel. que tem Um
               .WithMany(c => c.vendasingressos) //o lado da rel. que tem Muitos
               .HasForeignKey(e => e.idFilme) //prop chave estrangeira
               .HasConstraintName("FK_Filmes_VendaIngresso") //nome do relacionamento
               .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao

            });


            modelBuilder.Entity<Venda>(entidade =>
            {
                entidade.HasKey(e => e.id);
                //Tentando datetime
                entidade.Property(e => e.dataVenda);
                entidade.Property(e => e.valor).HasPrecision(8, 2);
                entidade.Property(e => e.tipoIngresso).HasMaxLength(150);
                entidade.Property(e => e.url).HasMaxLength(500);
                entidade.Property(e => e.idPreferencia).HasMaxLength(500);
                //relacionamento
                entidade.HasOne(e => e.status) //prop lado um
                .WithMany(c => c.vendas) //prop lado Muitos
                .HasForeignKey(e => e.idStatus) //prop chave estrangeira
                .HasConstraintName("FK_Vendas_Status") //nome do relacionamento
                .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao
            });


            modelBuilder.Entity<Status>(entidade => {
                entidade.HasKey(e => e.id);//chave primaria
                //qtde max caracteres
                entidade.Property(e => e.descricao).HasMaxLength(150);

            });


            modelBuilder.Entity<Cargo>(entidade => {
                entidade.HasKey(e => e.id);
                entidade.Property(e => e.descricao).HasMaxLength(20);               
    
            });

            modelBuilder.Entity<Empresa>(entidade =>
            {
                entidade.HasKey(e => e.id);
                entidade.Property(e => e.razaosocial).HasMaxLength(30);
                entidade.Property(e => e.cnpj).HasMaxLength(20);
                entidade.Property(e => e.telefone).HasMaxLength(20);
                entidade.Property(e => e.celular).HasMaxLength(20);
                entidade.Property(e => e.email).HasMaxLength(150);
                entidade.Property(e => e.cep).HasMaxLength(150);
                entidade.Property(e => e.logradouro).HasMaxLength(30);
                entidade.Property(e => e.numero).HasMaxLength(20);
                entidade.Property(e => e.bairro).HasMaxLength(30);
                entidade.Property(e => e.cidade).HasMaxLength(30);
                entidade.Property(e => e.estado).HasMaxLength(30);
                entidade.Property(e => e.complemento).HasMaxLength(30);

            });

            modelBuilder.Entity<Login>(entidade => {
                entidade.HasKey(e => e.id);
                entidade.Property(e => e.login).HasMaxLength(150);
                entidade.Property(e => e.senha).HasMaxLength(150);
                entidade.Property(e => e.ativo);
                //criando uma relação: (filme só tem 1 categoria:
                entidade.HasOne(e => e.cargo) //o lado da rel. que tem Um
                .WithMany(c => c.usuarios) //o lado da rel. que tem Muitos
                .HasForeignKey(e => e.idCargo) //prop chave estrangeira
                .HasConstraintName("FK_Login_Cargo") //nome do relacionamento
                .OnDelete(DeleteBehavior.NoAction); //configuração da exclusao
            });
        }

    }
}
