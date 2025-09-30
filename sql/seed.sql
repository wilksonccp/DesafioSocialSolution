USE TesteTecnicoImobiliaria;
GO

INSERT INTO dbo.CLIENTE (NM_CLIENTE, DS_EMAIL, NR_CPF, NR_CNPJ, FL_ATIVO)
VALUES ('Cliente Teste', 'cliente@teste.com', '12345678901', NULL, 1);

DECLARE @clienteId INT = SCOPE_IDENTITY();
DECLARE @tipoNegocio INT = (SELECT TOP 1 CD_TIPO_IMOVEL FROM dbo.TIPO_IMOVEL ORDER BY CD_TIPO_IMOVEL);

INSERT INTO dbo.IMOVEL
(
    CD_CLIENTE,
    CD_TIPO_IMOVEL,
    VL_IMOVEL,
    DT_PUBLICACAO,
    DS_IMOVEL,
    FL_ATIVO,
    NR_CEP,
    NM_LOGRADOURO,
    DS_COMPLEMENTO,
    NM_BAIRRO,
    NM_LOCALIDADE,
    SG_UF
)
VALUES
(
    @clienteId,
    @tipoNegocio,
    350000.00,
    CAST(GETDATE() AS DATE),
    'Apartamento modelo para testes',
    1,
    '01001000',
    'Praça da Sé',
    'Apto 101',
    'Sé',
    'São Paulo',
    'SP'
);
GO
