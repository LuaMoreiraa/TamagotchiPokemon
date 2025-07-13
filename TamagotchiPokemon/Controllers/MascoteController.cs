using TamagotchiPokemon.Models;
using TamagotchiPokemon.Services;
using TamagotchiPokemon.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MascoteController
{
    private readonly IPokeApiService _pokeApiService;
    private readonly TamagotchiView _view;
    private readonly ArteController _arteController;

    private readonly List<PokemonTamagotchi> _mascotesAdotados;

    public MascoteController(IPokeApiService pokeApiService, TamagotchiView view, ArteController arteController)
    {
        _pokeApiService = pokeApiService ?? throw new ArgumentNullException(nameof(pokeApiService));
        _view = view ?? throw new ArgumentNullException(nameof(view));
        _arteController = arteController ?? throw new ArgumentNullException(nameof(arteController));

        _mascotesAdotados = new List<PokemonTamagotchi>();
    }

    public async Task AdotarPokemonAsync()
    {
        List<Pokemon> listaPokemons;

        try
        {
            listaPokemons = await _pokeApiService.ObterListaPokemonsAsync(15);
        }
        catch (Exception ex)
        {
            _view.MostrarMensagem($"Erro ao carregar pokémons: {ex.Message}");
            return;
        }

        bool confirmou = false;

        while (!confirmou)
        {
            Console.Clear();
            _view.MostrarMenuPokemons(listaPokemons.ConvertAll(p => p.Nome));
            _view.MostrarMensagem("Escolha o Pokémon para ver detalhes ou 0 para sair:");
            int opcao = _view.LerOpcao(0, listaPokemons.Count);
            if (opcao == 0) return;

            var pokemon = listaPokemons[opcao - 1];
            pokemon = await _pokeApiService.ObterDetalhesPokemonAsync(pokemon.Nome);

            Console.Clear();
            _view.MostrarMensagem($"Informações de {pokemon.Nome.ToUpper()}:");
            _view.MostrarMensagem($"Altura: {pokemon.Altura}");
            _view.MostrarMensagem($"Peso: {pokemon.Peso}");
            _view.MostrarMensagem("Tipos: " + string.Join(", ", pokemon.Tipos));

            _view.MostrarMensagem("\n1 - Confirmar adoção");
            _view.MostrarMensagem("2 - Voltar para lista");
            _view.MostrarMensagem("0 - Cancelar");

            int escolha = _view.LerOpcao(0, 2);
            switch (escolha)
            {
                case 1:
                    var novoMascote = new PokemonTamagotchi(pokemon);

                    Console.WriteLine($"DEBUG: Antes da adição: {_mascotesAdotados.Count} mascotes");
                    _mascotesAdotados.Add(novoMascote);
                    Console.WriteLine($"DEBUG: Depois da adição: {_mascotesAdotados.Count} mascotes");

                    _arteController.MostrarArte("Pokebola");
                    _view.MostrarMensagem($"{pokemon.Nome} foi adotado! Abrindo Pokébola...");
                    _view.EsperarEnter();

                    await InteragirComMascote(novoMascote);
                    confirmou = true;
                    break;

                case 2:
                    break;

                case 0:
                    return;
            }
        }
    }

    private async Task InteragirComMascote(PokemonTamagotchi mascote)
    {
        if (mascote == null) return;

        bool sair = false;

        while (!sair && mascote.Vivo)
        {
            Console.Clear();
            _view.MostrarMenuInteracoes();

            int opcao = _view.LerOpcao(0, 6);

            switch ((AcaoUsuario)opcao)
            {
                case AcaoUsuario.Sair:
                    sair = true;
                    break;

                case AcaoUsuario.Alimentar:
                    mascote.Alimentar();
                    _arteController.MostrarArte("Se_alimentando");
                    _view.MostrarMensagem("Alimentado!");
                    break;

                case AcaoUsuario.Brincar:
                    mascote.Brincar();
                    _arteController.MostrarArte("Brincando");
                    _view.MostrarMensagem("Brincando!");
                    break;

                case AcaoUsuario.Dormir:
                    mascote.Dormir();
                    _arteController.MostrarArte("Dormindo");
                    _view.MostrarMensagem("Dormindo...");
                    break;

                case AcaoUsuario.VerStatus:
                    _arteController.MostrarArte("Normal");
                    _view.MostrarStatus(
                        mascote.PokemonBase.Nome,
                        mascote.Fome,
                        mascote.Energia,
                        mascote.Felicidade,
                        mascote.Banho,
                        mascote.Saude,
                        mascote.Vivo);
                    break;

                case AcaoUsuario.Banhar:
                    mascote.Banhar();
                    _arteController.MostrarArte("Banhar");
                    _view.MostrarMensagem("Estou tomando banho, que friooo!!...");
                    break;

                case AcaoUsuario.Medicar:
                    mascote.Medicar();
                    _arteController.MostrarArte("Medicar");
                    _view.MostrarMensagem("Medicando...");
                    break;
            }

            _view.EsperarEnter();
            mascote.AtualizarEstado();

            if (!mascote.Vivo)
            {
                _arteController.MostrarArte("Morto");
                _view.MostrarMensagem(mascote.MotivoMorte ?? $"{mascote.PokemonBase.Nome} morreu por falta de cuidado... :C");
                _view.EsperarEnter();
                sair = true;
            }
        }
    }

    public void ListarMascotes()
    {
        if (_mascotesAdotados.Count == 0)
        {
            _view.MostrarMensagem("Você ainda não adotou nenhum mascote, Volte ao menu e escolha seu Pokémon");
            _view.EsperarEnter();
            return;
        }

        while (true)
        {
            Console.Clear();
            _view.MostrarMensagem("Escolha um mascote para ver detalhes:");

            for (int i = 0; i < _mascotesAdotados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_mascotesAdotados[i].PokemonBase.Nome}");
            }
            Console.WriteLine("0. Voltar");

            int opcao = _view.LerOpcao(0, _mascotesAdotados.Count);
            if (opcao == 0)
                return;

            var mascote = _mascotesAdotados[opcao - 1];

            if (mascote.Vivo)
            {
                _arteController.MostrarArte("Normal");
            }
            else
            {
                _arteController.MostrarArte("Morto");
            }

            _view.MostrarMensagem($"Detalhes do mascote: {mascote.PokemonBase.Nome}");
            _view.MostrarMensagem($"Status do mascote:");
            _view.MostrarMensagem($"- Vivo: {(mascote.Vivo ? "Sim" : "Não")}");
            _view.MostrarMensagem($"- Fome: {mascote.Fome}");
            _view.MostrarMensagem($"- Banho: {mascote.Banho}");
            _view.MostrarMensagem($"- Energia: {mascote.Energia}");
            _view.MostrarMensagem($"- Felicidade: {mascote.Felicidade}");
            _view.MostrarMensagem($"- Saúde: {mascote.Saude}");

            _view.EsperarEnter();
        }
    }

    public async Task SelecionarMascoteParaInteragir()
    {
        if (_mascotesAdotados.Count == 0)
        {
            _view.MostrarMensagem("Você ainda não adotou nenhum mascote para interagir.");
            _view.EsperarEnter();
            return;
        }

        while (true)
        {
            Console.Clear();
            _view.MostrarMensagem("Escolha um mascote para interagir:");

            for (int i = 0; i < _mascotesAdotados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_mascotesAdotados[i].PokemonBase.Nome}");
            }
            Console.WriteLine("0. Voltar");

            int opcao = _view.LerOpcao(0, _mascotesAdotados.Count);
            if (opcao == 0) return;

            var mascote = _mascotesAdotados[opcao - 1];

            if (!mascote.Vivo)
            {
                _view.MostrarMensagem($"{mascote.PokemonBase.Nome} está morto e não pode interagir.");
                _view.EsperarEnter();
                continue;
            }

            await InteragirComMascote(mascote);
            break;
        }
    }


    public void RemoverMascote()
    {
        if (_mascotesAdotados.Count == 0)
        {
            _view.MostrarMensagem("Você não tem nenhum mascote para remover.");
            _view.EsperarEnter();
            return;
        }

        while (true)
        {
            Console.Clear();
            _view.MostrarMensagem("Escolha um mascote para remover:");

            for (int i = 0; i < _mascotesAdotados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_mascotesAdotados[i].PokemonBase.Nome}");
            }
            Console.WriteLine("0. Voltar");

            int opcao = _view.LerOpcao(0, _mascotesAdotados.Count);
            if (opcao == 0) return;

            var mascote = _mascotesAdotados[opcao - 1];

            Console.Clear();
            _view.MostrarMensagem($"Deseja realmente remover o mascote {mascote.PokemonBase.Nome}?");
            _view.MostrarMensagem("1 - Sim");
            _view.MostrarMensagem("2 - Não");

            int confirma = _view.LerOpcao(1, 2);
            if (confirma == 1)
            {
                _mascotesAdotados.RemoveAt(opcao - 1);
                _view.MostrarMensagem($"{mascote.PokemonBase.Nome} removido.");
                _view.EsperarEnter();
                return;
            }
        }
    }
}
