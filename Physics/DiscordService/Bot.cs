//
//                  Program.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord;
using Discord.WebSocket;
namespace Physics.DiscordService
{
    /// <summary>
    /// Série de classes parciais de estruturação do bot.
    /// </summary>
    partial class Bot
    {
        /// <summary>
        /// Inicializa a estrutura do bot.
        /// </summary>
        public Bot ()
        {
            //  Listas
            Guilds = new ();
            TextChannels = new ();

            //  Ações
            ActionRU = new ();

            //  Funcionamento
            socket = new ();

            socket.Log += Log;
            socket.MessageReceived += MessageReceive;
        }

        /// <summary> Socket de conexão com o discord. </summary>
        private readonly DiscordSocketClient socket;

        /// <summary> Conjunto de guilds do discord aos quais o bot tem acesso registrado. </summary>
        public readonly Dictionary<ulong, Database.Guild> Guilds;
        /// <summary> Conjunto de canais de texto reconhecidos pelo bot. </summary>
        public readonly Dictionary<string, Database.TextChannel> TextChannels;

        /// <summary> Ação interna do bot referente a atualização do cardápio do RU. </summary>
        public Actions.RU ActionRU;

        /// <summary> Pega a socket de conexão para uso. </summary>
        public DiscordSocketClient Socket { get { return socket; } }
    }
}
//
//      Fim do código