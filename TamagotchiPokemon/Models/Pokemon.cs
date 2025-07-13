using System.Collections.Generic;

namespace TamagotchiPokemon.Models
{
    public class Pokemon
    {
        public string Nome { get; set; } = string.Empty;

        public List<string> Tipos { get; set; } = new List<string>();

        public int Altura { get; set; }

        public int Peso { get; set; }

        public List<string> Habilidades { get; set; } = new List<string>();

        public string Descricao { get; set; } = string.Empty;

    }
}
