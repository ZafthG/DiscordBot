//
//                  Bot/Actions/RU/Update.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord;
using Discord.Commands;
namespace Physics.DiscordService.Actions
{
    partial class RU
    {
        /// <summary>
        /// Verifica se o cardápio do RU deve ser atualizado.
        /// </summary>
        private async Task UpdateVerify ()
        {
            Utilits.Log.WriteLine($"Iniciando verificação para atualização dos cardápios . . .", Utilits.ConsoleLog.MessageType.Debug);
            try
            {
                if (DateTime.Now.ToUniversalTime().Ticks < nextUpdate.Ticks)
                    return;

                await Update();
            }
            catch (Exception e)
            {
                Utilits.Log.WriteLine($"Falha na instância de atualização [RU]:\n\tHResult: {e.HResult},\n\tMessage: {e.Message},\n\tLink: {e.HelpLink}.", Utilits.ConsoleLog.MessageType.Error);
            }
        }

        /// <summary>
        /// Faz a atualização do cardápio do RU.
        /// </summary>
        private async Task Update ()
        {
            Utilits.Log.WriteLine("Atualizando cardápio dos restaurantes universitários . . .", Utilits.ConsoleLog.MessageType.System);
            foreach (Database.RU ru in Elements)
            {
                await ru.LoadMenuFromWeb();
                Utilits.Log.WriteLine($"Enviando novo cardápio de {ru.Name} para os canais receptores.", Utilits.ConsoleLog.MessageType.Debug);
                if (ru.GetMenuPrint == null)
                    continue;
                await Global.Bot.TextChannels[$"TEST_TCH"].Send($"**{ru.Name.ToUpper()}**\n\n{ru.GetMenuPrint}");
            }

            if (DateTime.Now.ToUniversalTime().AddHours(-3).AddDays(1).Year == DateTime.Now.ToUniversalTime().AddHours(-3).Year)
                nextUpdate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.ToUniversalTime().AddHours(-3).AddDays(1).Day, Settings.Alert_StandardHour, 0, 0);
            else
                nextUpdate = new DateTime(DateTime.Now.AddYears(1).Year, 1, 1, Settings.Alert_StandardHour, 0, 0);
        }
    }
}
//
//      Fim do código