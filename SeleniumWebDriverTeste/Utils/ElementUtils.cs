using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Web.Configuration;
using System.Linq;
using System.Reflection;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SeleniumWebDriverTeste.Utils
{
    public class ElementUtils
    {
        private IWebDriver driver;
        private string browser;
        private string ambiente;
        private string nomeProjeto;
        public ElementUtils()
        {
            browser = WebConfigurationManager.AppSettings["Browser"];
            nomeProjeto = WebConfigurationManager.AppSettings["nomeProjeto"];
            ambiente = WebConfigurationManager.AppSettings["Ambiente"];

            InicializarDriver();
        }
        private void InicializarDriver()
        {
            Console.WriteLine("Inicializando Driver...");

            if (browser.Equals("chrome"))
            {
                var Options = new ChromeOptions();
                Options.AddArguments("--disable-extensions");
                Options.AddArguments("--start-maximized");

                var Servico = ChromeDriverService.CreateDefaultService();

                driver = new ChromeDriver(Servico, Options, TimeSpan.FromMinutes(3));

            }
            else
            {
                //Inserir outros browsers
            }
        }

        public IWebDriver RecuperarInstanciaDriver()
        {
            return driver;
        }

        public void DirecionarAmbiente(string caminhoMenu)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl(ambiente + "/" + caminhoMenu);
            driver.Manage().Window.Maximize();
        }

        public void Finalizar()
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                }

                var chromeDriverProcesses = Process.GetProcesses().Where(pr => pr.ProcessName == "chromedriver");

                if (chromeDriverProcesses.Count() != 0)
                {
                    foreach (var process in chromeDriverProcesses)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            Console.WriteLine("Driver finalizado.");
        }

        public void PreencherCampo(IWebElement campo, String texto, int segundos = 30)
        {

            try
            {
                campo.SendKeys(texto);
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public void Clicar(IWebElement elemento, int segundos = 30)
        {
            try
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(segundos);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(segundos);

                elemento.Click();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CompararTexto(string vlEsperado, IWebElement elemento, bool falha = true)
        {
            var vlEncontrado = elemento.Text;
            if (falha)
            {
                Assert.AreEqual(vlEsperado, vlEncontrado);
            }
            else
            {
                try
                {
                    Assert.AreEqual(vlEsperado, vlEncontrado);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor Esperado: " + vlEsperado + Environment.NewLine +
                                      "Valor Encontrado: " + vlEncontrado + Environment.NewLine +
                                      "Erro: " + e);
                }
            }
        }
        public void CompararTexto(string vlEsperado, string vlEncontrado, bool falha = true)
        {
            if (falha)
            {
                Assert.AreEqual(vlEsperado, vlEncontrado);
            }
            else
            {
                try
                {
                    Assert.AreEqual(vlEsperado, vlEncontrado);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Valor Esperado: " + vlEsperado + Environment.NewLine +
                                      "Valor Encontrado: " + vlEncontrado + Environment.NewLine +
                                      "Erro: " + e);
                }
            }
        }
        public void CompararTextoParcial(string vlEsperado, IWebElement elemento)
        {
            var vlEncontrado = elemento.Text;
            
            if (vlEncontrado.Contains(vlEsperado))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Valor esperado não existe no valor encontrado");
            }            
        }
        public void CompararTextoParcial(string vlEsperado, string vlEncontrado)
        {
            if (vlEncontrado.Contains(vlEsperado))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail("Valor esperado não existe no valor encontrado");
            }
        }
        public string TirarPrint(string nomeArquivo = "screen", ScreenshotImageFormat formatoImagem = ScreenshotImageFormat.Jpeg)
        {
            var projectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            nomeArquivo = nomeArquivo + "." + formatoImagem.ToString().ToLower();
            var fileLocation = Path.Combine(projectPath, nomeArquivo);

            var ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(fileLocation, ScreenshotImageFormat.Png);
            return fileLocation;
        }
    }
}
