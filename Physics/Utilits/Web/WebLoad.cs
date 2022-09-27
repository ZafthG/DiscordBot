//
//                  Web/WebLoad.cs
//
//      Por Gabriel Ferreira (ZafthG)
//
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
namespace Physics.Utilits
{
    partial class Web
    {
        /// <summary>
        /// Carrega uma página a partir de uma URL.
        /// </summary>
        /// <param name="url">URL da página que deve ser carregada.</param>
        public Web (string url)
        {
            HtmlWeb webRequest = new HtmlWeb();
            document = webRequest.Load(url);
        }
    }
}
//
//      Fim do código