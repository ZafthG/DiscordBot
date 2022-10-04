//
//                  TimerEvent.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
namespace Physics.Events
{
    /// <summary>
    /// Evento de execução no timer.
    /// </summary>
    internal class TimerEvent
    {
        /// <summary>
        /// Construtor para um novo evento de tempo.
        /// </summary>
        /// <param name="interval_toExecute">Intervalo de tempo em que o evento deve ser executado.</param>
        public TimerEvent(TimeSpan interval_toExecute)
        {
            TimeInterval = new TimeSpan();
            IntervalToExecute = interval_toExecute.TotalSeconds;
        }

        /// <summary> Intervalo de tempo para registro de análise. </summary>
        private TimeSpan TimeInterval;
        /// <summary> Intervalo de execução para o evento em segundos. </summary>
        private double IntervalToExecute;

        /// <summary> Delegate para execução interna de multiplos eventos. </summary>
        protected delegate Task InternalEventToRun();
        /// <summary> Conjunto de eventos que devem ser executados. </summary>
        protected InternalEventToRun? EventsToRun = null;

        /// <summary>
        /// Altera o intervalo de execução do evento.
        /// </summary>
        /// <param name="interval">Novo intervalo de execução.</param>
        protected void SetIntervalToExecute(TimeSpan interval) { IntervalToExecute = interval.TotalSeconds; }

        /// <summary> Reseta o contador de tempo. </summary>
        public void ResetTimer () { TimeInterval = TimeSpan.Zero; }
        /// <summary>
        /// Tarefa do evento que aguarda até o instante ao qual o evento deve ser chamado.
        /// </summary>
        public async Task AwaitEvent()
        {
            Utilits.Log.Write($"Chamando evento de espera >> ", Utilits.ConsoleLog.MessageType.Debug);
            if (EventsToRun == null)
                return;

            TimeInterval = new TimeSpan(TimeInterval.Hours, TimeInterval.Minutes, TimeInterval.Seconds + 1);
            Utilits.Log.WriteLine($"Incrementado TimeSpan ({TimeInterval.TotalSeconds} / {IntervalToExecute})", Utilits.ConsoleLog.MessageType.Debug);

            if (TimeInterval.TotalSeconds >= IntervalToExecute)
            {
                Utilits.Log.WriteLine($"Executando evento.", Utilits.ConsoleLog.MessageType.Debug);
                await EventsToRun();
                TimeInterval = TimeSpan.Zero;
            }
        }
    }
}
//
//      Fim do código