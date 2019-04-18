using OpenQA.Selenium;

namespace SeleniumWebDriverTeste.Feature.TelaBuscaGoogle.POs
{
    public class BuscaGoogle
    {
        readonly IWebDriver _driver;
        public BuscaGoogle(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement CampoBusca { get { return _driver.FindElement(By.Name("q")); } }
        public IWebElement BotaoPesquisa { get { return _driver.FindElement(By.Name("btnK")); } }
        public IWebElement BotaoSorte { get { return _driver.FindElement(By.Name("btnI")); } }
        public IWebElement ResultadoPesquisa { get { return _driver.FindElement(By.Id("search")); } }        
    }
}
