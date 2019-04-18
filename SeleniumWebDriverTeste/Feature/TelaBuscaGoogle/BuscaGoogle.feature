#language: pt-BR
Funcionalidade: BuscaGoogle
	Como um usuário de internet
	Eu desejo realizar buscar no Google
	Para ter acesso a informações consultadas

@TesteRegressao
Cenario: Busca com sucesso
	Dado que eu acesso o site do Google
	E informo "WebDriver" no campo de busca
	E clico no botao pesquisar
	Entao o Google retorna as informações correspondentes a "WebDriver"

@TesteRegressao
Cenario: Estou com sorte
	Dado que eu acesso o site do Google
	E informo "WebDriver" no campo de busca
	E clico no botao estou com sorte
	Entao sou redirecionado para o site Seleniumhq.org