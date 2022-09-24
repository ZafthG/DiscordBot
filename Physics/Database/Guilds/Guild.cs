//
//                  Guild.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord.WebSocket;
using MySql.Data.MySqlClient;
using System.Data.Common;
namespace Physics.Database
{
    /// <summary>
    /// Classe de inicialização do programa.
    /// </summary>
    internal class Guild
    {
        //  > Construtores da classe.
        #region Constructors

        public Guild (ulong id, string tag)
        {
            this.id = id;
            this.tag = tag;

            guild = Global.Bot.Socket.GetGuild(id);
        }

        #endregion
        //  ----------------------------- Fim da região
        //  > Variáveis da classe.
        #region Variables

        private readonly ulong id;
        private readonly string tag;

        private readonly SocketGuild guild;

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        public ulong Id { get { return id; } }

        #endregion
        //  ----------------------------- Fim da região

        /// <summary>
        /// Carrega is servidores registrados no banco de dados.
        /// </summary>
        public static Task LoadGuilds ()
        {
            if (!Global.DataClient.IsConnected)
            {
                _ = Global.DataClient.Open();
                return Task.CompletedTask;
            }

            string commandString = "select * from guilds";
            MySqlCommand command = new MySqlCommand(commandString, Global.DataClient.Connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ulong id = reader.GetUInt64("id");
                    string tag = reader.GetString("tag");

                    Global.Bot.AddGuild(new Guild(id, tag));
                    Utilits.Log.WriteLine($"Carregado servidor [id: {id}, tag: {tag}] a partir do banco de dados.", Utilits.ConsoleLog.MessageType.Database);
                }

                reader.Close();
            }
            catch (MySqlException e)
            {
                Utilits.Log.WriteLine($"[{e.Code}] Falha ao tentar carregar os servidores permitidos a partir do banco de dados: {e.Message}", Utilits.ConsoleLog.MessageType.Error);
            }
            finally
            {
                Global.DataClient.UpdateConnection();
            }

            return Task.CompletedTask;
        }
    }
}
//
//      Fim do código