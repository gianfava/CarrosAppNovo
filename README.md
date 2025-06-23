
# <img src="https://github.com/gianfava/CarrosAppNovo/blob/master/logo.png" alt="Logo" width="80"/> Projeto de Cadastro de Carros em .NET MAUI

Este é um aplicativo multiplataforma para desktop e mobile, desenvolvido com **.NET MAUI**, que permite o gerenciamento de uma frota de veículos. O aplicativo realiza operações de **CRUD** (Criar, Ler, Atualizar e Deletar) em um banco de dados **MySQL** hospedado remotamente.

## 🚀 Demonstração

<img src="https://github.com/gianfava/CarrosAppNovo/blob/master/screenshot.png" alt="Screenshot da Aplicação" width="740"/>

## 🔧 Funcionalidades

- **Listagem de Veículos:** Exibe todos os carros cadastrados em uma lista de fácil visualização.
- **Cadastro de Novos Veículos:** Permite adicionar novos carros ao banco de dados através de um formulário simples e intuitivo.
- **Atualização de Dados:** Ao selecionar um veículo da lista, seus dados são carregados no formulário para edição.
- **Exclusão de Registros:** Cada item na lista possui um botão para exclusão direta do registro, com confirmação.
- **Busca Dinâmica:** Filtra a lista de veículos em tempo real conforme o usuário digita no campo de busca.
- **Interface Personalizada:** Inclui ícones nos botões, logo da aplicação e um layout responsivo para melhor experiência do usuário.

## 🛠️ Tecnologias Utilizadas

- **.NET MAUI:** Framework da Microsoft para criação de aplicativos nativos multiplataforma (Android, iOS, macOS e Windows) com uma única base de código em C# e XAML.
- **C#:** Linguagem principal utilizada para a lógica do aplicativo.
- **XAML:** Linguagem de marcação para a interface do usuário.
- **MySQL:** Banco de dados para persistência dos dados.
- **MySqlConnector:** Pacote NuGet utilizado para comunicação com o MySQL.
- **MVVM (Model-View-ViewModel):** Arquitetura utilizada para separar lógica de negócio da interface.

## 📦 Como Testar o Projeto

### ✅ Pré-requisitos

- Visual Studio 2022 (ou superior) com a carga de trabalho **".NET Multi-platform App UI development"** instalada.
- Acesso a um servidor de banco de dados MySQL.

### 1️⃣ Configuração do Banco de Dados

Crie a tabela `carro` no seu banco MySQL com o seguinte script:

```sql
CREATE TABLE `carro` (
  `id` int NOT NULL AUTO_INCREMENT,
  `marca` varchar(50) DEFAULT NULL,
  `modelo` varchar(50) DEFAULT NULL,
  `ano` int DEFAULT NULL,
  `cor` varchar(30) DEFAULT NULL,
  `placa` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`id`)
);
```

### 2️⃣ Configuração da String de Conexão

Abra o arquivo `Models/Conecta.cs` no Visual Studio e configure os dados de conexão no método `Conexao()`:

```csharp
public bool Conexao()
{
    var StrCon = new MySqlConnectionStringBuilder
    {
        Server = "SEU_SERVIDOR_MYSQL",
        Port = 3306,
        UserID = "SEU_USUARIO",
        Password = "SUA_SENHA",
        Database = "Carros", // Ou o nome do seu banco de dados
        SslMode = MySqlSslMode.None
    };
    Conn = new MySqlConnection(StrCon.ToString());
    // ...
}
```

### 3️⃣ Compilar e Executar

- Abra o arquivo da solução (`.sln`) no Visual Studio 2022.
- Restaure os pacotes NuGet (geralmente é automático na primeira abertura do projeto).
- Selecione a plataforma de destino (Windows, Android, iOS ou macOS).
- Pressione **F5** ou clique em **Play** para compilar e executar a aplicação.

---

✍️ Desenvolvido com 💙 usando .NET MAUI.
