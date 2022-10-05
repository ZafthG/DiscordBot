//
//                  Database/RU.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using MySql.Data.MySqlClient;
namespace Physics.Database
{
    /// <summary>
    /// Classe de inicialização do programa.
    /// </summary>
    internal class RU
    {
        //  > Construtores da classe.
        #region Constructors

        /// <summary>
        /// Construtor para o elemento RU.
        /// </summary>
        /// <param name="id">ID referente ao RU.</param>
        /// <param name="url">URL para consulta web.</param>
        public RU (ulong id, string tag, string textname, string url)
        {
            RURecheck = new ();
            this.id = id;
            this.tag = tag;
            this.textname = textname;
            this.url = url;
        }

        #endregion
        //  ----------------------------- Fim da região
        //  > Variáveis da classe.
        #region Variables

        /// <summary> ID referente ao RU. </summary>
        private ulong id;
        /// <summary> Tag única de identificação do RU. </summary>
        private string tag;
        /// <summary> Nome de exebição do RU. </summary>
        private string textname;
        /// <summary> URL de consulta para o RU. </summary>
        private string url;

        /// <summary> Contador específico para impedir um overloop do carregamento via Web. </summary>
        private int breakCounter = 0;

        /// <summary> Lista contendo os cardápios presentes neste RU vinculados a alguma data. </summary>
        private List<Menu>? menus;

        /// <summary> Evento responsável por re-verificar operações. </summary>
        private Events.RURecheck RURecheck;

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        /// <summary> Adquire o nome de exibição do RU. </summary>
        public string Name { get { return textname; } }
        /// <summary> [Versão provisória para um único menu] Pega o menu para impressão. </summary>
        public string? GetMenuPrint { get { return menus == null ? null : menus.ToArray()[0].ToString(); } }

        #endregion
        //  ----------------------------- Fim da região

        /// <summary>
        /// Carrega o cardápio do RU a partir do site.
        /// </summary>
        public Task LoadMenuFromWeb ()
        {
            menus = new ();

            try
            {
                Utilits.Log.WriteLine($"Carregado conteúdo do cardápio de [tag: {tag}] em [date: {DateTime.Now.ToUniversalTime().AddHours(-3).Date.ToString("dd/MM/yyyy")}] via 'http'.", Utilits.ConsoleLog.MessageType.System);
                menus.Add(new Menu(url, DateTime.Now.ToUniversalTime().AddHours(-3)));
            }
            catch (Exception e)
            {
                menus = null;
                if (e.Message.Contains("ERR#00h"))
                {
                    Utilits.Log.WriteLine($"Falha ao carregar um cardápio para [tag: {tag}, url: {url}]: Nenhum cardápio pode ser localizado para a data de hoje. Tentando novamente em cerca de 1 hora.", Utilits.ConsoleLog.MessageType.Waring);
                    if (breakCounter < Settings.Services_MaxTries)
                    {
                        RURecheck.AddAction(this);
                        breakCounter++;
                    }
                    else
                        breakCounter = 0;
                }
                else
                    Utilits.Log.WriteLine($"Erro ao carregar um cardápio para [tag: {tag}, url: {url}]: \n\t{e.Message}", Utilits.ConsoleLog.MessageType.Error);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Carrega o registro dos RUs presente no banco de dados.
        /// </summary>
        public static Task LoadFromDB ()
        {

            DataService data = new();
            try
            {
                data.Open().Wait();

                string commandString = $"select * from ru";
                MySqlCommand command = new MySqlCommand(commandString, data.Connection);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ulong id = reader.GetUInt64("id");
                    string tag = reader.GetString("tag");
                    string textname = reader.GetString("name");
                    string url = reader.GetString("url");

                    Global.Bot.ActionRU.Elements.Add(new RU(id, tag, textname, url));
                    Utilits.Log.WriteLine($"Carregado restaurante universitário [id={id}, tag={tag}, textname={textname}, url={url} a partir do banco de dados.", Utilits.ConsoleLog.MessageType.Database);
                }

                reader.Close();
            }
            catch (MySqlException e)
            {
                Utilits.Log.WriteLine($"[{e.Code}] Falha ao tentar carregar os objetos 'Restaurante Universitário' [RU] a partir do banco de dados: {e.Message}", Utilits.ConsoleLog.MessageType.Error);
            }
            catch (Exception e)
            {
                Utilits.Log.WriteLine($"Falha no carregamento de dados via DB [RU]:\n\tHResult: {e.HResult},\n\tMessage: {e.Message},\n\tLink: {e.HelpLink}.", Utilits.ConsoleLog.MessageType.Error);
            }
            finally
            {
                _ = data.Close();
            }

            return Task.CompletedTask;
        }
    }
}
//
//      Fim do código