//
//                  Database/Menu.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using HtmlAgilityPack;
namespace Physics.Database
{
    /// <summary>
    /// Cardápio referente a um dia e a um RU.
    /// </summary>
    internal class Menu : Utilits.Web
    {
        //  > Construtores da classe.
        #region Constructors

        /// <summary>
        /// Instância um novo objeto de cardápio.
        /// </summary>
        /// <param name="url">Link para consulta no site do RU.</param>
        /// <param name="reference">Data de referência para a qual o cardápio pertence.</param>
        public Menu (string url, DateTime reference) : base (url)
        {
            breakfast = new ();
            lunch = new();
            dinner = new();

            date = reference;

            try { LoadFromWeb().Wait(); } catch { throw; }
        }

        #endregion
        //  ----------------------------- Fim da região
        //  > Variáveis da classe.
        #region Variables

        /// <summary> Data para a qual o cardápio é válido. </summary>
        private readonly DateTime date;

        /// <summary> Comidas presentes no café da manhã do cardápio. </summary>
        private readonly List<Food> breakfast;
        /// <summary> Comidas presentes no almoço do cardápio. </summary>
        private readonly List<Food> lunch;
        /// <summary> Comidas presentes no jantar do cardápio. </summary>
        private readonly List<Food> dinner;

        #endregion
        //  ----------------------------- Fim da região
        //  > Propriedades da classe.
        #region Proprieties

        #endregion
        //  ----------------------------- Fim da região

        /// <summary>
        /// Carrega o menu a partir da internet.
        /// </summary>
        public async Task LoadFromWeb ()
        {
            int index = await GetIndexFromDate(Select("#post div p").ToArray());
            HtmlNode[] tables = Select("#post div table").ToArray();

            Utilits.Log.WriteLine($"Index recebido: {index}", Utilits.ConsoleLog.MessageType.Debug);

            if (index < 0)
                throw new Exception("ERR#00h");

            HtmlNode[] lines = SelectFrom(tables[index], "tbody tr").ToArray();

            for (int i = 0; i < lines.Length; i += 2)
            {
                //  Remove o lixo do texto.
                string cleanText = lines[i].InnerHtml
                    .Replace("<td>", "")
                    .Replace("</td>", "")
                    .Replace("<strong>", "")
                    .Replace("</strong>", "")
                    .Trim()
                    .ToUpper()
                    .Replace(" ", "_")
                    .Replace("Á", "A")
                    .Replace("É", "E")
                    .Replace("Ã", "A")
                    .Replace("Ç", "C");

                int n = i + 1;
                string content = Regex.Replace(lines[n].InnerHtml, @"<td([^>]*)>", "");
                content = Regex.Replace(content, @"<\/td([^>]*)>", "");
                content = Regex.Replace(content, @"<strong([^>]*)>", "");
                content = Regex.Replace(content, @"<\/strong([^>]*)>", "");
                content = Regex.Replace(content, @"<img([^>]*[^'\/])>", "");
                content = Regex.Replace(content, @"<a([^>]*)>", "");
                content = Regex.Replace(content, @"<\/a([^>]*)>", "");
                content = Regex.Replace(content, @"<br>", "\n");
                content = content.Replace("c/", "com");

                string[] foods = content.Split('\n');
                if (cleanText == "CAFE_DA_MANHA")
                    await ReplaceFoods(breakfast, foods);
                else if (cleanText == "ALMOCO")
                    await ReplaceFoods(lunch, foods);
                else if (cleanText == "JANTAR")
                    await ReplaceFoods(dinner, foods);
                else
                    throw new Exception($"Falha ao carregar um cardápio, o elemento '{cleanText}' é desconhecido no contexto.");
            }
        }

