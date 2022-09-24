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
        /// Adiciona um novo canal a lista de canais de texto autorizados pelo bot.
        /// </summary>
        /// <param name="channel">Estrutura do canal que deve ser adicionado. </param>
        public void AddTextChannel (Database.TextChannel channel) { TextChannels.Add(channel.Tag, channel); }

        /// <summary>
        /// Verifica se um canal de texto é reconhecido pelo bot.
        /// </summary>
        /// <param name="channel_id">ID do canal de texto.</param>
        /// <returns>Retorna o resultado.</returns>
        private bool VerifyTextChannel (ulong channel_id)
        {
            foreach (Database.TextChannel channel in TextChannels.Values)
            {
                if (channel.Id == channel_id)
                    return true;
            }

            return false;
        }
    }
}
//
//      Fim do código