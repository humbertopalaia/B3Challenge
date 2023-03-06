
# B3Challenge Cadastro de Tarefas

Código criado para teste de conhecimentos técnicos para vaga na B3.


## B3Challenge.Task.Worker


Microserviço (console application), em .NET 6 (C#), responsável por escutar a fila do Rabbit e efetuar as operações de inclusão, alteração e exclusão.



## B3Challenge.Gateway.API

API em .NET 6 (C#) responsável por:
- Enfileirar solicitação de inclusão
- Enfileirar solicitação de exclusão
- Enfileirar solicitação de atualização
- Realizar buscas acessando a API de consulta (B3Challenge.Search.API)


## B3Challenge.Search.API

API em .NET 6 (C#) responsável por realizar consultas no banco de dados:


## B3Challenge Frontend

Frontend desenvolvido em Angular 15 com um cadastro de tarefas desenvolvido de forma componentizada.


## Instalação ambiente desenvolvimento

- Pré-requisitos
    
    [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/vs/community/)

    [Visual Code](https://code.visualstudio.com)
    
    [NodeJS v18.12.1](https://nodejs.org/dist/v18.14.1/)

    Angular CLI 15.2.0 (usar comando npm install -g @angular/cli@15.2.0)

    [Docker Desktop](https://www.docker.com/get-started/)

    [Imagem Docker rabbitmq](https://hub.docker.com/_/rabbitmq)
    
    [Imagem Docker Sql Server](https://hub.docker.com/_/microsoft-mssql-server)
    
    [Git 2.38.1 ou mais atual](https://git-scm.com/downloads)

    [Azure Data Studio](https://azure.microsoft.com/pt-br/products/data-studio/) ou Sql Management Express
   

- Configurando

      Instale as ferramentas de pré-requisitos e reinicie o PC

  *1. Baixando os fontes*      

       1.1 Crie um diretório para armazenar os fontes, por exemplo: C:\Trabalho\B3Challenge*
       1.2 Abra o Prompt de Comando em modo administrador e navegue até o diretório criado
       1.3 Clone o repositório (git clone https://github.com/humbertopalaia/B3Challenge.git)

  *2. Banco de dados*

        2.1 Através do docker suba um container com o banco de dados Sql Server        
        2.2 Conecte-se no banco master com o usuário sa ou outro com permissão de administrador.        
        2.3 Execute o script B3ChallengeCREATEDATABASE.sql localizado na pasta SqlScripts na raiz do diretório clonado


*3. Configurando B3Challenge.Task.Worker*
    
    - Appsettings:
      
      Configurar a connection string "Default" de acordo com o ip usuário e senha de 
     
      Como a aplicação é executada em docker, não se faz necessário configurar o Host do 
      rabbit, pois ele pega como referência a instância configurada no docker-compose,
      se necessário configurar o usuário e a senha de acesso ao rabbit, por padrão vem guest/guest.

*4. Configurando B3Challenge.Search.API*
    
    - Appsettings
    
      Configurar a connection string "Default" de acordo com o ip usuário e senha de acesso do banco de dados Sql Server.

*5. Configurando B3Challenge.Gateway.API*

- Appsettings
    
       Como a aplicação é executada em docker, não se faz necessário configurar o Host
       do rabbit, pois ele pega como referência a instância configurada no 
       docker-compose, se necessário configurar o usuário e a senha de acesso ao rabbit, 
       por padrão vem guest/guest.

       Da mesma forma não é preciso configurar o acesso a B3challange.Search.API,pois
       as duas aplicações rodam no mesmo docker compose.

*5. Configurando B3Challenge Frontend*

    - Abra a pasta do projeto no Visual Studio Code
    
    - Acesse o terminal do Visual Studio Code (Ctrl+") e digite o comando npm install e vá tomar um café, pois demora. =)

    - Após terminar de instalar as dependências, acesse o arquivo environment.ts 
    localizado em: src/environments/environments.ts

    - Se necessário, altere a API de acesso do B3Challenge.Gateway.API, por padrão o 
    projeto vem configurado para acessar a porta 11594, conforme ajustado no arquivo yml 
    do docker compose do projeto .NET.
    
## Geração de pacotes para deploy 

*Aplicações .NET 6:*
    
    - Pelo Visual Studio 2022, acesse o terminal de comando e navegue até a pasta do projeto, execute o comando 
    de acordo com o ambiente de deploy, será criado uma pasta deploy na raiz da pasta do projeto contendo o pacote a ser publicado.


    Ambiente Windows x64
    
      dotnet publish -o deploy -f net6.0 -r win-x64 -c Release --self-contained true


    Ambiente Linux x64
        
        dotnet publish -o deploy -f net6.0 -r linux-x64 -c Release --self-contained true




*B3Challenge Frontend (Angular 15)*

    - Pelo Visual Studio Code, acesse o terminal e navegue até a pasta raiz do projeto e 
    execute o comando abaixo, será criado uma pasta chamada dist na raiz do projeto contendo o pacote a ser publicado:

    ng build --watch


