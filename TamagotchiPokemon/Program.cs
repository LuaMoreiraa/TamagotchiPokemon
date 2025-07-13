using RestSharp;
using System;
using System.IO;
using System.Threading.Tasks;
using TamagotchiPokemon.Models;
using TamagotchiPokemon.Services;
using TamagotchiPokemon.Views;
using TamagotchiPokemon.Artes;

namespace TamagotchiPokemon
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "TAMAGOTCHI POKÉMON";

            Console.WriteLine("Bem-vindo ao");
            Console.WriteLine(MainLogo.logo);

            Console.Write("Qual é seu nome? ");
            string nomeUsuario = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(nomeUsuario))
            {
                Console.WriteLine("Nome inválido. Encerrando o jogo.");
                return;
            }


            string pastaAscii = Path.Combine(AppContext.BaseDirectory, "Artes");

            if (!Directory.Exists(pastaAscii))
            {
                Console.WriteLine($"Pasta de artes não foi encontrada: {pastaAscii}");
                Console.ReadKey();
                return;
            }

            var restClient = new RestClient("https://pokeapi.co/api/v2/");
            IPokeApiService pokeApiService = new PokeApiService(restClient);

            var view = new TamagotchiView();
            var arteController = new ArteController(pastaAscii);
            var mascoteController = new MascoteController(pokeApiService, view, arteController);
            var gameController = new GameController(view, mascoteController, nomeUsuario);

            await gameController.IniciarAsync();

            Console.WriteLine("Obrigado por jogar! Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
