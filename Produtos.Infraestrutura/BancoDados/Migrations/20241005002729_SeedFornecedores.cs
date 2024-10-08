using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class SeedFornecedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Fornecedor] ON
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 1)
BEGIN
	INSERT INTO [dbo].[Fornecedor]
		([Id]
		,[Descricao]
		,[Cnpj])
	VALUES
		(1
		,'Fornecedor 1'
		,'94482919000102')
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 2)
BEGIN
	INSERT INTO [dbo].[Fornecedor]
		([Id]
		,[Descricao]
		,[Cnpj])
	VALUES
		(2
		,'Fornecedor 2'
		,'82828292000110')
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 3)
BEGIN
	INSERT INTO [dbo].[Fornecedor]
		([Id]
		,[Descricao]
		,[Cnpj])
	VALUES
		(3
		,'Fornecedor 3'
		,'08638211000166')
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 4)
BEGIN
	INSERT INTO [dbo].[Fornecedor]
		([Id]
		,[Descricao]
		,[Cnpj])
	VALUES
		(4
		,'Fornecedor 4'
		,'32876985000100')
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 5)
BEGIN
	INSERT INTO [dbo].[Fornecedor]
		([Id]
		,[Descricao]
		,[Cnpj])
	VALUES
		(5
		,'Fornecedor 5'
		,'01470908000121')
END
GO


GO
SET IDENTITY_INSERT [dbo].[Fornecedor] OFF
GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 1)
BEGIN 
	DELETE FROM [dbo].[Fornecedor] WHERE [Id] = 1
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 2)
BEGIN 
	DELETE FROM [dbo].[Fornecedor] WHERE [Id] = 2
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 3)
BEGIN 
	DELETE FROM [dbo].[Fornecedor] WHERE [Id] = 3
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 4)
BEGIN 
	DELETE FROM [dbo].[Fornecedor] WHERE [Id] = 4
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Fornecedor] WHERE [Id] = 5)
BEGIN 
	DELETE FROM [dbo].[Fornecedor] WHERE [Id] = 5
END
GO
			");
        }
    }
}
