//
//                  DataService.cs
//
//      Por Gabriel Ferreira (ZafthG)
using MySql.Data.MySqlClient;
namespace Physics.Database
{
    /// <summary>
    /// Delegate
    /// </summary>
    /// <returns></returns>
    public delegate Task LoadData();

    /// <summary>
    /// Classe responsável pelo gerenciamento da conexão MySQL.
    /// </summary>
    internal class DataService
    {
        //  ----------------------------- Fim da região
        //  > Variáveis da classe.
        #region Variables

        /// <summary> Conexão MySQL. </summary>
        private MySqlConnection connection = new ();

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        /// <summary> Conexão MySQL. </summary>
        public MySqlConnection Connection { get { return connection; } }

        #endregion
        //  ----------------------------- Fim da região

        /// <summary> Eventos que devem ser carregados. </summary>
        public static LoadData? LoadEvents = null;

        /// <summary>
        /// Abre uma conexão MySQL.
        /// </summary>
        public async Task Open()
        {
            int tries = 0;
            string connectString = $"server={Settings.MySQL_Server};port=3306;uid={Settings.MySQL_Uid};pwd={Settings.MySQL_Pwd};database={Settings.MySQL_Database};";

            while (tries < Settings.Services_MaxTries)
            {
                try
                {
                    connection = new MySqlConnection(connectString);
                    await connection.OpenAsync();

                    Utilits.Log.WriteLine("Conexão MySQL aberta com sucesso.", Utilits.ConsoleLog.MessageType.Debug);

                    break;
                }
                catch (MySqlException e)
                {
                    tries++;

                    Utilits.Log.WriteLine($"Falha ao tentar abrir a conexão MySQL:\n\t{e.Message}", Utilits.ConsoleLog.MessageType.Error);
                }
            }
        }

        /// <summary>
        /// Fecha a conexão MySQL.
        /// </summary>
        public async Task Close ()
        {
            if (connection.State != System.Data.ConnectionState.Open)
                return;

            await connection.CloseAsync();
        }
    }
}
//
//      Fim do código