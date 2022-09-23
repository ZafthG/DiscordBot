//
//                  DataService.cs
//
//      Por Gabriel Ferreira (ZafthG)
using MySql.Data.MySqlClient;
namespace Physics.Database
{
    /// <summary>
    /// Classe responsável pelo gerenciamento da conexão MySQL.
    /// </summary>
    internal class DataService
    {
        //  > Construtores da classe.
        #region Constructors

        #endregion
        //  ----------------------------- Fim da região
        //  > Variáveis da classe.
        #region Variables

        /// <summary> Indica o tempo relativo ao qual a conexão está ausente. </summary>
        private TimeSpan timeFreeConnection = TimeSpan.Zero;

        /// <summary>  </summary>
        private MySqlConnection connection = new ();

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        /// <summary> Pega o tempo que a conexão MySQL está inativa. </summary>
        public TimeSpan GetTimeSleep { get { return timeFreeConnection; } }

        #endregion
        //  ----------------------------- Fim da região
        //  > Funções de classe.
        //  Elementos sem retorno.
        #region Methods
        //  ----------------------------- Sub-regiões
        //  > Funções sem retorno assíncronas.
        #region Async

        /// <summary> Abre uma conexão MySQL. </summary>
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
                    break;
                }
                catch (MySqlException e)
                {
                    tries++;

                    Utilits.Log.WriteLine($"Falha ao tentar abrir a conexão MySQL: {e.Message}", Utilits.ConsoleLog.MessageType.Error);
                }
            }
        }

        /// <summary> Fecha a conexão MySQL. </summary>
        public async Task Close ()
        {
            if (connection.State != System.Data.ConnectionState.Open)
                return;

            await connection.CloseAsync();
        }

        #endregion
        //  ----------------------------- Fim da sub-região
        //  > Funções sem retorno síncronas.
        #region Sync

        #endregion
        //  ----------------------------- Fim da sub-região
        #endregion
        //  ----------------------------- Fim da região
        //  > Funções de classe.
        //  Elementos com retorno.
        #region Functions
        //  ----------------------------- Sub-regiões
        //  > Funções sem retorno assíncronas.
        #region Async



        #endregion
        //  ----------------------------- Fim da sub-região
        //  > Funções sem retorno síncronas.
        #region Sync

        #endregion
        //  ----------------------------- Fim da sub-região
        #endregion
        //  ----------------------------- Fim da região
    }
}
//
//      Fim do código