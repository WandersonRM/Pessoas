# Pessoas
Projeto asp net core

##1.	Deploy de uma aplicação Asp Net Core
-Aconselho a deixar uma pasta no disco principal somente com os projetos Asp Net Core, como mostrado na Figura 1.

-Copie o link do repositório: Link: <https://github.com/JoaoDosDevs/Pessoas>
-Abra a visual estúdio, clique na opção a direita “Clonar um repositório” como descrito na Figura 2.


-Irá aparecer uma tela igual à que está na figura 3. Na opção “1” indicada na seta deve-se colocar o link do repositório e na opção “2” deve-se colocar o diretório onde o arquivo do projeto vai ser descarregado, isto é, na pasta que foi criada na figura 1, ao terminar é só clicar logo abaixo em “clone”.

-	Após o ter clonado o projeto deve ser exibido uma tela igual a essa da figura 4. Caso não apareça assim basta clicar 2x em no arquivo com o nome do projeto **.sln**.

-	Agora iremos fazer as primeiras configurações, clique em Ferramentas> Gerenciador de Pacotes do Nuget >Console do Gerenciador de Pacotes para abrir o Console do gerenciador de pacotes (Figura 5). E execute o dotnet restore _NomeDoProjeto.sln_

-Normalmente bastaria acessar o _appsettings.json_ e inserir sua _StringConnection_, como mostrado na Figura 6 abaixo para fazer a conexão com banco de dados. Mas o projeto foi configurado para não conter segredos dos usuários dentro do código por isso vamos configurar o ambiente de desenvolvimento e produção.

-Aqui está um exemplo de _StringConnection_: 
“Data Source=DESKTOP-SGIU0K8;Initial Catalog=PessoasDatabase;Integrated Security=True”

Observação: o Valor da propriedade Data Soucer pode ser obtido em sua máquina abrindo o Microsoft SQL Server Management Studio 18, como na Figura 7

##2.	Configuração do ambiente de desenvolvimento

**Como implementar:** Abra o Console do gerenciador de pacotes (Figura 5). E execute as seguintes instruções:
######dotnet user-secrets init
-	Logo depois digite o comando para criando uma chave. Este comando define a App:ConnectionString chave para o valor especificado.
dotnet user-secrets set "App:ConnectionString" "Data Source=DESKTOP-SGIU0K8;Initial Catalog=PessoasDatabase;Integrated Security=True "
-	No caso esse comando irá ter a sua conectionString dentro dela com o nome do banco de dados que você irá definir conforme explicado na seção 3 Configuração do Banco de dados. 
-	Comando para listar as chaves guardadas, caso queira ver se chave foi registrada.
######dotnet user-secrets list
-	Deletar uma chave caso precise.
######dotnet user-secrets remove "App:ConnectionString"
##3.	Configuração do Banco de dados
Agora vamos para parte de configuração do banco de dados, para isso acesse o menu superior do Visual Studio em Exibir > SQL Serve Object Explore (Clique nessa opção). Irá aparece uma janela igual a essa da Figura 8, acesse o seu banco de dados SQL serve em Database, clique com o botão direito do mouse, e clique na opção Add new Database, Escreva um nome para o banco de dados e depois atualize seu arquivo appsettings.json  na StringConnection no campo Catalog com o nome que você escolheu para o banco de dados.

Figura 8
 

-	**Migrações:** Para rodar as migrações vá para o terminal e digite o comando:
add-migration migracaoInicial

-A Figura 9 mostra como é a mensagem exibida logo pós a migração ser bem sucedida.

Figura 9
 
Obs.: Esse último nome fica ao seu critério normalmente uso migracao + nome da tabela ou recursos que adicionando ao projeto.

-Feito isso é só mudar de depuração de IISExpress para o com nome do seu projeto como mostrado na Figura 10 e clicar no   para executar.



