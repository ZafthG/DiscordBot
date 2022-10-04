//
//                  Database/Food.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using Discord;
using MySql.Data.MySqlClient;
namespace Physics.Database
{
    /// <summary>
    /// Classe de inicialização do programa.
    /// </summary>
    internal class Food
    {
        //  > Construtores da classe.
        #region Constructors

        /// <summary>
        /// Inicializa uma nova comida.
        /// </summary>
        /// <param name="name">Nome da comida para consulta no banco de dados.</param>
        public Food (string name)
        {
            this.id = 0;
            this.name = name;
            this.nametext = null;
            this.foodComponents = null;

            Utilits.Log.WriteLine($"Processando comida: {name}", Utilits.ConsoleLog.MessageType.Debug);
            DataService data = new();

            if (!LoadFromDB())
            {
                data.Open().Wait();
                try
                {
                    string commandString = $"insert into foods (name) values ('{name}')";
                    MySqlCommand command = new MySqlCommand(commandString, data.Connection);

                    if (command.ExecuteNonQuery() > 0)
                        Utilits.Log.WriteLine($"Adicionado ao banco de dados a comida (name: {name}) com sucesso.", Utilits.ConsoleLog.MessageType.Database);
                    if (!LoadFromDB())
                        Utilits.Log.WriteLine($"Erro ao tentar recarregar Food::(name: {name}) inserido no banco de dados.", Utilits.ConsoleLog.MessageType.Error);
                }
                catch (MySqlException error)
                {
                    Utilits.Log.WriteLine($"Falha ao tentar executar uma operação MySQL em '{nameof(Food)}':\n\t[{error.Code}] {error.Message}", Utilits.ConsoleLog.MessageType.Error);
                }
                finally
                {
                    _ = data.Close();
                }
            }
        }

        #endregion
        //  ----------------------------- Fim da região
        //  > Variáveis da classe.
        #region Variables

        /// <summary> ID da comida presente no banco de dados. </summary>
        private ulong id;
        /// <summary> Nome da comida baseada na tabela do RU. </summary>
        private string name;
        /// <summary> Cadeia de texto para o nome da comida para a exibição. </summary>
        private string? nametext;
        /// <summary> Lista os componentes e as características da comida. </summary>
        private List<Enum.FoodComponents>? foodComponents;

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        /// <summary> Adquire o nome da comida. </summary>
        public string Name { get { return nametext == null ? name : nametext; } }
        /// <summary> Adquire o conjunto de componentes de descrição e categorização da comida. </summary>
        public List<Enum.FoodComponents>? Components { get { return foodComponents;  } }

        #endregion
        //  ----------------------------- Fim da região
        //  > Funções de classe.

        /// <summary>
        /// Carrega uma comida a partir do banco de dados.
        /// </summary>
        public bool LoadFromDB()
        {
            DataService data = new();
            try
            {
                data.Open().Wait();
                string commandString = $"select * from foods where name = '{name}'";
                MySqlCommand command = new MySqlCommand(commandString, data.Connection);
                Utilits.Log.WriteLine($"Executando SQL Command: {commandString} in {command.Connection.ConnectionString}", Utilits.ConsoleLog.MessageType.Debug);

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id = reader.GetUInt64("id");
                    if (!reader.IsDBNull(2))
                        nametext = reader.GetString("nametext");

                    if (!reader.IsDBNull(3))
                    {
                        string[] components = reader.GetString("components").Split(';');
                        for(int i = 0; i < components.Length; i++)
                        { 
                            if (foodComponents == null)
                                foodComponents = new();

                            foodComponents.Add((Enum.FoodComponents)Convert.ToInt32(components[i]));
                        }
                    }

                    Utilits.Log.WriteLine($"Carregada comida [id: {id}, name: {name}] a partir do banco de dados.", Utilits.ConsoleLog.MessageType.Database);

                    reader.Close();
                    return true;
                }

                reader.Close();
            }
            catch (MySqlException error)
            {
                Utilits.Log.WriteLine($"Falha ao tentar executar uma operação MySQL em '{nameof(Food)}':\n\t[{error.Code}] {error.Message}", Utilits.ConsoleLog.MessageType.Error);
            }
            finally
            {
                _ = data.Close();
            }

            return false;
        }
        
    }
}
//
//      Fim do código