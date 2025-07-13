using System;
using System.Collections.Generic;

namespace TamagotchiPokemon.Views
{
    public class TamagotchiView
    {
        public void MostrarMenuPokemons(List<string> nomesPokemons)
        {
            Console.WriteLine("\nEscolha um Pokémon para adotar:");
            for (int i = 0; i < nomesPokemons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {nomesPokemons[i]}");
            }
            Console.WriteLine("0. Sair");
        }

        public void MostrarMenuInteracoes()
        {
            Console.WriteLine("\nO que deseja fazer com seu Pokémon?");
            Console.WriteLine("1. Alimentar");
            Console.WriteLine("2. Brincar");
            Console.WriteLine("3. Colocar para dormir");
            Console.WriteLine("4. Ver status");
            Console.WriteLine("5. Banhar");
            Console.WriteLine("6. Medicar");
            Console.WriteLine("0. Voltar");
        }

        public void MostrarMenuEstados(List<string> estados)
        {
            Console.WriteLine("\nEscolha um estado para ver a arte:");
            for (int i = 0; i < estados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {estados[i].Replace('_', ' ')}");
            }
            Console.WriteLine("0. Voltar");
        }

        public void MostrarArte(string asciiArt)
        {
            Console.Clear();
            Console.WriteLine(asciiArt);
        }

        public void MostrarStatus(string nome, int fome, int energia, int felicidade, int banho, int saude, bool vivo)
        {
            Console.WriteLine($"--- Status de {nome} ---");
            Console.WriteLine($"Fome:       {fome}/100");
            Console.WriteLine($"Energia:    {energia}/100");
            Console.WriteLine($"Felicidade: {felicidade}/100");
            Console.WriteLine($"Banho:    {banho}/100");
            Console.WriteLine($"Saúde:      {saude}/100");
            Console.WriteLine($"Status:     {(vivo ? "Vivo" : "Morto")}");
        }



        public void MostrarMensagem(string mensagem)
        {
            Console.WriteLine(mensagem);
        }

        public int LerOpcao(int min, int max)
        {
            while (true)
            {
                Console.Write("Opção: ");
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out int opcao))
                {
                    if (opcao >= min && opcao <= max)
                    {
                        return opcao;
                    }
                }
                Console.WriteLine($"Por favor, insira um número entre {min} e {max}.");
            }
        }

        public void EsperarEnter()
        {
            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }

        public void MostrarMensagemFimDoJogo(string mensagem)
        {
            Console.WriteLine(mensagem);
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}
