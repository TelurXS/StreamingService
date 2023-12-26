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

CREATE TABLE [Accounts] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(64) NOT NULL,
    [Login] nvarchar(32) NOT NULL,
    [Email] nvarchar(64) NOT NULL,
    [Password] nvarchar(256) NOT NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Genre] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(64) NOT NULL,
    CONSTRAINT [PK_Genre] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [GenreTitle] (
    [GenresId] uniqueidentifier NOT NULL,
    [TitlesId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_GenreTitle] PRIMARY KEY ([GenresId], [TitlesId]),
    CONSTRAINT [FK_GenreTitle_Genre_GenresId] FOREIGN KEY ([GenresId]) REFERENCES [Genre] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Images] (
    [Id] uniqueidentifier NOT NULL,
    [Uri] nvarchar(256) NOT NULL,
    [TitleId] uniqueidentifier NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Titles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Description] nvarchar(1024) NOT NULL,
    [Slug] nvarchar(128) NOT NULL,
    [ReleaseDate] datetime2 NOT NULL,
    [ImageId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Titles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Titles_Images_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [Images] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LocalizedTexts] (
    [Id] uniqueidentifier NOT NULL,
    [Language] nvarchar(16) NOT NULL,
    [Value] nvarchar(1024) NOT NULL,
    [TitleId] uniqueidentifier NULL,
    [TitleId1] uniqueidentifier NULL,
    CONSTRAINT [PK_LocalizedTexts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LocalizedTexts_Titles_TitleId] FOREIGN KEY ([TitleId]) REFERENCES [Titles] ([Id]),
    CONSTRAINT [FK_LocalizedTexts_Titles_TitleId1] FOREIGN KEY ([TitleId1]) REFERENCES [Titles] ([Id])
);
GO

CREATE TABLE [Rate] (
    [Id] uniqueidentifier NOT NULL,
    [Value] real NOT NULL,
    [AuthorId] uniqueidentifier NOT NULL,
    [TitleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Rate] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rate_Accounts_AuthorId] FOREIGN KEY ([AuthorId]) REFERENCES [Accounts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Rate_Titles_TitleId] FOREIGN KEY ([TitleId]) REFERENCES [Titles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Series] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [Uri] nvarchar(256) NOT NULL,
    [TitleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Series] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Series_Titles_TitleId] FOREIGN KEY ([TitleId]) REFERENCES [Titles] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Accounts_Email] ON [Accounts] ([Email]);
GO

CREATE UNIQUE INDEX [IX_Accounts_Login] ON [Accounts] ([Login]);
GO

CREATE INDEX [IX_GenreTitle_TitlesId] ON [GenreTitle] ([TitlesId]);
GO

CREATE INDEX [IX_Images_TitleId] ON [Images] ([TitleId]);
GO

CREATE INDEX [IX_LocalizedTexts_TitleId] ON [LocalizedTexts] ([TitleId]);
GO

CREATE INDEX [IX_LocalizedTexts_TitleId1] ON [LocalizedTexts] ([TitleId1]);
GO

CREATE INDEX [IX_Rate_AuthorId] ON [Rate] ([AuthorId]);
GO

CREATE INDEX [IX_Rate_TitleId] ON [Rate] ([TitleId]);
GO

CREATE INDEX [IX_Series_TitleId] ON [Series] ([TitleId]);
GO

CREATE INDEX [IX_Titles_ImageId] ON [Titles] ([ImageId]);
GO

CREATE UNIQUE INDEX [IX_Titles_Slug] ON [Titles] ([Slug]);
GO

ALTER TABLE [GenreTitle] ADD CONSTRAINT [FK_GenreTitle_Titles_TitlesId] FOREIGN KEY ([TitlesId]) REFERENCES [Titles] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Images] ADD CONSTRAINT [FK_Images_Titles_TitleId] FOREIGN KEY ([TitleId]) REFERENCES [Titles] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231226151346_Initial', N'7.0.14');
GO

COMMIT;
GO

