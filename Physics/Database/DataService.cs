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

        /// <summary> Informa se está ou não conectado. </summary>
        private bool isConnected = false;
        /// <summary> Conexão MySQL. </summary>
        private MySqlConnection connection = new ();
        /// <summary> Evento responsável por verificar o uso da conexão MySQL. </summary>
        private Events.UseVerify? UseVerifyEvent = null;

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        /// <summary> Informa se está ou não conectado ao MySQL. </summary>
        public bool IsConnected { get { return isConnected; } }
        /// <summary> Conexão MySQL. </summary>
        public MySqlConnection Connection { get { return connection; } }

        #endregion
        //  ----------------------------- Fim da região

        /// <summary>  </summary>
        public LoadData? LoadEvents = null;

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

                    Utilits.Log.WriteLine("Conexão MySQL aberta com sucesso.", Utilits.ConsoleLog.MessageType.Database);

                    isConnected = true;
                    if (LoadEvents != null) await LoadEvents();
                    if (UseVerifyEvent == null) UseVerifyEvent = new ();

                    break;
                }
                catch (MySqlException e)
                {
                    tries++;

                    Utilits.Log.WriteLine($"Falha ao tentar abrir a conexão MySQL: {e.Message}", Utilits.ConsoleLog.MessageType.Error);
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
            isConnected = false;

            if (UseVerifyEvent == null)
                return;

            UseVerifyEvent.Destroy();
            UseVerifyEvent = null;
        }

        /// <summary>
        /// Para evitar que a conexão seja fechada, ao chamar esse método ela é atualizada.
        /// </summary>
        public void UpdateConnection () { if (UseVerifyEvent != null) UseVerifyEvent.ResetTimer(); }
    }
}
//
//      Fim do código