using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using BoDi;
using Allure.Commons;
using SeleniumWebDriverTeste.Utils;
using System.Web.Configuration;

namespace SeleniumWebDriverTeste.Hooks.Steps
{
    [Binding]
    public class HooksSteps
    {

        public static IObjectContainer _objectContainer;
        public static ScenarioContext _scenarioContext;
        public static AllureLifecycle _allureLifecycle;
        public static ElementUtils _elementUtils;
        #region Configuracao

        public HooksSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _allureLifecycle = AllureLifecycle.Instance;

        }

        [BeforeFeature]
        [OneTimeSetUp]
        public static void IniciarTestes()
        {

        }

        [BeforeScenario]
        public static void IniciarCenario()
        {
            _elementUtils = new ElementUtils();
        }

        [AfterScenario]
        public static void FinalizarTeste()
        {
            if (_scenarioContext != null && _scenarioContext.TestError != null)
            {
                var path = _elementUtils.TirarPrint();
                _allureLifecycle.AddAttachment(path);
            }
            AllureHackForScenarioOutlineTests();
            _elementUtils.Finalizar();
        }

        private static void AllureHackForScenarioOutlineTests()
        {
            _scenarioContext.TryGetValue(out TestResult testresult);
            _allureLifecycle.UpdateTestCase(testresult.uuid, tc =>
            {
                tc.name = _scenarioContext.ScenarioInfo.Title;
                tc.historyId = Guid.NewGuid().ToString();
            });
        }
        #endregion


        #region Descricao dos Passos

        [Given(@"que utilizo o projeto X")]
        public void DadoQueUtilizoOProjetoX()
        {
            Console.WriteLine("Testes iniciados");
        }

        [Given(@"escrevo testes utilizando a prática BDD")]
        public void DadoEscrevoTestesUtilizandoAPraticaBDD()
        {
            Console.WriteLine("com prática de BDD");
        }

        [When(@"instancio o Selenium Driver do Projeto")]
        public void QuandoInstancioOSeleniumDriverDoProjeto()
        {
            Console.WriteLine("utilizando Selenium WebDriver");
        }

        [Then(@"desejo abrir um Navegador para reproduzir passos de meus testes")]
        public void EntaoDesejoAbrirUmNavegadorParaReproduzirPassosDeMeusTestes()
        {
            string Browser = WebConfigurationManager.AppSettings["Browser"];
            Console.WriteLine("e o navegador: " + Browser);
        }
        #endregion
    }
}
