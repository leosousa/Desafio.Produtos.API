using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestrutura.BancoDados.Migrations
{
    /// <inheritdoc />
    public partial class SeedProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
SET IDENTITY_INSERT [dbo].[Produto] ON
GO

SET IDENTITY_INSERT [dbo].[Produto] ON
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 1)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (1
		   ,'Produto 1'
           ,1
           ,'2024-01-01'
           ,'2025-06-18'
           ,1)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 2)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (2
		   ,'Produto 2'
           ,1
           ,'2024-01-01'
           ,'2025-06-19'
           ,1)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 3)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (3
		   ,'Produto 3'
           ,1
           ,'2024-01-02'
           ,'2025-06-20'
           ,1)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 4)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (4
		   ,'Produto 4'
           ,0
           ,'2024-01-02'
           ,'2025-06-21'
           ,1)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 5)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (5
		   ,'Produto 5'
           ,1
           ,'2024-01-02'
           ,'2025-06-21'
           ,1)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 6)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (6
		   ,'Produto 6'
           ,0
           ,'2024-01-02'
           ,'2025-06-21'
           ,2)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 7)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (7
		   ,'Produto 7'
           ,1
           ,'2024-01-02'
           ,'2025-06-22'
           ,2)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 8)
BEGIN
	INSERT INTO [dbo].[Produto]
			   ([Id]
			   ,[Descricao]
			   ,[Situacao]
			   ,[DataFabricacao]
			   ,[DataValidade]
			   ,[IdFornecedor])
		 VALUES
			   (8
			   ,'Produto 8'
			   ,1
			   ,'2024-01-03'
			   ,'2025-06-22'
			   ,2)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 9)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (9
		   ,'Produto 9'
           ,0
           ,'2024-01-03'
           ,'2025-06-23'
           ,2)
END
GO

IF NOT EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 10)
BEGIN
	INSERT INTO [dbo].[Produto]
           ([Id]
		   ,[Descricao]
           ,[Situacao]
           ,[DataFabricacao]
           ,[DataValidade]
           ,[IdFornecedor])
     VALUES
           (10
		   ,'Produto 10'
           ,1
           ,'2024-01-03'
           ,'2025-06-23'
           ,3)
END
GO


SET IDENTITY_INSERT [dbo].[Produto] OFF
GO

GO
SET IDENTITY_INSERT [dbo].[Produto] OFF
GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 1)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 1
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 2)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 2
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 3)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 3
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 4)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 4
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 5)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 5
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 6)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 6
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 7)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 7
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 8)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 8
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 9)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 9
END
GO

IF EXISTS (SELECT 1 FROM [dbo].[Produto] WHERE [Id] = 10)
BEGIN
	DELETE FROM [dbo].[Produto] WHERE [Id] = 10
END
GO
            ");
        }
    }
}
