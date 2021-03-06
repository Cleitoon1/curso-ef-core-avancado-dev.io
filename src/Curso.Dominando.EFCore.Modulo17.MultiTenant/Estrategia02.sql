IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [dbo].[Persons] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [TenantId] nvarchar(max) NULL,
    CONSTRAINT [PK_Persons] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [dbo].[Products] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [TenantId] nvarchar(max) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'TenantId') AND [object_id] = OBJECT_ID(N'[dbo].[Persons]'))
    SET IDENTITY_INSERT [dbo].[Persons] ON;
INSERT INTO [dbo].[Persons] ([Id], [Name], [TenantId])
VALUES (1, N'Person 1', N'tenant-1'),
(2, N'Person 2', N'tenant-1'),
(3, N'Person 3', N'tenant-2');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'TenantId') AND [object_id] = OBJECT_ID(N'[dbo].[Persons]'))
    SET IDENTITY_INSERT [dbo].[Persons] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'TenantId') AND [object_id] = OBJECT_ID(N'[dbo].[Products]'))
    SET IDENTITY_INSERT [dbo].[Products] ON;
INSERT INTO [dbo].[Products] ([Id], [Description], [TenantId])
VALUES (1, N'Product 1', N'tenant-2'),
(2, N'Product 2', N'tenant-2'),
(3, N'Product 3', N'tenant-1');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'TenantId') AND [object_id] = OBJECT_ID(N'[dbo].[Products]'))
    SET IDENTITY_INSERT [dbo].[Products] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210906125357_EstrategiaTenantSchema', N'5.0.9');
GO

COMMIT;
GO

