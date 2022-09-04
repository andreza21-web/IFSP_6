using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWII6_TP01
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);

            builder.MapRoute("/livro/{index:int}/nome", BookNames);
            builder.MapRoute("/livro/{index:int}/TotalToString", TotalToString);
            builder.MapRoute("/livro/{index:int}/Autores", NameAuthors);
            builder.MapRoute("/livro/ApresentaLivro", GerarHtml);
            builder.MapRoute("/creditos", CreditosAlunos);
            var rotas = builder.Build();

            app.UseRouter(rotas);

            app.Run(Roteamento);
        }

        public Task Roteamento(HttpContext context)
        {
            var _repo = new BookCSV();

            var caminhosAtendidos = new Dictionary<string, RequestDelegate>
            {
                { "/livro/ApresentaLivro", GerarHtml}
             };

            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhosAtendidos[context.Request.Path];
                return metodo.Invoke(context);
            }

            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Caminho inexistente.");

        }
        public Task BookNames(HttpContext context)
        {
           
            int bookIndex = Convert.ToInt32(context.GetRouteValue("index"));
            var _repo = new BookCSV();

            var book = _repo.Books[bookIndex];

            var htmlFIle = LoadHTMLFile("BookNames");
            htmlFIle = htmlFIle.Replace("#NOME_LIVRO", book.Name);

            return context.Response.WriteAsync(htmlFIle);

        }
        public Task TotalToString(HttpContext context)
        {
            int bookIndex = Convert.ToInt32(context.GetRouteValue("index"));
            var _repo = new BookCSV();

            var book = _repo.Books[bookIndex];

            var htmlFIle = LoadHTMLFile("ToString");
            htmlFIle = htmlFIle.Replace("#RESULTADO", book.ToString());

            return context.Response.WriteAsync(htmlFIle);

        }
        public Task NameAuthors(HttpContext context)
        {
          
            int bookIndex = Convert.ToInt32(context.GetRouteValue("index"));
            var _repo = new BookCSV();

            var book = _repo.Books[bookIndex];
            var htmlFIle = LoadHTMLFile("NameAuthors");
            htmlFIle = htmlFIle.Replace("#RESULTADO", book.getAuthorNames());

            return context.Response.WriteAsync(htmlFIle);

        }
        public Task GerarHtml(HttpContext context)
        {
            int bookIndex = Convert.ToInt32(context.GetRouteValue("index"));
            var _repo = new BookCSV();
            var book = _repo.Books[bookIndex];

            var htmlFIle = LoadHTMLFile("Book");

            htmlFIle = htmlFIle.Replace("#BOOK-NAME#", book.Name);
            foreach (Author author in book.Authors)
            {
                htmlFIle = htmlFIle.Replace("#AUTHOR#", $"<li>{author.Name}</li>#AUTHOR#");
            }
            htmlFIle = htmlFIle.Replace("#AUTHOR#", "");

            return context.Response.WriteAsync(htmlFIle);
        }


        public Task CreditosAlunos(HttpContext context)
        {
            var htmlFIle = LoadHTMLFile("CreditosAlunos");

            return context.Response.WriteAsync(htmlFIle);

        }
        private string LoadHTMLFile(string fileName)
        {
            var fullPath = $@"C:\Ambiente\projetos\6-semestre\SWII6-TP01\SWII6-TP01\HTML\{fileName}.html";
            using (var arquivo = File.OpenText(fullPath))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
