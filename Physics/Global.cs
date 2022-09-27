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
        /// <summary> Estrutura de serviço para a conexão ao MySQL. </summary>
        public static Database.DataService DataClient = new ();
        /// <summary> Estrutura do serviço de bot gerenciado pelo Discord. </summary>
        public static DiscordService.Bot Bot = new ();
        /// <summary> Contador de tempo para eventos temporais. </summary>
        public static Events.Timer Timer = new ();
    }
}
//
//      Fim do código