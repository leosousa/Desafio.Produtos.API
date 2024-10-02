using Bogus;
using Bogus.Extensions.Brazil;
using Dominio.Entidades;

namespace TesteUnitario.Dominio.Entidades;

public class FornecedorMock : Faker<Fornecedor>
{
    private FornecedorMock() : base("pt_BR")
    {
        RuleFor(task => task.Id, faker => faker.Random.Int(min:1));

        RuleFor(task => task.Descricao, faker => faker.Lorem.Random.String(1, 255));

        RuleFor(task => task.Cnpj, faker => faker.Company.Cnpj(true));
    }

    public static Fornecedor GerarObjeto()
    {
        return new FornecedorMock().Generate();
    }
}