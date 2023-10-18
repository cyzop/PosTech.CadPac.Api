# Cadastro de Histório Médico
[![License](https://img.shields.io/badge/license-MIT-green)](./LICENSE)

rep-test-project

# Sobre o Projeto

O Cadastro e Histórico Médico foi desenvolvido durante a FASE 1 do curso ARQUITETURA DE SISTEMAS .NET COM AZURE.

Este cadastro tem o intuito de armazenar, de forma simplificada, o histórico médico de pacientes de um consultório médico.

A aplicação consite em uma listagem de pacientes, onde ao consultar um paciente será paresentado seu Histório Médico contendo os Sintomas reportados, os Diagnósticos e os respectivos Tratamentos prescritos pelo médico.

## Modelo Conceitual

# Tecnologias utilizadas
## Back end
- Microsoft .Net Core 7
- MongoDB

## Front end

# Como executar o projeto

## Back end

```bash
# clonar o repositório
git clone https://github.com/cyzop/rep-test-project
```

### MongoDb

Pode utilizar tanto a instalação local do banco de dados (OnPremise), quanto a utilização do banco Cloud DBaaS.

### Para utilizar instalação local
- Instalar o banco NoSql MongoDB localmente
- Ajustar os parâmetros de configuração no arquivo appsettings.json da api (ConnectionString e Secret).

``` AppSettings OnPremise
Exemplo:
 "RepositorySettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "PacienteRepository",
    "RepositoryName": "PacienteCollection",
    "Secret": ""
  }
```

### Para utilizar banco em núvem
- Ajustar os parêmtros de configuração no arquivo appsettings.json da api (ConnectionString e Secret), configurando a url do servidor em núvem, e usuário e senha para autenticação do acesso

``` AppSettings DBaaS
Exemplo:
 "RepositorySettings": {
    "ConnectionString": "mongodb+srv://{0}@mongocluster.3oa3jww.mongodb.net/",
    "Database": "PacienteRepository",
    "RepositoryName": "PacienteCollection",
    "Secret": "usuariobancodedados:senhadousuariobancodedados"
  }
```
### Utilizando o Visual Studio Community 2022
- Abrir a solução do projeto (PosTech.CadPac.Api.sln) no VS
- Definir o projeto PosTech.CadPac.Api como projeto para inicialização
- Iniciar o projeto com Depuração apertando o F5, para executar o projeto utilizando o Swagger
