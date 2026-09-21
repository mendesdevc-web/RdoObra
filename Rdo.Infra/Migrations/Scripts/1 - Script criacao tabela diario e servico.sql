-- TABELA: servicos
CREATE TABLE [dbo].[servicos]
(
    [id] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [df_servicos_id] DEFAULT NEWSEQUENTIALID(),

    [obra_id] UNIQUEIDENTIFIER NOT NULL,
    [descricao] VARCHAR(200) NOT NULL,
    [unidade_medida] VARCHAR(10) NOT NULL,
    [quantidade_orcada] DECIMAL(12,2) NOT NULL,
    [peso_orcamento] DECIMAL(5,2) NULL,

    CONSTRAINT [pk_servicos]
        PRIMARY KEY ([id]),

    CONSTRAINT [fk_servicos_obra]
        FOREIGN KEY ([obra_id])
        REFERENCES [dbo].[obras] ([id]),

    CONSTRAINT [ck_servicos_peso_orcamento]
        CHECK ([peso_orcamento] >= 0 AND [peso_orcamento] <= 100)
);
GO

-- TABELA: diarios
CREATE TABLE [dbo].[diarios]
(
    [id] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [df_diarios_id] DEFAULT NEWSEQUENTIALID(),

    [obra_id] UNIQUEIDENTIFIER NOT NULL,
    [usuario_id] INT NOT NULL,
    [numero_sequencial] INT NOT NULL,
    [data] DATE NOT NULL,
    [clima] VARCHAR(100) NULL,
    [efetivo_mao_obra] NVARCHAR(MAX) NULL,
    [equipamentos] NVARCHAR(MAX) NULL,
    [ocorrencias] NVARCHAR(MAX) NULL,
    [comentario] NVARCHAR(MAX) NULL,
    [status] VARCHAR(20) NOT NULL,

    [criado_em] DATETIME2(7) NOT NULL
        CONSTRAINT [df_diarios_criado_em] DEFAULT SYSDATETIME(),

    CONSTRAINT [pk_diarios]
        PRIMARY KEY ([id]),

    CONSTRAINT [fk_diarios_obra]
        FOREIGN KEY ([obra_id])
        REFERENCES [dbo].[obras] ([id]),

    CONSTRAINT [fk_diarios_usuario]
        FOREIGN KEY ([usuario_id])
        REFERENCES [dbo].[usuarios] ([id]),

    CONSTRAINT [ck_diarios_status]
        CHECK ([status] IN (
            'rascunho',
            'enviado',
            'aprovado',
            'devolvido'
        )),

    CONSTRAINT [uq_diarios_obra_data]
        UNIQUE ([obra_id], [data]),

    CONSTRAINT [uq_diarios_obra_numero]
        UNIQUE ([obra_id], [numero_sequencial])
);
GO