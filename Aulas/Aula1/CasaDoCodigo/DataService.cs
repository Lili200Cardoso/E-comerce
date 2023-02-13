using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;
        public DataService(ApplicationContext contexto,
            IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            //EnsureCreated();
            //caso o banco de dados não exista, esse comando o criará!
            //O único problema  é que depois que vc usa esse método vc não poderá mais gerar nenhuma migration no seu sistema!
            //Por isso trocaremos esse método pelo o método Migrate();
            contexto.Database.Migrate();

            List<Livro> livros = GetLivros();

            produtoRepository.SaveProdutos(livros);
            
        }
        private static List<Livro> GetLivros()
        {
            var json = File.ReadAllText("livros.json");
            //convertendo um arquivo Json
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
            return livros;
        }


    }

  
}
