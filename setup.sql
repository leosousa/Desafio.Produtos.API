IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'ProdutosDb')
BEGIN
    CREATE DATABASE ProdutosDb
END