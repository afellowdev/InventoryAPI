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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220116034317_InitialCreate')
BEGIN
    CREATE TABLE [ItemLocations] (
        [Id] int NOT NULL IDENTITY,
        [Location] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_ItemLocations] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220116034317_InitialCreate')
BEGIN
    CREATE TABLE [ItemTypes] (
        [Id] int NOT NULL IDENTITY,
        [Type] nvarchar(20) NOT NULL,
        CONSTRAINT [PK_ItemTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220116034317_InitialCreate')
BEGIN
    CREATE TABLE [Items] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(100) NOT NULL,
        [Notes] nvarchar(200) NOT NULL,
        [UpdatedDate] datetime2 NOT NULL,
        [ItemTypeId] int NOT NULL,
        [ItemLocationId] int NOT NULL,
        CONSTRAINT [PK_Items] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Items_ItemLocations_ItemLocationId] FOREIGN KEY ([ItemLocationId]) REFERENCES [ItemLocations] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Items_ItemTypes_ItemTypeId] FOREIGN KEY ([ItemTypeId]) REFERENCES [ItemTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220116034317_InitialCreate')
BEGIN
    CREATE INDEX [IX_Items_ItemLocationId] ON [Items] ([ItemLocationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220116034317_InitialCreate')
BEGIN
    CREATE INDEX [IX_Items_ItemTypeId] ON [Items] ([ItemTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220116034317_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220116034317_InitialCreate', N'6.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220118033757_AddItemImage')
BEGIN
    CREATE TABLE [ItemImage] (
        [Id] int NOT NULL IDENTITY,
        [ImageFile] nvarchar(200) NOT NULL,
        [Notes] nvarchar(200) NOT NULL,
        [ItemId] int NOT NULL,
        CONSTRAINT [PK_ItemImage] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ItemImage_Items_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Items] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220118033757_AddItemImage')
BEGIN
    CREATE INDEX [IX_ItemImage_ItemId] ON [ItemImage] ([ItemId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220118033757_AddItemImage')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220118033757_AddItemImage', N'6.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220118035217_AddItemImage_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220118035217_AddItemImage_2', N'6.0.1');
END;
GO

COMMIT;
GO

