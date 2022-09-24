//
//                  Bot/Guild.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord.WebSocket;
namespace Physics.DiscordService
{
    /// <summary>
    /// Classe de inicialização do programa.
    /// </summary>
    partial class Bot
    {
        /// <summary>
        /// Adiciona um novo servidor a lista de servidores autorizados pelo bot.
        /// </summary>
        /// <param name="guild">Estrutura do serviço carregada.</param>
        public void AddGuild (Database.Guild guild) { Guilds.Add(guild.Id, guild); }
    }
}
//
//      Fim do código