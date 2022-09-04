using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using SWII6_TP01;

internal class Program
{
    private static void Main(string[] args)
    {
        new Books();
        new BookCSV();

        IWebHost host = new WebHostBuilder().UseKestrel().UseStartup<Startup>().Build();
        host.Run();
    }
}