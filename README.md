
# Testes de Unidade com xUnit

Este projeto utiliza o framework **xUnit** para a criação e execução de testes de unidade.  
O objetivo dos testes de unidade é garantir que cada parte individual do código funcione corretamente, de forma isolada e previsível.

---

## O que é xUnit?

**xUnit** é um dos principais frameworks de testes para aplicações .NET.  
Ele é conhecido pela sua abordagem enxuta, suporte a boas práticas e integração nativa com ferramentas modernas de desenvolvimento, como o Visual Studio, GitHub Actions e Azure DevOps.

Principais vantagens do xUnit:
- Estrutura simples e moderna para escrever testes.
- Suporte a testes parametrizados e dados externos.
- Execução paralela de testes para aumentar a performance.
- Boa integração com ferramentas de CI/CD.
- Comunidade ativa e atualizações constantes.

---

## Estrutura dos Testes

Os testes são organizados seguindo uma estrutura comum:
- **Arrange**: Configuração do cenário de teste.
- **Act**: Execução da ação que se deseja testar.
- **Assert**: Validação do resultado.

Exemplo básico de teste:

```csharp
using Xunit;

public class CalculatorTests
{
    [Fact]
    public void Add_ShouldReturnCorrectSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(2, 3);

        // Assert
        Assert.Equal(5, result);
    }
}
```

### Atributos principais:
- `[Fact]`: Define um teste que não possui parâmetros.
- `[Theory]`: Permite executar o mesmo teste com diferentes conjuntos de dados.
- `[InlineData]`: Fornece dados diretamente para testes `[Theory]`.

Exemplo de teste com `[Theory]`:

```csharp
public class CalculatorTests
{
    [Theory]
    [InlineData(2, 3, 5)]
    [InlineData(-2, 2, 0)]
    [InlineData(0, 0, 0)]
    public void Add_ShouldReturnExpectedResult(int a, int b, int expected)
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        int result = calculator.Add(a, b);

        // Assert
        Assert.Equal(expected, result);
    }
}
```

---

## Convenções de Nomenclatura dos Testes

Uma prática recomendada é usar nomes descritivos para os métodos de teste, seguindo o formato:

```
MétodoASerTestado_CenárioEsperado_ResultadoEsperado
```

Exemplo:

```csharp
GetUserById_WhenUserExists_ReturnsUser()
```

Essa convenção ajuda a entender rapidamente o que está sendo testado apenas lendo o nome do teste.

---

## Rodando os Testes

Para executar os testes de unidade:

**Via Visual Studio:**
- Menu → Test → Run All Tests
- Ou através da janela "Test Explorer".

**Via Terminal:**
- Na raiz do projeto, execute:

```bash
dotnet test
```

O comando irá compilar o projeto de testes e executar todos os testes encontrados.

---

## Boas Práticas em Testes de Unidade

- **Isolamento**: Cada teste deve isolar a funcionalidade que está validando, sem dependências externas (como banco de dados, arquivos, APIs).
- **Automação**: Os testes devem ser automáticos e não exigir intervenção manual.
- **Reprodutibilidade**: Um teste deve produzir o mesmo resultado independente de quantas vezes for executado.
- **Clareza**: Um teste falho deve indicar claramente o que não está funcionando.
- **Rapidez**: Testes de unidade devem ser rápidos para permitir feedback constante durante o desenvolvimento.
- **Um único objetivo por teste**: Cada teste deve validar apenas uma coisa.

---

## Estrutura Recomendada para Projeto de Testes

Geralmente, recomenda-se criar um projeto separado para os testes:

```
/src
  /MyApp (código principal)
/tests
  /MyApp.Tests (testes de unidade)
```

Assim, é possível manter o código da aplicação separado dos testes, facilitando a manutenção e o build do projeto.

---

## Dependências Necessárias

Para instalar o xUnit e o runner de testes:

```bash
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet add package Microsoft.NET.Test.Sdk
```

Esses pacotes são necessários para:
- Criar os testes (`xunit`).
- Integrar com o Visual Studio (`xunit.runner.visualstudio`).
- Executar os testes (`Microsoft.NET.Test.Sdk`).

---

# 📌 Observação

Este projeto é de estudos, portanto, os testes podem conter exemplos simples focados no entendimento de boas práticas, organização de testes e estruturação com xUnit.
