using Bogus;
using Dominio.Contratos.Repositorio;
using Dominio.Servicos.Fornecedor.BuscaPorId;
using Moq;
using TesteUnitario.Dominio.Entidades;

namespace TesteUnitario.Dominio.Servicos.Fornecedor;

public class FornecedorBuscaPorIdDomainServiceTest
{
    private Mock<IFornecedorRepository> _fornecedorRepository;

    public FornecedorBuscaPorIdDomainServiceTest()
    {
        _fornecedorRepository = new();
    }

    private FornecedorBuscaPorIdDomainService GerarCenario(Mock<IFornecedorRepository> _fornecedorRepository)
    {
        return new FornecedorBuscaPorIdDomainService(_fornecedorRepository.Object);
    }

    [Fact(DisplayName = "Não deve buscar o fornecedor se o id não for enviado")]
    public async Task BuscarFornecedorSemIdentificadorEnviado()
    {
        int id = 0;

        var servicoDominio = GerarCenario(_fornecedorRepository);

        var result = await servicoDominio.BuscaPorId(id);

        Assert.Null(result);
    }

    [Fact(DisplayName = "Deve buscar o fornecedor quando o id for enviado")]
    public async Task DeveBuscarFornecedorQuandoOIdentificadorEnviado()
    {
        var faker = new Faker();

        var id = faker.Random.Int(min:1);

        var resultadoEsperado = FornecedorMock.GerarObjeto();

        _fornecedorRepository.Setup(repository => repository.BuscarPorIdAsync(id)).ReturnsAsync(resultadoEsperado);

        var servicoDominio = GerarCenario(_fornecedorRepository);

        var fornecedor = await servicoDominio.BuscaPorId(id);

        Assert.NotNull(fornecedor);
    }
}