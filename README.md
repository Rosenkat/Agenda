# Agenda
Projeto de Agenda para estudar mais sobre o uso do EntityFramework nas operações entre C# e banco de dados. 
__________________________________________________________________________________________________________________________________________________________________________
INSTRUÇÕES DE USO:

Para que possa tirar proveito de 100% da estrutura deste código, recomendo utilizar o banco de dados SQL Server, deixo abaixo o script da tabela onde são gravados estes dados:

USE [Banco criado no seu SQL Server]
GO

/****** Object:  Table [dbo].[Contatos]    Script Date: 26/02/2023 20:46:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contatos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](80) NULL,
	[Email] [varchar](90) NULL,
	[Telefone] [varchar](20) NULL,
 CONSTRAINT [PK_Contatos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
