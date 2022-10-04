//
//                  Database/RU/Recheck.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord;
using Discord.Commands;
namespace Physics.Database.Events
{
    /// <summary>
    /// Estrutura interna referente ao bot responsável por gerenciar a atualização de
    /// cardápio dos restaurantes universitários da UFPR.
    /// </summary>
    partial class RURecheck : Physics.Events.TimerEvent
    {
        /// <summary>
        /// Construtor para uma nova ação interna do bot de atualização do RU.
        /// </summary>
        public RURecheck() : base(new TimeSpan(0, Settings.Default_TimeUpdate, 0))
        {
            Utilits.Log.WriteLine($"Inicializando método de re-verificação . . .", Utilits.ConsoleLog.MessageType.Debug);
            EventsToRun += RunEvent;
        }

        /// <summary> Lista contendo os RUs para re-verificação. </summary>
        private RU? ruToCheck;

        /// <summary>
        /// Adiciona uma ação ao evento.
        /// </summary>
        /// <param name="targget">Alvo para a re-verificação.</param>
        public void AddAction (RU targget)
        {
            Global.Timer.TimerEvents += this.AwaitEvent;
            ruToCheck = targget;
        }

        /// <summary>
        /// Operador de execução e destrução do evento.
        /// </summary>
        private async Task RunEvent ()
        {
            if (ruToCheck == null)
            {
                Global.Timer.TimerEvents -= this.AwaitEvent;
                return;
            }

            Utilits.Log.WriteLine($"Executando re-verificação para o cardápio de um RU . . .", Utilits.ConsoleLog.MessageType.Debug);
            try
            {
                await ruToCheck.LoadMenuFromWeb();
                ruToCheck = null;
            }
            catch (Exception e)
            {
                Utilits.Log.WriteLine($"Falha na revalidação [RU]:\n\tHResult: {e.HResult},\n\tMessage: {e.Message},\n\tLink: {e.HelpLink}.", Utilits.ConsoleLog.MessageType.Error);
                throw;
            }

            if (ruToCheck == null)
                Global.Timer.TimerEvents -= this.AwaitEvent;
        }
    }
}
//
//      Fim do código