# Pessoas
Projeto asp net core

## 1.	Deploy de uma aplicação Asp Net Core
-Aconselho a deixar uma pasta no disco principal somente com os projetos Asp Net Core, como mostrado na Figura 1.<br />
Figura 1<br />
![01](https://user-images.githubusercontent.com/72811894/96017551-83541480-0e20-11eb-905e-125c587e6f6c.png)<br />

-Copie o link do repositório: Link: <https://github.com/JoaoDosDevs/Pessoas>
-Abra a visual estúdio, clique na opção a direita “Clonar um repositório” como descrito na Figura 2.<br />
Figura 2<br />
![02](https://user-images.githubusercontent.com/72811894/96017552-83ecab00-0e20-11eb-9bd8-2db3635076a9.png)<br />

-Irá aparecer uma tela igual à que está na figura 3. Na opção “1” indicada na seta deve-se colocar o link do repositório e na opção “2” deve-se colocar o diretório onde o arquivo do projeto vai ser descarregado, isto é, na pasta que foi criada na figura 1, ao terminar é só clicar logo abaixo em “clone”.<br />
Figura 3<br />
![03](https://user-images.githubusercontent.com/72811894/96017548-82bb7e00-0e20-11eb-9d78-db4aef0a9024.png)<br />

-	Após o ter clonado o projeto deve ser exibido uma tela igual a essa da figura 4. Caso não apareça assim basta clicar 2x em no arquivo com o nome do projeto **.sln**.
Figura 4<br />
![04](https://user-images.githubusercontent.com/72811894/96017549-83541480-0e20-11eb-9cd7-dbf808147a74.png)<br />

-	Agora iremos fazer as primeiras configurações, clique em Ferramentas> Gerenciador de Pacotes do Nuget >Console do Gerenciador de Pacotes para abrir o Console do gerenciador de pacotes (Figura 5). E execute o dotnet restore _NomeDoProjeto.sln_<br />
Figura 5<br />
![05](https://user-images.githubusercontent.com/72811894/96017554-83ecab00-0e20-11eb-9ffc-80f9943f1134.png)<br />

-Normalmente bastaria acessar o _appsettings.json_ e inserir sua _StringConnection_, como mostrado na Figura 6 abaixo para fazer a conexão com banco de dados. Mas o projeto foi configurado para não conter segredos dos usuários dentro do código por isso vamos configurar o ambiente de desenvolvimento e produção.<br />
Figura 6<br />
![06](https://user-images.githubusercontent.com/72811894/96017556-84854180-0e20-11eb-9ba6-e47dc6ac9c88.png)<br />

-Aqui está um exemplo de _StringConnection_: 
“Data Source=DESKTOP-SGIU0K8;Initial Catalog=PessoasDatabase;Integrated Security=True”

Observação: o Valor da propriedade Data Soucer pode ser obtido em sua máquina abrindo o Microsoft SQL Server Management Studio 18, como na Figura 7.<br />
Figura 7<br />
-![07](https://user-images.githubusercontent.com/72811894/96017557-84854180-0e20-11eb-8474-158e84a19901.png)<br />

## 2.	Configuração do ambiente de desenvolvimento

**Como implementar:** Abra o Console do gerenciador de pacotes (Figura 5). E execute as seguintes instruções:<br />
###### dotnet user-secrets init<br />
-	Logo depois digite o comando para criando uma chave. Este comando define a App:ConnectionString chave para o valor especificado.
dotnet user-secrets set "App:ConnectionString" "Data Source=DESKTOP-SGIU0K8;Initial Catalog=PessoasDatabase;Integrated Security=True "
-	No caso esse comando irá ter a sua conectionString dentro dela com o nome do banco de dados que você irá definir conforme explicado na seção 3 Configuração do Banco de dados. 
-	Comando para listar as chaves guardadas, caso queira ver se chave foi registrada.<br />
###### dotnet user-secrets list<br />
-	Deletar uma chave caso precise.<br />
###### dotnet user-secrets remove "App:ConnectionString"<br />
## 3.	Configuração do Banco de dados
Agora vamos para parte de configuração do banco de dados, para isso acesse o menu superior do Visual Studio em Exibir > SQL Serve Object Explore (Clique nessa opção). Irá aparece uma janela igual a essa da Figura 8, acesse o seu banco de dados SQL serve em Database, clique com o botão direito do mouse, e clique na opção Add new Database, Escreva um nome para o banco de dados e depois atualize seu arquivo appsettings.json  na StringConnection no campo Catalog com o nome que você escolheu para o banco de dados.

Figura 8<br />
![08](https://user-images.githubusercontent.com/72811894/96017546-818a5100-0e20-11eb-875e-de47b9c13013.png)<br />

-	**Migrações:** Para rodar as migrações vá para o terminal e digite o comando:
add-migration migracaoInicial

-A Figura 9 mostra como é a mensagem exibida logo pós a migração ser bem sucedida.<br />

Figura 9<br />
![09](https://user-images.githubusercontent.com/72811894/96017547-82bb7e00-0e20-11eb-84b2-e7bc9bfe0e8e.png)<br />
Obs.: Esse último nome fica ao seu critério normalmente uso migracao + nome da tabela ou recursos que adicionando ao projeto.

-Feito isso é só mudar de depuração de IISExpress para o com nome do seu projeto como mostrado na Figura 10 e clicar no   para executar.<br />
Figura 10<br />
![10](https://user-images.githubusercontent.com/72811894/96019558-0e360e80-0e23-11eb-8bf4-afdb89038524.png)


