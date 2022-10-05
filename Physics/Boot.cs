//
//                  Boot.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
namespace Physics
{
    /// <summary>
    /// Classe de inicialização do programa em meio assíncrino.
    /// </summary>
    internal sealed class Boot
    {
        //  > Construtores da classe.
        #region Constructors

        #endregion
        //  ----------------------------- Fim da região
        //  > Variáveis da classe.
        #region Variables

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        #endregion
        //  ----------------------------- Fim da região
        //  > Funções de classe.
        //  Elementos sem retorno.
        #region Methods
        //  ----------------------------- Sub-regiões
        //  > Funções sem retorno assíncronas.
        #region Async

        /// <summary> Inicializa o programa em modo assíncrono. </summary>
        public static async Task Run()
        {
            try
            {                
                //  Conecta o MySQL e o Discord.
                Task bot = Global.Bot.Connect();
                await bot;

                //  Eventos de load do MySQL.
                Database.DataService.LoadEvents += Database.Guild.LoadFromDB;
                Database.DataService.LoadEvents += Database.TextChannel.LoadFromDB;
                Database.DataService.LoadEvents += Database.RU.LoadFromDB;
                await Database.DataService.LoadEvents();

                //  Executa os eventos temporais em tempo de execução.
                _ = Global.Timer.Execute();

                await Task.Delay(-1);
            }
            catch (Exception e)
            {
                Utilits.Log.WriteLine($"Falha global:\n\tHResult: {e.HResult},\n\tMessage: {e.Message},\n\tLink: {e.HelpLink}.", Utilits.ConsoleLog.MessageType.Error);
            }
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