//
//                  Log.cs
//
//      Por Gabriel Ferreira (ZafthG)
namespace Physics.Utilits
{
    /// <summary>
    /// Classe responsável pela impressão de informações no console.
    /// </summary>
    internal class Log
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



        #endregion
        //  ----------------------------- Fim da sub-região
        //  > Funções sem retorno síncronas.
        #region Sync

        /// <summary> Escreve uma mensagem de uma linha no console. </summary>
        /// <param name="message"> Mensagem a ser impressa no console. </param>
        /// <param name="type">  </param>
        public static void WriteLine (string message, ConsoleLog.MessageType type)
        {
            Write($"{message}\n", type);
        }

        /// <summary> Escreve uma mensagem no console. </summary>
        /// <param name="message"> Mensagem a ser impressa no console. </param>
        /// <param name="type">  </param>
        public static void Write(string message, ConsoleLog.MessageType type)
        {
            if (type == ConsoleLog.MessageType.Debug && !Settings.Debug)
                return;

            Console.ForegroundColor = GetColor(type);
            Console.Write($"[{DateTime.Now.ToUniversalTime().AddHours(-3).ToString("yyyy/MM/dd HH:mm:ss ffff f")}][{type.ToString().ToUpper()}] {message}");
            Console.ForegroundColor = GetColor(ConsoleLog.MessageType.Default);
        }

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

        /// <summary> Adquire a cor para impressão baseada no tipo da
        /// mensagem.</summary>
        /// <param name="type">Tipo de mensagem específico.</param>
        /// <returns>Retorna a cor para o texto do console.</returns>
        private static ConsoleColor GetColor(ConsoleLog.MessageType type)
        {
            switch (type)
            {
                case ConsoleLog.MessageType.System: return ConsoleColor.DarkGreen;
                case ConsoleLog.MessageType.Bot: return ConsoleColor.Magenta;
                case ConsoleLog.MessageType.Receive: return ConsoleColor.DarkGray;
                case ConsoleLog.MessageType.Send: return ConsoleColor.DarkCyan;
                case ConsoleLog.MessageType.Database: return ConsoleColor.Cyan;
                case ConsoleLog.MessageType.Waring: return ConsoleColor.Yellow;
                case ConsoleLog.MessageType.Error: return ConsoleColor.Red;
                case ConsoleLog.MessageType.Debug: return ConsoleColor.Gray;
                default: return ConsoleColor.White;
            }
        }

        #endregion
        //  ----------------------------- Fim da sub-região
        #endregion
        //  ----------------------------- Fim da região
    }
}
//
//      Fim do código