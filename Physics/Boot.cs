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
            //  Conecta o MySQL e o Discord.
            Task data = Global.DataClient.Open();
            Task bot = Global.Bot.Connect();
            await bot; await data;

            //  Eventos de load do MySQL.
            Global.DataClient.LoadEvents += Database.Guild.LoadGuilds;
            Global.DataClient.LoadEvents += Database.TextChannel.LoadChannels;
            await Global.DataClient.LoadEvents();

            //  Executa os eventos temporais em tempo de execução.
            _ = Global.Timer.Execute();

            await Task.Delay(-1);
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