        /// <summary>
        /// Pega as comidas recebidas via carregamento web, carrega a comida a partir do banco de dados, ou cria o registro dela
        /// e, por fim, aloca essa comida para o cardápio no turno destinado.
        /// </summary>
        /// <param name="list">Lista para adição das comidas.</param>
        /// <param name="foods">Comidas a serem tratadas e selecionadas.</param>
        /// <returns></returns>
        private Task ReplaceFoods(List<Food> list, string[] foods)
        {
            foreach(string content in foods)
            {
                string foodname = content.Trim().Replace(" ", ".");
                foodname = Regex.Replace(foodname, @"\.{2,}", "");
                foodname = foodname.Replace(".", " ");

                if (string.IsNullOrEmpty(foodname) || string.IsNullOrWhiteSpace(foodname))
                    continue;

                list.Add(new Food(foodname));
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Pega o index de consulta relacionado a uma data de referência.
        /// </summary>
        /// <param name="nodes">Nós da página html para consulta.</param>
        /// <returns>Retorna um par chave-valor de data e </returns>
        public Task<int> GetIndexFromDate(HtmlNode[] nodesRef)
        {
            List<HtmlNode> nodesList = new ();
            for (int i = 0; i < nodesRef.Length; i++)
            {
                if (string.IsNullOrEmpty(nodesRef[i].InnerHtml) || string.IsNullOrWhiteSpace(nodesRef[i].InnerHtml))
                    continue;

                nodesList.Add(nodesRef[i]);
            }

            HtmlNode[] nodes = nodesList.ToArray();
            for (int i = 0; i < nodes.Length; i++)
            {
                Utilits.Log.WriteLine($"[{i}] Analizando. . .: {nodes[i].InnerHtml}", Utilits.ConsoleLog.MessageType.Debug);
                Match match = Regex.Match(nodes[i].InnerHtml, @"([0-9][0-9]\/[0-9][0-9]\/[0-9][0-9][0-9][0-9])");
                if (!match.Success)
                {
                    match = Regex.Match(nodes[i].InnerHtml, @"([0-9][0-9]\/[0-9][0-9]\/[0-9][0-9])");
                    if (!match.Success)
                        continue;
                }

                string[] resultString = match.Value.Split('/');

                DateTime yearFix = DateTime.Parse($"01/01/{Convert.ToInt32(resultString[2])}");
                DateTime result = new DateTime(yearFix.Year, Convert.ToInt32(resultString[1]), Convert.ToInt32(resultString[0]));
                Utilits.Log.WriteLine($"Carregando cardápio RU >> Data: {result.Date.ToString("dd/MM/yyyy")}, Index: {i} == Comp: {date.ToString("dd/MM/yyyy")}", Utilits.ConsoleLog.MessageType.Debug);
                if (result.Date.ToString("dd/MM/yyyy") == date.Date.ToString("dd/MM/yyyy"))
                    return Task.FromResult(i);
            }

            return Task.FromResult(-1);
        }

        /// <summary>
        /// Obtem o menu em formato de impressão.
        /// </summary>
        public override string ToString()
        {
            StringBuilder result = new ();
            CultureInfo pt_br = new CultureInfo("pt-BR");

            result.Append($"Bom dia para todos! O cardápio de hoje (**{DateTime.Now.ToString("dd/MM/yyyy - dddd", pt_br).ToUpper()}**) é:\n");
            result.Append("\n**CAFÉ DA MANHÃ**\n");
            result.Append(Print(breakfast));
            result.Append("\n**ALMOÇO**\n");
            result.Append(Print(lunch));
            result.Append("\n**JANTAR**\n");
            result.Append(Print(dinner));

            return result.ToString();
        }

        /// <summary>
        /// Prepara a impressão do cardápio de maneira definitiva.
        /// </summary>
        /// <param name="list">Lista para se basear.</param>
        /// <returns>Retorna o texto organizado.</returns>
        private string Print (List<Food> list)
        {
            StringBuilder result = new ();
            StringBuilder sectionMain = new ();
            StringBuilder? sectionDessert = null;
            StringBuilder? sectionVegan = null;
            foreach (Food food in list)
            {
                //  Se uma das comidas não tiver sido configurada no banco de dados, faz a impressão simples.
                if (food.Components == null)
                    return PrintStandard(list);

                StringBuilder maker = new ();
                maker.Append($"- {food.Name}");

                if (food.Components.Contains(Enum.FoodComponents.Vegan))
                    maker.Append(" 🍃");
                if (food.Components.Contains(Enum.FoodComponents.HasMilk))
                    maker.Append(" 🥛");
                if (food.Components.Contains(Enum.FoodComponents.HaveAllergicProducts))
                    maker.Append(" ☠️");
                if (food.Components.Contains(Enum.FoodComponents.HaveGluten))
                    maker.Append(" 🍞");
                if (food.Components.Contains(Enum.FoodComponents.HaveEggs))
                    maker.Append(" 🥚");
                if (food.Components.Contains(Enum.FoodComponents.AnimalOrigin))
                    maker.Append(" 🍖");
                if (food.Components.Contains(Enum.FoodComponents.HaveHoney))
                    maker.Append(" 🍯");
                if (food.Components.Contains(Enum.FoodComponents.HavePepper))
                    maker.Append(" 🌶");

                if (food.Components.Contains(Enum.FoodComponents.MainCourse) && !food.Components.Contains(Enum.FoodComponents.Vegan))
                    sectionMain.Append($"\t{maker}\n");
                else if (food.Components.Contains(Enum.FoodComponents.MainCourse) && food.Components.Contains(Enum.FoodComponents.Vegan))
                {
                    if (sectionVegan == null)
                        sectionVegan = new ();
                    sectionVegan.Append($"\t\t{maker}\n");
                }
                else if (food.Components.Contains(Enum.FoodComponents.Garnish))
                    sectionMain.Append($"\t{maker}\n");
                else if (food.Components.Contains(Enum.FoodComponents.Salad))
                    sectionMain.Append($"\t{maker}\n");
                else if (food.Components.Contains(Enum.FoodComponents.Dessert))
                {
                    if (sectionDessert == null)
                        sectionDessert = new();
                    sectionDessert.Append($"\t\t{maker}\n");
                }
            }

            result.Append(sectionMain);
            if (sectionVegan != null)
            {
                result.Append("\n\t**Opção vegana:**\n");
                result.Append(sectionVegan);
            }
            if (sectionDessert != null)
            {
                result.Append("\n\t**Sobremesa:**\n");
                result.Append(sectionDessert);
            }

            return result.ToString();
        }

        /// <summary>
        /// Prepara a impressão do cardápio de maneira simples e padrão para caso um dos elementos não tenha sido corretamente configurado.
        /// </summary>
        /// <param name="list">Lista de elementos para se basear.</param>
        /// <returns>Retorna o texto organizado.</returns>
        private string PrintStandard (List<Food> list)
        {
            StringBuilder result = new();
            foreach (Food food in list)
                result.Append($"\t - {food.Name}\n");
            return result.ToString();
        }
    }
}
//
//      Fim do código