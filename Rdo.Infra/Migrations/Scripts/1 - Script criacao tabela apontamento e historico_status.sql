-- TABELA: apontamentos
CREATE TABLE [dbo].[apontamentos]
(
    [id] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [df_apontamentos_id] DEFAULT NEWSEQUENTIALID(),

    [diario_id] UNIQUEIDENTIFIER NOT NULL,
    [servico_id] UNIQUEIDENTIFIER NOT NULL,
    [quantidade_executada] DECIMAL(12,2) NOT NULL,

    CONSTRAINT [pk_apontamentos]
        PRIMARY KEY ([id]),

    CONSTRAINT [fk_apontamentos_diario]
        FOREIGN KEY ([diario_id])
        REFERENCES [dbo].[diarios] ([id]),

    CONSTRAINT [fk_apontamentos_servico]
        FOREIGN KEY ([servico_id])
        REFERENCES [dbo].[servicos] ([id])
);
GO

-- TABELA: historico_status
CREATE TABLE [dbo].[historico_status]
(
    [id] UNIQUEIDENTIFIER NOT NULL
        CONSTRAINT [df_historico_status_id] DEFAULT NEWSEQUENTIALID(),

    [diario_id] UNIQUEIDENTIFIER NOT NULL,
    [usuario_id] INT NOT NULL,
    [status_anterior] VARCHAR(20) NOT NULL,
    [status_novo] VARCHAR(20) NOT NULL,
    [comentario] NVARCHAR(MAX) NULL,
    
    [data_hora] DATETIME2(7) NOT NULL
        CONSTRAINT [df_historico_status_data_hora] DEFAULT SYSDATETIME(),

    CONSTRAINT [pk_historico_status]
        PRIMARY KEY ([id]),

    CONSTRAINT [fk_historico_status_diario]
        FOREIGN KEY ([diario_id])
        REFERENCES [dbo].[diarios] ([id]),

    CONSTRAINT [fk_historico_status_usuario]
        FOREIGN KEY ([usuario_id])
        REFERENCES [dbo].[usuarios] ([id]),

    CONSTRAINT [ck_historico_status_anterior]
        CHECK ([status_anterior] IN (
            'rascunho',
            'enviado',
            'aprovado',
            'devolvido'
        )),

    CONSTRAINT [ck_historico_status_novo]
        CHECK ([status_novo] IN (
            'rascunho',
            'enviado',
            'aprovado',
            'devolvido'
        ))
);
GO