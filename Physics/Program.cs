//
//                  Program.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
namespace Physics
{
    /// <summary>
    /// Classe de inicialização do programa.
    /// </summary>
    internal sealed class Program
    {
        /// <summary> Ponto de entrada do programa. Executa de maneira síncrona
        /// e da sequência para a inicialização assíncrona. </summary>
        private static void Main() => Boot.Run().Wait();
    }
}
//
//      Fim do código