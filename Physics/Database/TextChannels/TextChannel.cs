//
//                  Guild.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using System.Text;
using System.Security.Cryptography;
using Discord.WebSocket;
using Discord.Rest;
using MySql.Data.MySqlClient;
using System.Data.Common;
namespace Physics.Database
{
    /// <summary>
    /// Classe de inicialização do programa.
    /// </summary>
    internal class TextChannel
    {
        //  > Construtores da classe.
        #region Constructors

        public TextChannel(ulong id, ulong guild_id, string tag)
        {
            this.id = id;
            this.guild_id = guild_id;
            this.tag = tag;

            guild = Global.Bot.Socket.GetGuild(guild_id);
            channel = guild.GetChannel(id);
        }

        #endregion
        //  ----------------------------- Fim da região
        //  > Variáveis da classe.
        #region Variables

        private readonly ulong id;
        private readonly ulong guild_id;
        private readonly string tag;

        private readonly SocketGuild guild;
        private readonly SocketChannel channel;

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        public ulong Id { get { return id; } }
        public string Tag { get { return tag; } }

        #endregion
        //  ----------------------------- Fim da região

        /// <summary>
        /// Envia uma mensagem para este canal.
        /// </summary>
        /// <param name="message">Mensagem a ser enviada.</param>
        /// <returns>Retorna a mensagem que foi enviada.</returns>
        public async Task<RestUserMessage> Send (string message)
        {
            ISocketMessageChannel textChannel = (ISocketMessageChannel) channel;
            MD5 hash = MD5.Create();

            Utilits.Log.WriteLine($"Enviando mensagem para [id: {id}, guild_id: {guild_id}, tag: {tag}]: {Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(message)))}", Utilits.ConsoleLog.MessageType.Bot);
            return await textChannel.SendMessageAsync(message);
        }

        /// <summary>
        /// Carrega is servidores registrados no banco de dados.
        /// </summary>
        public static Task LoadFromDB()
        {

            foreach (Guild guild in Global.Bot.Guilds.Values)
            {
                DataService data = new();



                try
                {
                    data.Open().Wait();

                    string commandString = $"select id, tag from text_channels where guild_id = '{guild.Id}'";
                    MySqlCommand command = new MySqlCommand(commandString, data.Connection);

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ulong id = reader.GetUInt64("id");
                        string tag = reader.GetString("tag");

                        Global.Bot.AddTextChannel(new TextChannel(id, guild.Id, tag));
                        Utilits.Log.WriteLine($"Carregado canal referente à [guild_id: {guild.Id}], [id: {id}, tag: {tag}] a partir do banco de dados.", Utilits.ConsoleLog.MessageType.Database);
                    }

                    reader.Close();
                }
                catch (MySqlException e)
                {
                    Utilits.Log.WriteLine($"[{e.Code}] Falha ao tentar carregar os servidores permitidos a partir do banco de dados: {e.Message}", Utilits.ConsoleLog.MessageType.Error);
                }
                finally
                {
                    _ = data.Close();
                }
            }

            return Task.CompletedTask;
        }
    }
}
//
//      Fim do código