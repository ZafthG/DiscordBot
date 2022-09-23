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
            socket = new ();
            socket.Log += Log;
        }

        /// <summary> Socket de conexão com o discord. </summary>
        private readonly DiscordSocketClient socket;
    }
}
//
//      Fim do código