using BoDi;
using OpenQA.Selenium;
using SeleniumWebDriverTeste.Feature.TelaBuscaGoogle.POs;
using SeleniumWebDriverTeste.Hooks.Steps;
using TechTalk.SpecFlow;

namespace SeleniumWebDriverTeste.Feature.TelaBuscaGoogle.Steps
{
    [Binding]
    public class BuscaGoogleSteps : HooksSteps
    {
        private readonly BuscaGoogle POBuscaGoogle;
        public BuscaGoogleSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext) : base(objectContainer, scenarioContext)
        {
            POBuscaGoogle = new BuscaGoogle(_elementUtils.RecuperarInstanciaDriver());
        }
        
        [Given(@"que eu acesso o site do Google")]
        public void DadoQueEuAcessoOSiteDoGoogle()
        {
            _elementUtils.DirecionarAmbiente("");
        }

        [Given(@"informo ""(.*)"" no campo de busca")]
        public void DadoInformoNoCampoDeBusca(string p0)
        {
            _elementUtils.PreencherCampo(POBuscaGoogle.CampoBusca, p0);
        }

        [Given(@"clico no botao pesquisar")]
        public void DadoClicoNoBotaoPesquisar()
        {
            _elementUtils.Clicar(POBuscaGoogle.BotaoPesquisa);
        }

        [Given(@"clico no botao estou com sorte")]
        public void DadoClicoNoBotaoEstouComSorte()
        {
            _elementUtils.Clicar(POBuscaGoogle.BotaoSorte);
        }

        [Then(@"o Google retorna as informações correspondentes a ""(.*)""")]
        public void EntaoOGoogleRetornaAsInformacoesCorrespondentesA(string p0)
        {
            _elementUtils.CompararTextoParcial(p0, POBuscaGoogle.ResultadoPesquisa);
        }

        [Then(@"sou redirecionado para o site Seleniumhq\.org")]
        public void EntaoSouRedirecionadoParaOSiteSeleniumhq_Org()
        {
            _elementUtils.CompararTexto("https://www.seleniumhq.org/projects/webdriver/", _elementUtils.RecuperarInstanciaDriver().Url.ToString());
        }

    }
}
