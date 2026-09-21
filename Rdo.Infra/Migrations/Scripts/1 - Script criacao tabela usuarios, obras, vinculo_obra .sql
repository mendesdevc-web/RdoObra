GO
-- Tabela:usuarios
CREATE TABLE [dbo].[usuarios]
(
[id] INT IDENTITY(1,1) NOT NULL,
    [email] NVARCHAR(MAX) NOT NULL,
    [usuario] NVARCHAR(MAX) NOT NULL,
    [cargo] INT NOT NULL,
    [senha_hash] VARBINARY(MAX) NOT NULL,
    [senha_salt] VARBINARY(MAX) NOT NULL,
    [token_data_criacao] DATETIME2(7) NOT NULL,

    CONSTRAINT [pk_usuarios]
        PRIMARY KEY ([id])
);

GO
-- TABELA: obras
CREATE TABLE [dbo].[obras]
(
    [id] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [df_obras_id] DEFAULT NEWSEQUENTIALID(),

    [nome] VARCHAR(150) NOT NULL,
    [endereco] VARCHAR(250) NOT NULL,
    [responsavel_tecnico] VARCHAR(150) NOT NULL,
    [status] VARCHAR(100) NULL,
    [data_inicio] DATE NOT NULL,
    [data_fim] DATE NULL,

    CONSTRAINT [pk_obras]
        PRIMARY KEY ([id])
);

GO
-- TABELA INTERMEDIÁRIA: obra_usuario
CREATE TABLE [dbo].[obra_usuario]
(
    [id] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [df_obra_usuario_id] DEFAULT NEWSEQUENTIALID(),

    [obra_id] UNIQUEIDENTIFIER NOT NULL,
    [usuario_id] INT NOT NULL,
    
    [papel] VARCHAR(20) NOT NULL,
    CONSTRAINT [pk_obra_usuario]
        PRIMARY KEY ([id]),

    CONSTRAINT [fk_obra_usuario_obra]
        FOREIGN KEY ([obra_id])
        REFERENCES [dbo].[obras] ([id]),

    CONSTRAINT [fk_obra_usuario_usuario]
        FOREIGN KEY ([usuario_id])
        REFERENCES [dbo].[usuarios] ([id]),

    CONSTRAINT [ck_obra_usuario_papel]
        CHECK ([papel] IN ('engenheiro', 'supervisor', 'adm')),

    CONSTRAINT [uq_obra_usuario]
        UNIQUE ([obra_id], [usuario_id])
);
GO