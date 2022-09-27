//
//                  Program.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
namespace Physics.Events
{
    /// <summary>
    /// Evento chamado na execução do Timer.
    /// </summary>
    internal delegate Task TimerEventDelegate();

    /// <summary>
    /// Executa um conjunto de eventos a cada 1 segundo.
    /// </summary>
    internal class Timer
    {
        /// <summary>
        /// Conjunto de eventos que devem ser executados na progressão de 1 segundo.
        /// </summary>
        public TimerEventDelegate? TimerEvents = null;

        /// <summary>
        /// Executa o timer, rodando os eventos 
        /// </summary>
        public async Task Execute ()
        {
            DateTime ago = DateTime.Now.ToUniversalTime();
            while (true)
            {
                long ticks = DateTime.Now.ToUniversalTime().Ticks - ago.Ticks;
                if (ticks >= new TimeSpan(0, 0, 1).Ticks) break;
            }

            if (TimerEvents != null)
                await TimerEvents();

            _ = Execute();
        }
    }
}
//
//      Fim do código