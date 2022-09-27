//
//                  UseVerify.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
namespace Physics.Database.Events
{
    /// <summary>
    /// Evento que verifica se o MySQL está em execução ou não.
    /// </summary>
    internal class UseVerify : Physics.Events.TimerEvent
    {
        /// <summary>
        /// Construtor para o evento. Determina o Timeout para o MySQL como sendo aquele
        /// pré-configurado em Settings.
        /// </summary>
        public UseVerify() : base (new TimeSpan(0, Settings.MySQL_Timeout, 0))
        {
            EventsToRun += this.CloseMySQL;
            Global.Timer.TimerEvents += this.AwaitEvent;
        }

        /// <summary>
        /// Evento responsável por fechar a conexão MySQL.
        /// </summary>
        private async Task CloseMySQL () { await Global.DataClient.Close(); }

        /// <summary> Remove o evento da lista de execuções. </summary>
        public void Destroy () { EventsToRun -= this.CloseMySQL; Global.Timer.TimerEvents -= this.AwaitEvent; }
    }
}
//
//      Fim do código