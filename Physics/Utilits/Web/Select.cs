//
//                  Web/Select.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
namespace Physics.Utilits
{
    /// <summary>
    /// Estrutura responsável por gerenciar dados provenientes da internet.
    /// </summary>
    partial class Web
    {
        /// <summary>
        /// Seleciona um conjunto de elementos com base em uma tag.
        /// </summary>
        /// <param name="tag">Tag para consulta.</param>
        /// <returns>Retorna os elementos selecionados</returns>
        protected IEnumerable<HtmlNode> Select (string tag)
        {
            return document.DocumentNode.QuerySelectorAll(tag);
        }

        /// <summary>
        /// Seleciona um conjunto de elementos com base em uma tag dentro de uma estrutura html.
        /// </summary>
        /// <param name="node">Nó html para consulta.</param>
        /// <param name="tag">Tag para consulta.</param>
        /// <returns>Retorna os elementos selecionados.</returns>
        protected IEnumerable<HtmlNode> SelectFrom (HtmlNode node, string tag)
        {
            return node.QuerySelectorAll(tag);
        }
    }
}
//
//      Fim do código