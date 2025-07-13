using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiPokemon.Models
{
    public enum EstadoTamagotchi
    {
        Normal,
        Brincando,
        Banhar,
        Dormindo,
        SeAlimentando,
        Doente,
        Triste,
        Morto
    }

    public enum AcaoUsuario
    {
        Sair = 0,
        Alimentar = 1,
        Brincar = 2,
        Dormir = 3,
        VerStatus = 4,
        Banhar = 5,
        Medicar = 6
    }

}
