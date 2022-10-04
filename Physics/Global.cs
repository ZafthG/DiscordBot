//
//                  Global.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
namespace Physics
{
    /// <summary>
    /// Classe de configurações do programa.
    /// </summary>
    internal static class Global
    {
        /// <summary> Contador de tempo para eventos temporais. </summary>
        public static Events.Timer Timer = new();

        /// <summary> Estrutura do serviço de bot gerenciado pelo Discord. </summary>
        public static DiscordService.Bot Bot = new ();
    }
}
//
//      Fim do código