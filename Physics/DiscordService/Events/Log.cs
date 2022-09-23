//
//                  Bot/Log.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord;
using Discord.Commands;
namespace Physics.DiscordService
{
    partial class Bot
    {
        /// <summary>
        /// Evento delegado para o tratamento de logs do bot.
        /// </summary>
        /// <param name="message">Mensagem a ser tratada.</param>
        private Task Log (LogMessage message)
        {
            if (message.Exception is CommandException cmdException)
            {
                Utilits.Log.WriteLine($"[Command/{message.Severity}] {cmdException.Command.Aliases[0]} failed to execute in {cmdException.Context.Channel}", Utilits.ConsoleLog.MessageType.Waring);
                Utilits.Log.WriteLine(cmdException.Message, Utilits.ConsoleLog.MessageType.Bot);
            }
            else
                Utilits.Log.WriteLine($"[General/{message.Severity}] {message.Message}", Utilits.ConsoleLog.MessageType.Bot);

            return Task.CompletedTask;
        }
    }
}
//
//      Fim do código