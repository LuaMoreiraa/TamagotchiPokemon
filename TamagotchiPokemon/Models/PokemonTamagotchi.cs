using TamagotchiPokemon.Models;

public class PokemonTamagotchi
{
    public Pokemon PokemonBase { get; }

    public int Fome { get; private set; } = 100;
    public int Felicidade { get; private set; } = 100;
    public int Energia { get; private set; } = 100;
    public int Saude { get; private set; } = 100;
    public int Banho { get; private set; } = 100;
    public string? MotivoMorte { get; private set; } = null;


    public bool Doente => Saude < 50;
    public bool Vivo { get; private set; } = true;

    public PokemonTamagotchi(Pokemon pokemon)
    {
        PokemonBase = pokemon;
    }

    // Alimentar o pokemon vai aumenta a fome e felicidade vai diminui a energia
    public void Alimentar()
    {
        if (!Vivo) return;

        Fome = Math.Min(Fome + 30, 100);
        Felicidade = Math.Min(Felicidade + 10, 100);
        Energia = Math.Max(Energia - 10, 0);
        Banho = Math.Max(Banho - 5, 0);
        AtualizarEstado();
    }

    // Brincar com o Pokemon vai aumentar a felicidade vai diminuir energia e fome 
    public void Brincar()
    {
        if (!Vivo) return;

        Felicidade = Math.Min(Felicidade + 20, 100);
        Energia = Math.Max(Energia - 20, 0);
        Fome = Math.Max(Fome - 15, 0);
        Banho = Math.Max(Banho - 10, 0);
        AtualizarEstado();
    }

    // Colocar o Pokemon para dormir recupera energia mas pode diminuir a fome e diminuir a higiene
    public void Dormir()
    {
        if (!Vivo) return;

        Energia = Math.Min(Energia + 40, 100);
        Fome = Math.Max(Fome - 20, 0);
        Banho = Math.Max(Banho - 15, 0);
        AtualizarEstado();
    }

    // Banhar aumenta Banho
    public void Banhar()
    {
        if (!Vivo) return;

        Banho = Math.Min(Banho + 50, 100);
        AtualizarEstado();
    }

    // Medicar seu pokemon recupera a saúde se ele ficar doente
    public void Medicar()
    {
        if (!Vivo) return;

        if (Doente)
        {
            Saude = Math.Min(Saude + 50, 100);
            if (Saude >= 50)
            {
                Console.WriteLine($"{PokemonBase.Nome} está curado!");
            }
            else
            {
                Console.WriteLine($"{PokemonBase.Nome} ainda precisa de cuidados.");
            }
        }
        else
        {
            Console.WriteLine($"{PokemonBase.Nome} não está doente.");
        }
    }

public void AtualizarEstado()
    {
        if (!Vivo) return;

        if (Fome <= 0)
        {
            Vivo = false;
            MotivoMorte = "morreu de fome ;C";
            return;
        }

        if (Saude <= 0)
        {
            Vivo = false;
            MotivoMorte = "Morreu doente e sem cuidados :C";
            return;
        }

        if (Banho < 20)
        {
            Saude = Math.Max(Saude - 10, 0);
        }

        if (Energia < 20)
        {
            Felicidade = Math.Max(Felicidade - 10, 0);
        }

        if (Felicidade <= 0)
        {
            Saude = Math.Max(Saude - 15, 0);
        }

        if (Saude <= 10)
        {
            Vivo = false;
            MotivoMorte = "Estava xoxo, capenga, manco, amenico, frágil e inconsistente... :C";
        }
    }
}
