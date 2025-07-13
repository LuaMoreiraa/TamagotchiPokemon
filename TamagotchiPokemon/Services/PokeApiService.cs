using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using TamagotchiPokemon.Models;

namespace TamagotchiPokemon.Services
{
    public class PokeApiService : IPokeApiService
    {
        private readonly RestClient _client;

        public PokeApiService(RestClient client)
        {
            _client = client;
        }

        public async Task<List<Pokemon>> ObterListaPokemonsAsync(int limite)
        {
            var pokemons = new List<Pokemon>();

            var request = new RestRequest($"pokemon?limit={limite}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new Exception("Falha ao obter lista de pokemons");

            var json = JObject.Parse(response.Content);
            var results = json["results"];

            foreach (var item in results)
            {
                string nome = item["name"].ToString();
                pokemons.Add(new Pokemon { Nome = Capitalizar(nome) });
            }

            return pokemons;
        }

        public async Task<Pokemon> ObterDetalhesPokemonAsync(string nome)
        {
            var request = new RestRequest($"pokemon/{nome.ToLower()}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                throw new Exception($"Falha ao obter detalhes do pokemon {nome}");

            var json = JObject.Parse(response.Content);

            var pokemon = new Pokemon
            {
                Nome = Capitalizar(json["name"].ToString()),
                Altura = (int)json["height"],
                Peso = (int)json["weight"],
                Tipos = new List<string>(),
                Habilidades = new List<string>()
            };

            foreach (var tipo in json["types"])
            {
                pokemon.Tipos.Add(tipo["type"]["name"].ToString());
            }

            foreach (var habilidade in json["abilities"])
            {
                pokemon.Habilidades.Add(habilidade["ability"]["name"].ToString());
            }

            pokemon.Descricao = await ObterDescricaoSpeciesAsync(nome);

            return pokemon;
        }

        private async Task<string> ObterDescricaoSpeciesAsync(string nome)
        {
            var request = new RestRequest($"pokemon-species/{nome.ToLower()}", Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return string.Empty;

            var json = JObject.Parse(response.Content);

            var flavorEntries = json["flavor_text_entries"];
            foreach (var entry in flavorEntries)
            {
                if (entry["language"]["name"].ToString() == "en")
                {
                    string text = entry["flavor_text"].ToString();
                    return text.Replace("\n", " ").Replace("\f", " ");
                }
            }

            return string.Empty;
        }

        private string Capitalizar(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;
            return char.ToUpper(texto[0]) + texto.Substring(1);
        }
    }
}
