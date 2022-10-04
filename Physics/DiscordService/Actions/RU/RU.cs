//
//                  Bot/Actions/RU/RU.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord;
using Discord.Commands;
namespace Physics.DiscordService.Actions
{
    /// <summary>
    /// Estrutura interna referente ao bot responsável por gerenciar a atualização de
    /// cardápio dos restaurantes universitários da UFPR.
    /// </summary>
    partial class RU : Events.TimerEvent
    {
        /// <summary>
        /// Construtor para uma nova ação interna do bot de atualização do RU.
        /// </summary>
        public RU () : base (new TimeSpan(0, Settings.Default_TimeUpdate, 0))
        {
            Elements = new();

            nextUpdate = DateTime.MinValue;

            EventsToRun += this.UpdateVerify;
            Global.Timer.TimerEvents += this.AwaitEvent;
        }

        /// <summary> Registra o momento da última atualização registrada. </summary>
        private DateTime nextUpdate;

        /// <summary> Lista contendo os restaurantes universitários para processamento. </summary>
        public List<Database.RU> Elements;
    }
}
//
//      Fim do código