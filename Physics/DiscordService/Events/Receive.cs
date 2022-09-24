//
//                  Bot/Receive.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord;
using Discord.WebSocket;
namespace Physics.DiscordService
{
    partial class Bot
    {
        /// <summary>
        /// Delegate responsável pelo conjunto de eventos de análise para mensagens recebidas.
        /// </summary>
        /// <param name="message">Mensagem recebida.</param>
        public delegate Task ReceiveEvent(SocketMessage message);
        /// <summary>
        /// Conjunto de eventos que serão chamados ao receber uma mensagem.
        /// </summary>
        public ReceiveEvent? ReceiveEvents = null;


        /// <summary>
        /// Evento delegado para o tratamento de logs do bot.
        /// </summary>
        /// <param name="message">Mensagem a ser tratada.</param>
        private Task MessageReceive(SocketMessage message)
        {
            if (!VerifyTextChannel(message.Channel.Id)) return Task.CompletedTask;

            foreach (SocketUser user in message.MentionedUsers)
            {
                if (user.Id == Settings.Discord_BotId)
                {
                    if (message.Author.Id == Settings.Discord_BotId) return Task.CompletedTask;

                    _ = TextChannels["TEST_TCH"].Send(message.Content);
                    Utilits.Log.WriteLine($"Recebido de [id: {message.Channel.Id}]: {message.Content}", Utilits.ConsoleLog.MessageType.Bot);

                    if (ReceiveEvents == null) return Task.CompletedTask;
                    ReceiveEvents(message);
                }
            }
            return Task.CompletedTask;
        }
    }
}
//
//      Fim do código