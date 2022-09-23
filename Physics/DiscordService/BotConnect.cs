//
//                  BotConnect.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord;
namespace Physics.DiscordService
{
    partial class Bot
    {
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

        /// <summary> Abre a conexão com o serviço do Discord. </summary>
        public async Task Connect()
        {
            await socket.LoginAsync(TokenType.Bot, Settings.Discord_BotToken);
            await socket.StartAsync();
            await Task.Delay(5000);
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