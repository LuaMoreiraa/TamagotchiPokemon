using TamagotchiPokemon.Views;
using System.Threading.Tasks;
using System;

public class GameController
{
    private readonly TamagotchiView _view;
    private readonly MascoteController _mascoteController;
    private readonly string _nomeUsuario;

    public GameController(TamagotchiView view, MascoteController mascoteController, string nomeUsuario)
    {
        _view = view;
        _mascoteController = mascoteController;
        _nomeUsuario = nomeUsuario;
    }

    public async Task IniciarAsync()
    {
        bool executando = true;

        while (executando)
        {
            Console.Clear();
            Console.WriteLine("===============================================================");
            Console.WriteLine($"Olá treinador {_nomeUsuario.ToUpper()}, o que você deseja fazer?");
            Console.WriteLine("1 - Adotar um Pokémon virtual");
            Console.WriteLine("2 - Ver seus Pokémons");
            Console.WriteLine("3 - Interagir com um Pokémon adotado");
            Console.WriteLine("4 - Excluir Pokémon");
            Console.WriteLine("5 - Sair");
            Console.WriteLine("===============================================================");

            int menuOpcao = _view.LerOpcao(1, 5);

            switch (menuOpcao)
            {
                case 1:
                    await _mascoteController.AdotarPokemonAsync();
                    break;

                case 2:
                    _mascoteController.ListarMascotes();
                    break;

                case 3:
                    await _mascoteController.SelecionarMascoteParaInteragir();
                    break;

                case 4:
                    _mascoteController.RemoverMascote();
                    break;

                case 5:
                    executando = false;
                    Console.Clear();
                    _view.MostrarMensagem("Até logo, treinador!");
                    _view.EsperarEnter();
                    break;
            }
        }
    }
}
