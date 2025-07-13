using System.Collections.Generic;
using System.Threading.Tasks;
using TamagotchiPokemon.Models;

namespace TamagotchiPokemon.Services
{
    public interface IPokeApiService
    {
        Task<List<Pokemon>> ObterListaPokemonsAsync(int limite);
        Task<Pokemon> ObterDetalhesPokemonAsync(string nome);
    }
}
