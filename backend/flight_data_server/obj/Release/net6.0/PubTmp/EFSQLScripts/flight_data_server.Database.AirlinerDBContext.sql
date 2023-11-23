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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230303210654_flightdb2')
BEGIN
    CREATE TABLE [Airliner] (
        [Id] int NOT NULL IDENTITY,
        [AirlinerName] nvarchar(max) NOT NULL,
        [AirlinerCallSign] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Airliner] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230303210654_flightdb2')
BEGIN
    CREATE TABLE [User] (
        [UserId] int NOT NULL IDENTITY,
        [UserName] nvarchar(max) NULL,
        [Password] nvarchar(max) NULL,
        [Role] nvarchar(max) NULL,
        CONSTRAINT [PK_User] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230303210654_flightdb2')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AirlinerCallSign', N'AirlinerName') AND [object_id] = OBJECT_ID(N'[Airliner]'))
        SET IDENTITY_INSERT [Airliner] ON;
    EXEC(N'INSERT INTO [Airliner] ([Id], [AirlinerCallSign], [AirlinerName])
    VALUES (1, N''THY'', N''Turkish Airliner'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AirlinerCallSign', N'AirlinerName') AND [object_id] = OBJECT_ID(N'[Airliner]'))
        SET IDENTITY_INSERT [Airliner] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230303210654_flightdb2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230303210654_flightdb2', N'6.0.13');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315040959_add_airliners2')
BEGIN
    DROP TABLE [User];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315040959_add_airliners2')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AirlinerCallSign', N'AirlinerName') AND [object_id] = OBJECT_ID(N'[Airliner]'))
        SET IDENTITY_INSERT [Airliner] ON;
    EXEC(N'INSERT INTO [Airliner] ([Id], [AirlinerCallSign], [AirlinerName])
    VALUES (2, N''UAE'', N''Emirates''),
    (3, N''SIA'', N''Singapore Airlines''),
    (4, N''ACA'', N''Air Canada''),
    (5, N''UAL'', N''United Airlines''),
    (6, N''DAL'', N''Delta Air Lines''),
    (7, N''AAL'', N''American Airlines''),
    (8, N''BAW'', N''British Airways''),
    (9, N''DLH'', N''Lufthansa''),
    (10, N''AFR'', N''Air France'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AirlinerCallSign', N'AirlinerName') AND [object_id] = OBJECT_ID(N'[Airliner]'))
        SET IDENTITY_INSERT [Airliner] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230315040959_add_airliners2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230315040959_add_airliners2', N'6.0.13');
END;
GO

COMMIT;
GO

