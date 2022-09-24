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

        /// <summary> Indica o tempo relativo ao qual a conexão está ausente. </summary>
        private TimeSpan timeFreeConnection = TimeSpan.Zero;
        /// <summary> Informa se está ou não conectado. </summary>
        private bool isConnected = false;
        /// <summary> Conexão MySQL. </summary>
        private MySqlConnection connection = new ();

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        /// <summary> Pega o tempo que a conexão MySQL está inativa. </summary>
        public TimeSpan GetTimeSleep { get { return timeFreeConnection; } }
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

                    timeFreeConnection = TimeSpan.Zero;
                    isConnected = true;

                    if (LoadEvents != null)
                        await LoadEvents();

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
            timeFreeConnection = TimeSpan.MaxValue;
            isConnected = false;
        }

        /// <summary>
        /// Desenvolve um laço infinito no qual a verifica a inatividade da conexão.
        /// </summary>
        public async Task UseVerify()
        {
            if (timeFreeConnection != TimeSpan.MaxValue)
            {
                DateTime Ago = DateTime.Now.ToUniversalTime();
                while (Ago.Minute >= DateTime.Now.ToUniversalTime().Minute) ;

                timeFreeConnection.Add(new TimeSpan(0, 0, 1));

                if (timeFreeConnection.Ticks >= new TimeSpan(0, Settings.MySQL_Timeout, 0).Ticks)
                    await Close();

                _ = UseVerify();
            }
        }

        /// <summary>
        /// Para evitar que a conexão seja fechada, ao chamar esse método ela é atualizada.
        /// </summary>
        public void UpdateConnection () { timeFreeConnection = TimeSpan.Zero; }
    }
}
//
//      Fim do código