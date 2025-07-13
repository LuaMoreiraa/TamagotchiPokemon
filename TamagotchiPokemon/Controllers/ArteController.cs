using System;
using System.IO;

public class ArteController
{
    private readonly string _pastaArtes;

    public ArteController(string pastaArtes)
    {
        if (string.IsNullOrEmpty(pastaArtes))
            throw new ArgumentException("O caminho da pasta de artes não pode tá vazio.", nameof(pastaArtes));

        _pastaArtes = pastaArtes;
    }

    public void MostrarArte(string estado)
    {
        if (string.IsNullOrWhiteSpace(estado))
        {
            Console.WriteLine("Não é possivel mostrar a arte");
            return;
        }

        string arquivo = Path.Combine(_pastaArtes, $"{estado}.txt");

        Console.Clear();

        if (File.Exists(arquivo))
        {
            try
            {
                string arte = File.ReadAllText(arquivo);
                Console.WriteLine(arte);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler a arte '{estado}': {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine($"Arte não foi encontrada para o estado: {estado}]");
            Console.WriteLine($"Procurado no: {arquivo}");
        }
    }
}
