USE [Golden]
GO
/****** Object:  Table [dbo].[BasicAttachmentList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicAttachmentList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[ParentType] [varchar](20) NOT NULL,
	[FileName] [varchar](150) NOT NULL,
	[Attachment] [varbinary](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AttachmentList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UX_AttachmentList] UNIQUE NONCLUSTERED 
(
	[ParentId] ASC,
	[FileName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicCalendarList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicCalendarList](
	[UserId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Notes] [varchar](1024) NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Calendar] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicCurrencyList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicCurrencyList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](5) NOT NULL,
	[ExchangeRate] [numeric](10, 2) NOT NULL,
	[Fixed] [bit] NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
	[Default] [bit] NOT NULL,
 CONSTRAINT [PK_CurrencyList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicImageGalleryList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicImageGalleryList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[FileName] [varchar](150) NOT NULL,
	[Attachment] [varbinary](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ImageGalleryList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UX_ImageGalleryList] UNIQUE NONCLUSTERED 
(
	[FileName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicItemList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicItemList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartNumber] [varchar](50) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Description] [text] NULL,
	[Unit] [varchar](10) NOT NULL,
	[Price] [numeric](10, 2) NOT NULL,
	[VatId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ItemList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ItemList] UNIQUE NONCLUSTERED 
(
	[PartNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicUnitList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicUnitList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](10) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
	[Default] [bit] NOT NULL,
 CONSTRAINT [PK_UnitList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_UnitList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BasicVatList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasicVatList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Value] [numeric](10, 2) NOT NULL,
	[Default] [bit] NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_VatList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_VatList] UNIQUE NONCLUSTERED 
(
	[Value] ASC,
	[Active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessAddressList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessAddressList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AddressType] [varchar](20) NOT NULL,
	[CompanyName] [varchar](150) NOT NULL,
	[ContactName] [varchar](150) NULL,
	[Street] [varchar](150) NOT NULL,
	[City] [varchar](150) NOT NULL,
	[PostCode] [varchar](20) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Email] [varchar](150) NULL,
	[BankAccount] [varchar](150) NULL,
	[Ico] [varchar](20) NULL,
	[Dic] [varchar](20) NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AddressList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessBranchList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessBranchList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](150) NOT NULL,
	[ContactName] [varchar](150) NULL,
	[Street] [varchar](150) NOT NULL,
	[City] [varchar](150) NOT NULL,
	[PostCode] [varchar](20) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Email] [varchar](150) NULL,
	[BankAccount] [varchar](150) NULL,
	[Ico] [varchar](20) NULL,
	[Dic] [varchar](20) NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_BranchList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_BranchList] UNIQUE NONCLUSTERED 
(
	[CompanyName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessCreditNoteList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessCreditNoteList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[Supplier] [varchar](512) NOT NULL,
	[Customer] [varchar](512) NOT NULL,
	[IssueDate] [datetime2](7) NOT NULL,
	[InvoiceNumber] [varchar](20) NULL,
	[Storned] [bit] NOT NULL,
	[TotalCurrencyId] [int] NOT NULL,
	[Description] [text] NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CreditNoteList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_CreditNoteList] UNIQUE NONCLUSTERED 
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessCreditNoteSupportList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessCreditNoteSupportList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[PartNumber] [varchar](50) NULL,
	[Name] [varchar](150) NOT NULL,
	[Unit] [varchar](10) NOT NULL,
	[PcsPrice] [numeric](10, 2) NOT NULL,
	[Count] [numeric](10, 2) NOT NULL,
	[TotalPrice] [numeric](10, 2) NOT NULL,
	[Vat] [numeric](10, 2) NOT NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CreditNoteItemList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessExchangeRateList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessExchangeRateList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[Value] [decimal](10, 2) NOT NULL,
	[ValidFrom] [date] NULL,
	[ValidTo] [date] NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_CourseList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessIncomingInvoiceList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessIncomingInvoiceList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[Supplier] [varchar](512) NOT NULL,
	[Customer] [varchar](512) NOT NULL,
	[IssueDate] [datetime2](7) NOT NULL,
	[TaxDate] [datetime2](7) NOT NULL,
	[MaturityDate] [datetime2](7) NOT NULL,
	[PaymentMethodId] [int] NOT NULL,
	[MaturityId] [int] NOT NULL,
	[OrderNumber] [varchar](50) NULL,
	[Storned] [bit] NOT NULL,
	[PaymentStatusId] [int] NOT NULL,
	[TotalCurrencyId] [int] NOT NULL,
	[Description] [text] NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_IncomingInvoiceList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_IncomingInvoiceList] UNIQUE NONCLUSTERED 
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessIncomingInvoiceSupportList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessIncomingInvoiceSupportList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[PartNumber] [varchar](50) NULL,
	[Name] [varchar](150) NOT NULL,
	[Unit] [varchar](10) NOT NULL,
	[PcsPrice] [numeric](10, 2) NOT NULL,
	[Count] [numeric](10, 2) NOT NULL,
	[TotalPrice] [numeric](10, 2) NOT NULL,
	[Vat] [numeric](10, 2) NOT NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_IncomingInvoiceItemList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessIncomingOrderList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessIncomingOrderList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[Supplier] [varchar](512) NOT NULL,
	[Customer] [varchar](512) NOT NULL,
	[Storned] [bit] NOT NULL,
	[TotalCurrencyId] [int] NOT NULL,
	[Description] [text] NULL,
	[CustomerOrderNumber] [varchar](50) NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_IncomingOrderList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_IncomingOrderList] UNIQUE NONCLUSTERED 
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessIncomingOrderSupportList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessIncomingOrderSupportList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[PartNumber] [varchar](50) NULL,
	[Name] [varchar](150) NOT NULL,
	[Unit] [varchar](10) NOT NULL,
	[PcsPrice] [numeric](10, 2) NOT NULL,
	[Count] [numeric](10, 2) NOT NULL,
	[TotalPrice] [numeric](10, 2) NOT NULL,
	[Vat] [numeric](10, 2) NOT NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_IncomingOrderItemList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessMaturityList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessMaturityList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Value] [int] NOT NULL,
	[Default] [bit] NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_MaturityList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_MaturityList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessNotesList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessNotesList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_NotesList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_NotesList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessOfferList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessOfferList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[Supplier] [varchar](512) NOT NULL,
	[Customer] [varchar](512) NOT NULL,
	[OfferValidity] [int] NOT NULL,
	[Storned] [bit] NOT NULL,
	[TotalCurrencyId] [int] NOT NULL,
	[Description] [text] NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_OfferList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_OfferList] UNIQUE NONCLUSTERED 
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessOfferSupportList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessOfferSupportList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[PartNumber] [varchar](50) NULL,
	[Name] [varchar](150) NOT NULL,
	[Unit] [varchar](10) NOT NULL,
	[PcsPrice] [numeric](10, 2) NOT NULL,
	[Count] [numeric](10, 2) NOT NULL,
	[TotalPrice] [numeric](10, 2) NOT NULL,
	[Vat] [numeric](10, 2) NOT NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_OfferItemList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessOutgoingInvoiceList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessOutgoingInvoiceList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[Supplier] [varchar](512) NOT NULL,
	[Customer] [varchar](512) NOT NULL,
	[IssueDate] [datetime2](7) NOT NULL,
	[TaxDate] [datetime2](7) NOT NULL,
	[MaturityDate] [datetime2](7) NOT NULL,
	[PaymentMethodId] [int] NOT NULL,
	[MaturityId] [int] NOT NULL,
	[OrderNumber] [varchar](50) NULL,
	[Storned] [bit] NOT NULL,
	[PaymentStatusId] [int] NOT NULL,
	[TotalCurrencyId] [int] NOT NULL,
	[Description] [text] NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[ReceiptId] [int] NULL,
	[CreditNoteId] [int] NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_OutgoingInvoiceList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_OutgoingInvoiceList] UNIQUE NONCLUSTERED 
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessOutgoingInvoiceSupportList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessOutgoingInvoiceSupportList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[PartNumber] [varchar](50) NULL,
	[Name] [varchar](150) NOT NULL,
	[Unit] [varchar](10) NOT NULL,
	[PcsPrice] [numeric](10, 2) NOT NULL,
	[Count] [numeric](10, 2) NOT NULL,
	[TotalPrice] [numeric](10, 2) NOT NULL,
	[Vat] [numeric](10, 2) NOT NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_OutgoingInvoiceItemList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessOutgoingOrderList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessOutgoingOrderList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[Customer] [varchar](512) NOT NULL,
	[Supplier] [varchar](512) NOT NULL,
	[Storned] [bit] NOT NULL,
	[TotalCurrencyId] [int] NOT NULL,
	[Description] [text] NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_OutgoingOrderList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_OutgoingOrderList] UNIQUE NONCLUSTERED 
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessOutgoingOrderSupportList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessOutgoingOrderSupportList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[PartNumber] [varchar](50) NULL,
	[Name] [varchar](150) NOT NULL,
	[Unit] [varchar](10) NOT NULL,
	[PcsPrice] [numeric](10, 2) NOT NULL,
	[Count] [numeric](10, 2) NOT NULL,
	[TotalPrice] [numeric](10, 2) NOT NULL,
	[Vat] [numeric](10, 2) NOT NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_OutgoingOrderItemList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessPaymentMethodList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessPaymentMethodList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
	[Default] [bit] NOT NULL,
	[Description] [text] NULL,
	[AutoGenerateReceipt] [bit] NOT NULL,
	[AllowGenerateReceipt] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PaymentMethodList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_PaymentMethodList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessPaymentStatusList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessPaymentStatusList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Default] [bit] NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Receipt] [bit] NOT NULL,
	[CreditNote] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PaymentStatusList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_PaymentStatusList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessReceiptList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessReceiptList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[InvoiceNumber] [varchar](20) NULL,
	[Supplier] [varchar](512) NOT NULL,
	[Customer] [varchar](512) NOT NULL,
	[IssueDate] [datetime2](7) NOT NULL,
	[Storned] [bit] NOT NULL,
	[TotalCurrencyId] [int] NOT NULL,
	[Description] [text] NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReceiptList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ReceiptList] UNIQUE NONCLUSTERED 
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessReceiptSupportList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessReceiptSupportList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentNumber] [varchar](20) NOT NULL,
	[PartNumber] [varchar](50) NULL,
	[Name] [varchar](150) NOT NULL,
	[Unit] [varchar](10) NOT NULL,
	[PcsPrice] [numeric](10, 2) NOT NULL,
	[Count] [numeric](10, 2) NOT NULL,
	[TotalPrice] [numeric](10, 2) NOT NULL,
	[Vat] [numeric](10, 2) NOT NULL,
	[TotalPriceWithVat] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReceiptItemList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BusinessWarehouseList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BusinessWarehouseList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[AllowNegativeStatus] [bit] NOT NULL,
	[Default] [bit] NOT NULL,
	[LockedByStockTaking] [bit] NOT NULL,
	[LastStockTaking] [datetime2](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WarehouseList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WarehouseList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocSrvDocTemplateList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocSrvDocTemplateList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[Template] [text] NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DocSrvDocTemplateList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_DocSrvDocTemplateList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocSrvDocumentationCodeLibraryList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocSrvDocumentationCodeLibraryList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](2096) NULL,
	[MdContent] [text] NOT NULL,
	[HtmlContent] [text] NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DocumentationCodeLibraryList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_DocumentationCodeLibraryList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocSrvDocumentationGroupList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocSrvDocumentationGroupList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DocumentationGroupList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_DocumentationGroupList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DocSrvDocumentationList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocSrvDocumentationList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DocumentationGroupId] [int] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Sequence] [int] NOT NULL,
	[Description] [text] NULL,
	[MdContent] [text] NOT NULL,
	[HtmlContent] [text] NOT NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[AutoVersion] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DocumentationList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_DocumentationList] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[DocumentationGroupId] ASC,
	[AutoVersion] ASC,
	[TimeStamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlobalEmailTemplateList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalEmailTemplateList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemLanguageId] [int] NOT NULL,
	[TemplateName] [varchar](50) NOT NULL,
	[Variables] [text] NOT NULL,
	[Subject] [varchar](255) NOT NULL,
	[Email] [text] NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_EmailTemplateList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_EmailTemplateList] UNIQUE NONCLUSTERED 
(
	[SystemLanguageId] ASC,
	[TemplateName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlobalLanguageList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalLanguageList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ServerLanguageList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ServerLanguageList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlobalMottoList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalMottoList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_MottoList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_MottoList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlobalUserList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalUserList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserName] [varchar](150) NOT NULL,
	[Password] [varchar](2048) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[SurName] [varchar](150) NOT NULL,
	[Description] [text] NULL,
	[Active] [bit] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Token] [varchar](2048) NULL,
	[Expiration] [datetime2](7) NULL,
	[Photo] [varbinary](max) NULL,
	[MimeType] [varchar](100) NULL,
	[PhotoPath] [varchar](500) NULL,
 CONSTRAINT [PK_username] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_UserList] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GlobalUserRoleList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GlobalUserRoleList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserRoleList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_UserRoleList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicSrvLicenseActivationFailList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicSrvLicenseActivationFailList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IpAddress] [varchar](50) NOT NULL,
	[UnlockCode] [varchar](50) NULL,
	[PartNumber] [varchar](50) NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LicenseActivationFailList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicSrvLicenseAlgorithmList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicSrvLicenseAlgorithmList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AddressId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[ValidFrom] [date] NULL,
	[ValidTo] [date] NULL,
	[Algorithm] [varchar](2000) NOT NULL,
	[Description] [text] NULL,
	[LimitActive] [bit] NOT NULL,
	[ActivationLimit] [int] NULL,
	[UsedCount] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LicSrvLicenseAlgorithmList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_LicSrvLicenseAlgorithmList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicSrvUsedLicenseList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicSrvUsedLicenseList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AlgorithmName] [varchar](30) NOT NULL,
	[PartNumber] [varchar](50) NOT NULL,
	[UnlockCode] [varchar](50) NOT NULL,
	[AddressId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[License] [varchar](50) NOT NULL,
	[ActivateDate] [datetime2](7) NOT NULL,
	[IpAddress] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UsedLicenseList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PortalMessagesList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PortalMessagesList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebMessagesList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebMessagesList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdGuidGroupList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdGuidGroupList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProdGuidGroupList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ProdGuidGroupList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdGuidOperationList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdGuidOperationList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkPlace] [int] NOT NULL,
	[PartNumber] [varchar](50) NOT NULL,
	[OperationNumber] [int] NOT NULL,
	[Note] [varchar](100) NOT NULL,
	[PcsPerHour] [int] NOT NULL,
	[KcPerKs] [numeric](10, 4) NOT NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProdGuidOperationList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ProdGuidOperationList] UNIQUE NONCLUSTERED 
(
	[WorkPlace] ASC,
	[OperationNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdGuidPartList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdGuidPartList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WorkPlace] [int] NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[Name] [varchar](100) NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProdGuidPartList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ProdGuidPartList] UNIQUE NONCLUSTERED 
(
	[WorkPlace] ASC,
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdGuidPersonList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdGuidPersonList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[PersonalNumber] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[SurName] [varchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProdGuidPersonList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ProdGuidPersonList] UNIQUE NONCLUSTERED 
(
	[PersonalNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProdGuidWorkList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProdGuidWorkList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[PersonalNumber] [int] NOT NULL,
	[WorkPlace] [int] NOT NULL,
	[OperationNumber] [int] NOT NULL,
	[WorkTime] [time](7) NOT NULL,
	[Pcs] [int] NOT NULL,
	[Amount] [numeric](10, 2) NOT NULL,
	[WorkPower] [numeric](10, 2) NOT NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProdGuidWorkList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProviderAutoGenServerReqList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderAutoGenServerReqList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IpAddress] [varchar](20) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProviderAutoGenServerCreateRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ProviderAutoGenServerCreateRequest] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProviderGeneratedLicenseList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderGeneratedLicenseList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartNumber] [varchar](50) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Expiration] [datetime2](7) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ProviderGeneratedLicenseList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ProviderGeneratedLicenseList] UNIQUE NONCLUSTERED 
(
	[PartNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProviderGeneratedToolList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProviderGeneratedToolList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Rating] [int] NULL,
	[DescActive] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_GeneratedToolList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerBrowsablePathList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerBrowsablePathList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[WebRootPath] [varchar](2048) NOT NULL,
	[AliasPath] [varchar](255) NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ServerBrowsablePathList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ServerBrowsablePathList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerHealthCheckTaskList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerHealthCheckTaskList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskName] [varchar](100) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[ServerDrive] [varchar](50) NULL,
	[FolderPath] [varchar](1024) NULL,
	[DbSqlConnection] [varchar](1024) NULL,
	[IpAddress] [varchar](20) NULL,
	[ServerUrlPath] [varchar](1024) NULL,
	[UrlPath] [varchar](1024) NULL,
	[SizeMB] [int] NULL,
	[Port] [int] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_HealthCheckTaskList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_HealthCheckTaskList] UNIQUE NONCLUSTERED 
(
	[TaskName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerLiveDataMonitorList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerLiveDataMonitorList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RootPath] [varchar](1024) NOT NULL,
	[FileExtensions] [varchar](1024) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ServerLiveDataMonitorList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ServerLiveDataMonitorList] UNIQUE NONCLUSTERED 
(
	[RootPath] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerSettingList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerSettingList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](150) NOT NULL,
	[Value] [nvarchar](150) NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_AdminConfiguration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerToolPanelDefinitionList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerToolPanelDefinitionList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToolTypeId] [int] NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Command] [varchar](500) NOT NULL,
	[IconName] [varchar](50) NOT NULL,
	[IconColor] [varchar](50) NOT NULL,
	[BackgroundColor] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ServerToolPanelDefinitionList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ServerToolPanelDefinitionList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServerToolTypeList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerToolTypeList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ServerToolTypeList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ServerToolTypeList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemCustomPageList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemCustomPageList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [varchar](250) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SystemCustomPageList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_SystemCustomPageList] UNIQUE NONCLUSTERED 
(
	[PageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemDocumentAdviceList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemDocumentAdviceList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BranchId] [int] NOT NULL,
	[DocumentType] [varchar](50) NOT NULL,
	[Prefix] [varchar](10) NOT NULL,
	[Number] [varchar](10) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DocumentAdvice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemDocumentTypeList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemDocumentTypeList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DocumentTypeList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_DocumentTypeList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemFailList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemFailList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[UserName] [varchar](50) NULL,
	[LogLevel] [varchar](20) NOT NULL,
	[Message] [text] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SystemFailList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemGroupMenuList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemGroupMenuList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SystemGroupMenuList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_SystemGroupMenuList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemIgnoredExceptionList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemIgnoredExceptionList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ErrorNumber] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[Active] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_IgnoredExceptionList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_IgnoredExceptionList] UNIQUE NONCLUSTERED 
(
	[ErrorNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemLoginHistoryList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemLoginHistoryList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IpAddress] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserName] [varchar](150) NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemMenuList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemMenuList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuType] [varchar](50) NOT NULL,
	[GroupId] [int] NOT NULL,
	[FormPageName] [varchar](250) NOT NULL,
	[AccessRole] [varchar](1024) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SystemMenuList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_GlobalMenuList] UNIQUE NONCLUSTERED 
(
	[FormPageName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemParameterList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemParameterList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[SystemName] [varchar](50) NOT NULL,
	[Value] [text] NOT NULL,
	[Type] [varchar](max) NOT NULL,
	[Description] [varchar](max) NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ParameterList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ParameterList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemReportList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemReportList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [varchar](50) NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[JoinedId] [bit] NOT NULL,
	[Description] [text] NULL,
	[ReportPath] [varchar](500) NULL,
	[MimeType] [varchar](100) NOT NULL,
	[File] [varbinary](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Default] [bit] NOT NULL,
 CONSTRAINT [PK_ReportList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemReportQueueList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemReportQueueList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Sequence] [int] NOT NULL,
	[Sql] [nvarchar](max) NOT NULL,
	[TableName] [varchar](150) NOT NULL,
	[Filter] [nvarchar](max) NULL,
	[Search] [varchar](50) NULL,
	[SearchColumnList] [nvarchar](max) NULL,
	[SearchFilterIgnore] [bit] NOT NULL,
	[RecId] [int] NULL,
	[RecIdFilterIgnore] [bit] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_ReportQueue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_ReportQueue] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemSvgIconList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSvgIconList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[SvgIconPath] [varchar](4096) NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SvgIconList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_SvgIconList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemTranslationList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemTranslationList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemName] [varchar](50) NOT NULL,
	[DescriptionCz] [varchar](500) NULL,
	[DescriptionEn] [varchar](500) NULL,
	[UserId] [int] NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SystemTranslationList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_SystemTranslationList] UNIQUE NONCLUSTERED 
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TemplateList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemplateList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Default] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_TemplateList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_TemplateList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebBannedIpAddressList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebBannedIpAddressList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IpAddress] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebBannedUserList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebBannedUserList] UNIQUE NONCLUSTERED 
(
	[IpAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebCodeLibraryList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebCodeLibraryList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](2096) NULL,
	[HtmlContent] [text] NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebCodeLibraryList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebCodeLibraryList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebCoreFileList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebCoreFileList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[FileName] [varchar](50) NOT NULL,
	[FileType] [varchar](50) NOT NULL,
	[MetroPath] [varchar](100) NOT NULL,
	[Description] [text] NULL,
	[FileContent] [text] NOT NULL,
	[UserId] [int] NOT NULL,
	[AutoMinify] [bit] NOT NULL,
	[AutoUpdateOnSave] [bit] NOT NULL,
	[AutoMinifyWaiting] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebCoreFileList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebCoreFileList] UNIQUE NONCLUSTERED 
(
	[FileName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebDeveloperNewsList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebDeveloperNewsList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebDeveloperNewsList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebDeveloperNewsList] UNIQUE NONCLUSTERED 
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebGroupMenuList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebGroupMenuList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[Onclick] [varchar](255) NULL,
	[Name] [varchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebGroupMenuList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebGroupMenuList] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebMenuList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebMenuList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[Sequence] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[MenuClass] [varchar](100) NULL,
	[Description] [varchar](2096) NULL,
	[HtmlContent] [text] NOT NULL,
	[UserMenu] [bit] NOT NULL,
	[AdminMenu] [bit] NOT NULL,
	[UserIPAddress] [varchar](50) NULL,
	[UserId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[Default] [bit] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebMenuList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebMenuList] UNIQUE NONCLUSTERED 
(
	[Name] ASC,
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebSettingList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebSettingList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](50) NOT NULL,
	[Value] [text] NOT NULL,
	[Description] [text] NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebSettingList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebSettingList] UNIQUE NONCLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebUserSettingList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebUserSettingList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](250) NOT NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebUserSettingList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebUserSettingList] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebVisitIpList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebVisitIpList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WebHostIp] [varchar](50) NOT NULL,
	[Description] [text] NULL,
	[WhoIsInformations] [text] NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebVisitIpList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_WebVisitIpList] UNIQUE NONCLUSTERED 
(
	[WebHostIp] ASC,
	[TimeStamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AttachmentList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_AttachmentList] ON [dbo].[BasicAttachmentList]
(
	[ParentId] ASC,
	[ParentType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AddressList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_AddressList] ON [dbo].[BusinessAddressList]
(
	[AddressType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_IncomingInvoiceItemList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_IncomingInvoiceItemList] ON [dbo].[BusinessIncomingInvoiceSupportList]
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_IncomingOrderList_Supplier]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_IncomingOrderList_Supplier] ON [dbo].[BusinessIncomingOrderList]
(
	[Supplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_IncomingOrderItemList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_IncomingOrderItemList] ON [dbo].[BusinessIncomingOrderSupportList]
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OfferList_Customer]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_OfferList_Customer] ON [dbo].[BusinessOfferList]
(
	[Customer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OfferItemList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_OfferItemList] ON [dbo].[BusinessOfferSupportList]
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OutgoingInvoiceList_Customer]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_OutgoingInvoiceList_Customer] ON [dbo].[BusinessOutgoingInvoiceList]
(
	[Customer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OutgoingInvoiceItemList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_OutgoingInvoiceItemList] ON [dbo].[BusinessOutgoingInvoiceSupportList]
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OutgoingOrderList_Supplier]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_OutgoingOrderList_Supplier] ON [dbo].[BusinessOutgoingOrderList]
(
	[Supplier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OutgoingOrderItemList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_OutgoingOrderItemList] ON [dbo].[BusinessOutgoingOrderSupportList]
(
	[DocumentNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_LicenseActivationFailList_IpAddress]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_LicenseActivationFailList_IpAddress] ON [dbo].[LicSrvLicenseActivationFailList]
(
	[IpAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_LicenseActivationFailList_PartNumber]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_LicenseActivationFailList_PartNumber] ON [dbo].[LicSrvLicenseActivationFailList]
(
	[PartNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_GeneratedToolList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_GeneratedToolList] ON [dbo].[ProviderGeneratedToolList]
(
	[Name] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_GeneratedToolList_1]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_GeneratedToolList_1] ON [dbo].[ProviderGeneratedToolList]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_GeneratedToolList_2]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_GeneratedToolList_2] ON [dbo].[ProviderGeneratedToolList]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DocumentAdviceList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_DocumentAdviceList] ON [dbo].[SystemDocumentAdviceList]
(
	[BranchId] ASC,
	[DocumentType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_LoginHistoryList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_LoginHistoryList] ON [dbo].[SystemLoginHistoryList]
(
	[IpAddress] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_LoginHistoryList_1]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_LoginHistoryList_1] ON [dbo].[SystemLoginHistoryList]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReportQueueList]    Script Date: 07.12.2023 13:45:43 ******/
CREATE NONCLUSTERED INDEX [IX_ReportQueueList] ON [dbo].[SystemReportQueueList]
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReportQueueList_1]    Script Date: 07.12.2023 13:45:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ReportQueueList_1] ON [dbo].[SystemReportQueueList]
(
	[TableName] ASC,
	[Sequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BasicAttachmentList] ADD  CONSTRAINT [DF_AttachmentList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BasicCalendarList] ADD  CONSTRAINT [DF_Calendar_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BasicCurrencyList] ADD  CONSTRAINT [DF_CurrencyList_ExchangeRate]  DEFAULT ((1)) FOR [ExchangeRate]
GO
ALTER TABLE [dbo].[BasicCurrencyList] ADD  CONSTRAINT [DF_CurrencyList_Fixed]  DEFAULT ((1)) FOR [Fixed]
GO
ALTER TABLE [dbo].[BasicCurrencyList] ADD  CONSTRAINT [DF_CurrencyList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BasicCurrencyList] ADD  CONSTRAINT [DF_CurrencyList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BasicCurrencyList] ADD  CONSTRAINT [DF_CurrencyList_Default]  DEFAULT ((0)) FOR [Default]
GO
ALTER TABLE [dbo].[BasicImageGalleryList] ADD  CONSTRAINT [DF_ImageGalleryList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BasicItemList] ADD  CONSTRAINT [DF_ItemList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BasicItemList] ADD  CONSTRAINT [DF_ItemList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BasicUnitList] ADD  CONSTRAINT [DF_UnitList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BasicUnitList] ADD  CONSTRAINT [DF_UnitList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BasicUnitList] ADD  CONSTRAINT [DF_UnitList_Default]  DEFAULT ((0)) FOR [Default]
GO
ALTER TABLE [dbo].[BasicVatList] ADD  CONSTRAINT [DF_VatList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BasicVatList] ADD  CONSTRAINT [DF_VatList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessAddressList] ADD  CONSTRAINT [DF_AddressList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessAddressList] ADD  CONSTRAINT [DF_AddressList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessBranchList] ADD  CONSTRAINT [DF_BranchList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessBranchList] ADD  CONSTRAINT [DF_BranchList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessCreditNoteList] ADD  CONSTRAINT [DF_CreditNoteList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessCreditNoteSupportList] ADD  CONSTRAINT [DF_CreditNoteItemList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessExchangeRateList] ADD  CONSTRAINT [DF_CourseList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessExchangeRateList] ADD  CONSTRAINT [DF_CourseList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList] ADD  CONSTRAINT [DF_IncomingInvoiceList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceSupportList] ADD  CONSTRAINT [DF_IncomingInvoiceItemList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessIncomingOrderList] ADD  CONSTRAINT [DF_IncomingOrderList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessIncomingOrderSupportList] ADD  CONSTRAINT [DF_IncomingOrderItemList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessMaturityList] ADD  CONSTRAINT [DF_MaturityList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessMaturityList] ADD  CONSTRAINT [DF_MaturityList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessNotesList] ADD  CONSTRAINT [DF_NotesList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessNotesList] ADD  CONSTRAINT [DF_NotesList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessOfferList] ADD  CONSTRAINT [DF_OfferList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessOfferSupportList] ADD  CONSTRAINT [DF_OfferItemList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList] ADD  CONSTRAINT [DF_OutgoingInvoiceList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceSupportList] ADD  CONSTRAINT [DF_OutgoingInvoiceItemList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderList] ADD  CONSTRAINT [DF_OutgoingOrderList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderSupportList] ADD  CONSTRAINT [DF_OutgoingOrderItemList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessPaymentMethodList] ADD  CONSTRAINT [DF_PaymentMethodList_AutoGenerateReceipt]  DEFAULT ((0)) FOR [AutoGenerateReceipt]
GO
ALTER TABLE [dbo].[BusinessPaymentMethodList] ADD  CONSTRAINT [DF_PaymentMethodList_AllowGenerateReceipt]  DEFAULT ((0)) FOR [AllowGenerateReceipt]
GO
ALTER TABLE [dbo].[BusinessPaymentMethodList] ADD  CONSTRAINT [DF_PaymentMethodList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessPaymentMethodList] ADD  CONSTRAINT [DF_PaymentMethodList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessPaymentStatusList] ADD  CONSTRAINT [DF_PaymentStatusList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessPaymentStatusList] ADD  CONSTRAINT [DF_PaymentStatusList_Receipt]  DEFAULT ((0)) FOR [Receipt]
GO
ALTER TABLE [dbo].[BusinessPaymentStatusList] ADD  CONSTRAINT [DF_PaymentStatusList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessReceiptList] ADD  CONSTRAINT [DF_ReceiptList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessReceiptSupportList] ADD  CONSTRAINT [DF_ReceiptItemList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BusinessWarehouseList] ADD  CONSTRAINT [DF_WarehouseList_IsLocked]  DEFAULT ((0)) FOR [LockedByStockTaking]
GO
ALTER TABLE [dbo].[BusinessWarehouseList] ADD  CONSTRAINT [DF_WarehouseList_LastStockTaking]  DEFAULT (getdate()) FOR [LastStockTaking]
GO
ALTER TABLE [dbo].[BusinessWarehouseList] ADD  CONSTRAINT [DF_WarehouseList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BusinessWarehouseList] ADD  CONSTRAINT [DF_WarehouseList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[DocSrvDocTemplateList] ADD  CONSTRAINT [DF_DocSrvDocTemplateList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[DocSrvDocumentationCodeLibraryList] ADD  CONSTRAINT [DF_DocumentationCodeLibraryList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[DocSrvDocumentationGroupList] ADD  CONSTRAINT [DF_DocumentationGroupList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[DocSrvDocumentationGroupList] ADD  CONSTRAINT [DF_DocumentationGroupList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[DocSrvDocumentationList] ADD  CONSTRAINT [DF_DocumentationList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[DocSrvDocumentationList] ADD  CONSTRAINT [DF_DocumentationList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[GlobalEmailTemplateList] ADD  CONSTRAINT [DF_EmailTemplateList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[GlobalLanguageList] ADD  CONSTRAINT [DF_ServerLanguageList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[GlobalMottoList] ADD  CONSTRAINT [DF_MottoList_UserId]  DEFAULT ((1)) FOR [UserId]
GO
ALTER TABLE [dbo].[GlobalMottoList] ADD  CONSTRAINT [DF_MottoList_Timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[GlobalUserList] ADD  CONSTRAINT [DF_UserList_active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[GlobalUserList] ADD  CONSTRAINT [DF_userList_timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[GlobalUserRoleList] ADD  CONSTRAINT [DF_UserRoleList_timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[LicSrvLicenseActivationFailList] ADD  CONSTRAINT [DF_LicenceActivationFailList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[LicSrvLicenseAlgorithmList] ADD  CONSTRAINT [DF_LicSrvLicenseAlgorithmList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[LicSrvUsedLicenseList] ADD  CONSTRAINT [DF_UsedLicenceList_ActivateDate]  DEFAULT (getdate()) FOR [ActivateDate]
GO
ALTER TABLE [dbo].[PortalMessagesList] ADD  CONSTRAINT [DF_WebMessagesList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[PortalMessagesList] ADD  CONSTRAINT [DF_WebMessagesList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[ProviderAutoGenServerReqList] ADD  CONSTRAINT [DF_ProviderAutoGenServerCreateRequest_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[ProviderGeneratedLicenseList] ADD  CONSTRAINT [DF_ProviderGeneratedLicenseList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[ProviderGeneratedToolList] ADD  CONSTRAINT [DF_GeneratedToolList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[ServerBrowsablePathList] ADD  CONSTRAINT [DF_ServerBrowsablePathList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[ServerHealthCheckTaskList] ADD  CONSTRAINT [DF_HealthCheckTaskList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ServerHealthCheckTaskList] ADD  CONSTRAINT [DF_HealthCheckTaskList_Timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[ServerLiveDataMonitorList] ADD  CONSTRAINT [DF_ServerLiveDataMonitorList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ServerLiveDataMonitorList] ADD  CONSTRAINT [DF_ServerLiveDataMonitorList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[ServerSettingList] ADD  CONSTRAINT [DF_AdminConfiguration_CreateDate]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[ServerSettingList] ADD  CONSTRAINT [DF_AdminConfiguration_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ServerToolPanelDefinitionList] ADD  CONSTRAINT [DF_ServerToolPanelDefinitionList_TimeStamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[ServerToolTypeList] ADD  CONSTRAINT [DF_ServerToolTypeList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemCustomPageList] ADD  CONSTRAINT [DF_SystemCustomPageList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SystemCustomPageList] ADD  CONSTRAINT [DF_SystemCustomPageList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemDocumentAdviceList] ADD  CONSTRAINT [DF_DocumentAdvice_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SystemDocumentAdviceList] ADD  CONSTRAINT [DF_DocumentAdvice_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemDocumentTypeList] ADD  CONSTRAINT [DF_DocumentTypeList_SystemName]  DEFAULT ('MustProgramming') FOR [SystemName]
GO
ALTER TABLE [dbo].[SystemDocumentTypeList] ADD  CONSTRAINT [DF_DocumentTypeList_Timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[SystemFailList] ADD  CONSTRAINT [DF_SystemFailList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemGroupMenuList] ADD  CONSTRAINT [DF_SystemGroupMenuList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SystemGroupMenuList] ADD  CONSTRAINT [DF_SystemGroupMenuList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemIgnoredExceptionList] ADD  CONSTRAINT [DF_IgnoredExceptionList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SystemIgnoredExceptionList] ADD  CONSTRAINT [DF_IgnoredExceptionList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemLoginHistoryList] ADD  CONSTRAINT [DF_LoginHistoryList_UserId]  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[SystemLoginHistoryList] ADD  CONSTRAINT [DF_LoginHistory_timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[SystemMenuList] ADD  CONSTRAINT [DF_SystemMenuList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SystemMenuList] ADD  CONSTRAINT [DF_SystemMenuList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemParameterList] ADD  CONSTRAINT [DF_ParameterList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemReportList] ADD  CONSTRAINT [DF_ReportList_JoinedId]  DEFAULT ((0)) FOR [JoinedId]
GO
ALTER TABLE [dbo].[SystemReportList] ADD  CONSTRAINT [DF_ReportList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SystemReportList] ADD  CONSTRAINT [DF_ReportList_TimeStamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[SystemReportList] ADD  CONSTRAINT [DF_ReportList_Default]  DEFAULT ((0)) FOR [Default]
GO
ALTER TABLE [dbo].[SystemReportQueueList] ADD  CONSTRAINT [DF_ReportQueue_SearchFilterIgnore]  DEFAULT ((0)) FOR [SearchFilterIgnore]
GO
ALTER TABLE [dbo].[SystemReportQueueList] ADD  CONSTRAINT [DF_ReportQueue_RecIdFilterIgnore]  DEFAULT ((0)) FOR [RecIdFilterIgnore]
GO
ALTER TABLE [dbo].[SystemReportQueueList] ADD  CONSTRAINT [DF_ReportQueueList_Timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[SystemSvgIconList] ADD  CONSTRAINT [DF_SvgIconList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[SystemTranslationList] ADD  CONSTRAINT [DF_SystemTranslationList_UserId]  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[SystemTranslationList] ADD  CONSTRAINT [DF_SystemTranslationList_Timestamp]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[TemplateList] ADD  CONSTRAINT [DF_TemplateList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[TemplateList] ADD  CONSTRAINT [DF_TemplateList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[WebBannedIpAddressList] ADD  CONSTRAINT [DF_WebBannedUserList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[WebBannedIpAddressList] ADD  CONSTRAINT [DF_WebBannedUserList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[WebCodeLibraryList] ADD  CONSTRAINT [DF_WebCodeLibraryList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[WebCoreFileList] ADD  CONSTRAINT [DF_WebCoreFileList_MetroPath]  DEFAULT ('') FOR [MetroPath]
GO
ALTER TABLE [dbo].[WebCoreFileList] ADD  CONSTRAINT [DF_WebCoreFileList_MinifyWaiting]  DEFAULT ((0)) FOR [AutoMinifyWaiting]
GO
ALTER TABLE [dbo].[WebCoreFileList] ADD  CONSTRAINT [DF_WebCoreFileList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[WebDeveloperNewsList] ADD  CONSTRAINT [DF_WebDeveloperNewsList_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[WebDeveloperNewsList] ADD  CONSTRAINT [DF_WebDeveloperNewsList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[WebGroupMenuList] ADD  CONSTRAINT [DF_WebGroupMenuList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[WebMenuList] ADD  CONSTRAINT [DF_WebMenuList_UserRestriction]  DEFAULT ((0)) FOR [UserMenu]
GO
ALTER TABLE [dbo].[WebMenuList] ADD  CONSTRAINT [DF_WebMenuList_AdminMenu]  DEFAULT ((0)) FOR [AdminMenu]
GO
ALTER TABLE [dbo].[WebMenuList] ADD  CONSTRAINT [DF_WebMenuList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[WebSettingList] ADD  CONSTRAINT [DF_WebSettingList_CreateDate]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[WebUserSettingList] ADD  CONSTRAINT [DF_WebUserSettingList_CreateDate]  DEFAULT (getdate()) FOR [Timestamp]
GO
ALTER TABLE [dbo].[WebVisitIpList] ADD  CONSTRAINT [DF_WebVisitIpList_TimeStamp]  DEFAULT (getdate()) FOR [TimeStamp]
GO
ALTER TABLE [dbo].[BasicAttachmentList]  WITH CHECK ADD  CONSTRAINT [FK_AttachmentList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BasicAttachmentList] CHECK CONSTRAINT [FK_AttachmentList_UserList]
GO
ALTER TABLE [dbo].[BasicCalendarList]  WITH CHECK ADD  CONSTRAINT [FK_Calendar_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BasicCalendarList] CHECK CONSTRAINT [FK_Calendar_UserList]
GO
ALTER TABLE [dbo].[BasicCurrencyList]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BasicCurrencyList] CHECK CONSTRAINT [FK_CurrencyList_UserList]
GO
ALTER TABLE [dbo].[BasicImageGalleryList]  WITH CHECK ADD  CONSTRAINT [FK_ImageGalleryList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BasicImageGalleryList] CHECK CONSTRAINT [FK_ImageGalleryList_UserList]
GO
ALTER TABLE [dbo].[BasicItemList]  WITH CHECK ADD  CONSTRAINT [FK_ItemList_CurrencyList] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BasicItemList] CHECK CONSTRAINT [FK_ItemList_CurrencyList]
GO
ALTER TABLE [dbo].[BasicItemList]  WITH CHECK ADD  CONSTRAINT [FK_ItemList_UnitList] FOREIGN KEY([Unit])
REFERENCES [dbo].[BasicUnitList] ([Name])
GO
ALTER TABLE [dbo].[BasicItemList] CHECK CONSTRAINT [FK_ItemList_UnitList]
GO
ALTER TABLE [dbo].[BasicItemList]  WITH CHECK ADD  CONSTRAINT [FK_ItemList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BasicItemList] CHECK CONSTRAINT [FK_ItemList_UserList]
GO
ALTER TABLE [dbo].[BasicItemList]  WITH CHECK ADD  CONSTRAINT [FK_ItemList_VatList] FOREIGN KEY([VatId])
REFERENCES [dbo].[BasicVatList] ([Id])
GO
ALTER TABLE [dbo].[BasicItemList] CHECK CONSTRAINT [FK_ItemList_VatList]
GO
ALTER TABLE [dbo].[BasicUnitList]  WITH CHECK ADD  CONSTRAINT [FK_UnitList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BasicUnitList] CHECK CONSTRAINT [FK_UnitList_UserList]
GO
ALTER TABLE [dbo].[BasicVatList]  WITH CHECK ADD  CONSTRAINT [FK_VatList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BasicVatList] CHECK CONSTRAINT [FK_VatList_UserList]
GO
ALTER TABLE [dbo].[BusinessAddressList]  WITH CHECK ADD  CONSTRAINT [FK_AddressList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessAddressList] CHECK CONSTRAINT [FK_AddressList_UserList]
GO
ALTER TABLE [dbo].[BusinessBranchList]  WITH CHECK ADD  CONSTRAINT [FK_BranchList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessBranchList] CHECK CONSTRAINT [FK_BranchList_UserList]
GO
ALTER TABLE [dbo].[BusinessCreditNoteList]  WITH CHECK ADD  CONSTRAINT [FK_CreditNoteList_CurrencyList] FOREIGN KEY([TotalCurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BusinessCreditNoteList] CHECK CONSTRAINT [FK_CreditNoteList_CurrencyList]
GO
ALTER TABLE [dbo].[BusinessCreditNoteList]  WITH CHECK ADD  CONSTRAINT [FK_CreditNoteList_OutgoingInvoiceList] FOREIGN KEY([InvoiceNumber])
REFERENCES [dbo].[BusinessOutgoingInvoiceList] ([DocumentNumber])
GO
ALTER TABLE [dbo].[BusinessCreditNoteList] CHECK CONSTRAINT [FK_CreditNoteList_OutgoingInvoiceList]
GO
ALTER TABLE [dbo].[BusinessCreditNoteList]  WITH CHECK ADD  CONSTRAINT [FK_CreditNoteList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessCreditNoteList] CHECK CONSTRAINT [FK_CreditNoteList_UserList]
GO
ALTER TABLE [dbo].[BusinessCreditNoteSupportList]  WITH CHECK ADD  CONSTRAINT [FK_CreditNoteItemList_CreditNoteList] FOREIGN KEY([DocumentNumber])
REFERENCES [dbo].[BusinessCreditNoteList] ([DocumentNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusinessCreditNoteSupportList] CHECK CONSTRAINT [FK_CreditNoteItemList_CreditNoteList]
GO
ALTER TABLE [dbo].[BusinessCreditNoteSupportList]  WITH CHECK ADD  CONSTRAINT [FK_CreditNoteItemList_UnitList] FOREIGN KEY([Unit])
REFERENCES [dbo].[BasicUnitList] ([Name])
GO
ALTER TABLE [dbo].[BusinessCreditNoteSupportList] CHECK CONSTRAINT [FK_CreditNoteItemList_UnitList]
GO
ALTER TABLE [dbo].[BusinessCreditNoteSupportList]  WITH CHECK ADD  CONSTRAINT [FK_CreditNoteItemList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessCreditNoteSupportList] CHECK CONSTRAINT [FK_CreditNoteItemList_UserList]
GO
ALTER TABLE [dbo].[BusinessExchangeRateList]  WITH CHECK ADD  CONSTRAINT [FK_CourseList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessExchangeRateList] CHECK CONSTRAINT [FK_CourseList_UserList]
GO
ALTER TABLE [dbo].[BusinessExchangeRateList]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeRateList_CurrencyList] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BusinessExchangeRateList] CHECK CONSTRAINT [FK_ExchangeRateList_CurrencyList]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingInvoiceList_CurrencyList] FOREIGN KEY([TotalCurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList] CHECK CONSTRAINT [FK_IncomingInvoiceList_CurrencyList]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingInvoiceList_MaturityList] FOREIGN KEY([MaturityId])
REFERENCES [dbo].[BusinessMaturityList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList] CHECK CONSTRAINT [FK_IncomingInvoiceList_MaturityList]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingInvoiceList_PaymentMethodList] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[BusinessPaymentMethodList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList] CHECK CONSTRAINT [FK_IncomingInvoiceList_PaymentMethodList]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingInvoiceList_PaymentStatusList] FOREIGN KEY([PaymentStatusId])
REFERENCES [dbo].[BusinessPaymentStatusList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList] CHECK CONSTRAINT [FK_IncomingInvoiceList_PaymentStatusList]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingInvoiceList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceList] CHECK CONSTRAINT [FK_IncomingInvoiceList_UserList]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceSupportList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingInvoiceItemList_IncomingInvoiceList] FOREIGN KEY([DocumentNumber])
REFERENCES [dbo].[BusinessIncomingInvoiceList] ([DocumentNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceSupportList] CHECK CONSTRAINT [FK_IncomingInvoiceItemList_IncomingInvoiceList]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceSupportList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingInvoiceItemList_UnitList] FOREIGN KEY([Unit])
REFERENCES [dbo].[BasicUnitList] ([Name])
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceSupportList] CHECK CONSTRAINT [FK_IncomingInvoiceItemList_UnitList]
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceSupportList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingInvoiceItemList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingInvoiceSupportList] CHECK CONSTRAINT [FK_IncomingInvoiceItemList_UserList]
GO
ALTER TABLE [dbo].[BusinessIncomingOrderList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingOrderList_CurrencyList] FOREIGN KEY([TotalCurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingOrderList] CHECK CONSTRAINT [FK_IncomingOrderList_CurrencyList]
GO
ALTER TABLE [dbo].[BusinessIncomingOrderList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingOrderList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingOrderList] CHECK CONSTRAINT [FK_IncomingOrderList_UserList]
GO
ALTER TABLE [dbo].[BusinessIncomingOrderSupportList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingOrderItemList_IncomingOrderList] FOREIGN KEY([DocumentNumber])
REFERENCES [dbo].[BusinessIncomingOrderList] ([DocumentNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusinessIncomingOrderSupportList] CHECK CONSTRAINT [FK_IncomingOrderItemList_IncomingOrderList]
GO
ALTER TABLE [dbo].[BusinessIncomingOrderSupportList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingOrderItemList_UnitList] FOREIGN KEY([Unit])
REFERENCES [dbo].[BasicUnitList] ([Name])
GO
ALTER TABLE [dbo].[BusinessIncomingOrderSupportList] CHECK CONSTRAINT [FK_IncomingOrderItemList_UnitList]
GO
ALTER TABLE [dbo].[BusinessIncomingOrderSupportList]  WITH CHECK ADD  CONSTRAINT [FK_IncomingOrderItemList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessIncomingOrderSupportList] CHECK CONSTRAINT [FK_IncomingOrderItemList_UserList]
GO
ALTER TABLE [dbo].[BusinessMaturityList]  WITH CHECK ADD  CONSTRAINT [FK_MaturityList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessMaturityList] CHECK CONSTRAINT [FK_MaturityList_UserList]
GO
ALTER TABLE [dbo].[BusinessNotesList]  WITH CHECK ADD  CONSTRAINT [FK_NotesList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessNotesList] CHECK CONSTRAINT [FK_NotesList_UserList]
GO
ALTER TABLE [dbo].[BusinessOfferList]  WITH CHECK ADD  CONSTRAINT [FK_OfferList_CurrencyList1] FOREIGN KEY([TotalCurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOfferList] CHECK CONSTRAINT [FK_OfferList_CurrencyList1]
GO
ALTER TABLE [dbo].[BusinessOfferList]  WITH CHECK ADD  CONSTRAINT [FK_OfferList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOfferList] CHECK CONSTRAINT [FK_OfferList_UserList]
GO
ALTER TABLE [dbo].[BusinessOfferSupportList]  WITH CHECK ADD  CONSTRAINT [FK_OfferItemList_OfferList] FOREIGN KEY([DocumentNumber])
REFERENCES [dbo].[BusinessOfferList] ([DocumentNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusinessOfferSupportList] CHECK CONSTRAINT [FK_OfferItemList_OfferList]
GO
ALTER TABLE [dbo].[BusinessOfferSupportList]  WITH CHECK ADD  CONSTRAINT [FK_OfferItemList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOfferSupportList] CHECK CONSTRAINT [FK_OfferItemList_UserList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceList_CreditNoteList] FOREIGN KEY([CreditNoteId])
REFERENCES [dbo].[BusinessCreditNoteList] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList] CHECK CONSTRAINT [FK_OutgoingInvoiceList_CreditNoteList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceList_CurrencyList] FOREIGN KEY([TotalCurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList] CHECK CONSTRAINT [FK_OutgoingInvoiceList_CurrencyList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceList_MaturityList] FOREIGN KEY([MaturityId])
REFERENCES [dbo].[BusinessMaturityList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList] CHECK CONSTRAINT [FK_OutgoingInvoiceList_MaturityList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceList_PaymentMethodList] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[BusinessPaymentMethodList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList] CHECK CONSTRAINT [FK_OutgoingInvoiceList_PaymentMethodList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceList_PaymentStatusList] FOREIGN KEY([PaymentStatusId])
REFERENCES [dbo].[BusinessPaymentStatusList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList] CHECK CONSTRAINT [FK_OutgoingInvoiceList_PaymentStatusList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceList_ReceiptList] FOREIGN KEY([ReceiptId])
REFERENCES [dbo].[BusinessReceiptList] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList] CHECK CONSTRAINT [FK_OutgoingInvoiceList_ReceiptList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceList] CHECK CONSTRAINT [FK_OutgoingInvoiceList_UserList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceSupportList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceItemList_OutgoingInvoiceList] FOREIGN KEY([DocumentNumber])
REFERENCES [dbo].[BusinessOutgoingInvoiceList] ([DocumentNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceSupportList] CHECK CONSTRAINT [FK_OutgoingInvoiceItemList_OutgoingInvoiceList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceSupportList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceItemList_UnitList] FOREIGN KEY([Unit])
REFERENCES [dbo].[BasicUnitList] ([Name])
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceSupportList] CHECK CONSTRAINT [FK_OutgoingInvoiceItemList_UnitList]
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceSupportList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingInvoiceItemList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingInvoiceSupportList] CHECK CONSTRAINT [FK_OutgoingInvoiceItemList_UserList]
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingOrderList_CurrencyList] FOREIGN KEY([TotalCurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderList] CHECK CONSTRAINT [FK_OutgoingOrderList_CurrencyList]
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingOrderList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderList] CHECK CONSTRAINT [FK_OutgoingOrderList_UserList]
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderSupportList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingOrderItemList_OutgoingOrderList] FOREIGN KEY([DocumentNumber])
REFERENCES [dbo].[BusinessOutgoingOrderList] ([DocumentNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderSupportList] CHECK CONSTRAINT [FK_OutgoingOrderItemList_OutgoingOrderList]
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderSupportList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingOrderItemList_UnitList] FOREIGN KEY([Unit])
REFERENCES [dbo].[BasicUnitList] ([Name])
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderSupportList] CHECK CONSTRAINT [FK_OutgoingOrderItemList_UnitList]
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderSupportList]  WITH CHECK ADD  CONSTRAINT [FK_OutgoingOrderItemList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessOutgoingOrderSupportList] CHECK CONSTRAINT [FK_OutgoingOrderItemList_UserList]
GO
ALTER TABLE [dbo].[BusinessPaymentMethodList]  WITH CHECK ADD  CONSTRAINT [FK_PaymentMethodList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessPaymentMethodList] CHECK CONSTRAINT [FK_PaymentMethodList_UserList]
GO
ALTER TABLE [dbo].[BusinessPaymentStatusList]  WITH CHECK ADD  CONSTRAINT [FK_PaymentStatusList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessPaymentStatusList] CHECK CONSTRAINT [FK_PaymentStatusList_UserList]
GO
ALTER TABLE [dbo].[BusinessReceiptList]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptList_CurrencyList] FOREIGN KEY([TotalCurrencyId])
REFERENCES [dbo].[BasicCurrencyList] ([Id])
GO
ALTER TABLE [dbo].[BusinessReceiptList] CHECK CONSTRAINT [FK_ReceiptList_CurrencyList]
GO
ALTER TABLE [dbo].[BusinessReceiptList]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptList_OutgoingInvoiceList] FOREIGN KEY([InvoiceNumber])
REFERENCES [dbo].[BusinessOutgoingInvoiceList] ([DocumentNumber])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[BusinessReceiptList] CHECK CONSTRAINT [FK_ReceiptList_OutgoingInvoiceList]
GO
ALTER TABLE [dbo].[BusinessReceiptList]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessReceiptList] CHECK CONSTRAINT [FK_ReceiptList_UserList]
GO
ALTER TABLE [dbo].[BusinessReceiptSupportList]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptItemList_ReceiptList] FOREIGN KEY([DocumentNumber])
REFERENCES [dbo].[BusinessReceiptList] ([DocumentNumber])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BusinessReceiptSupportList] CHECK CONSTRAINT [FK_ReceiptItemList_ReceiptList]
GO
ALTER TABLE [dbo].[BusinessReceiptSupportList]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptItemList_UnitList] FOREIGN KEY([Unit])
REFERENCES [dbo].[BasicUnitList] ([Name])
GO
ALTER TABLE [dbo].[BusinessReceiptSupportList] CHECK CONSTRAINT [FK_ReceiptItemList_UnitList]
GO
ALTER TABLE [dbo].[BusinessReceiptSupportList]  WITH CHECK ADD  CONSTRAINT [FK_ReceiptItemList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessReceiptSupportList] CHECK CONSTRAINT [FK_ReceiptItemList_UserList]
GO
ALTER TABLE [dbo].[BusinessWarehouseList]  WITH CHECK ADD  CONSTRAINT [FK_WarehouseList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[BusinessWarehouseList] CHECK CONSTRAINT [FK_WarehouseList_UserList]
GO
ALTER TABLE [dbo].[DocSrvDocTemplateList]  WITH CHECK ADD  CONSTRAINT [FK_DocSrvDocTemplateList_DocSrvDocumentationGroupList] FOREIGN KEY([GroupId])
REFERENCES [dbo].[DocSrvDocumentationGroupList] ([Id])
GO
ALTER TABLE [dbo].[DocSrvDocTemplateList] CHECK CONSTRAINT [FK_DocSrvDocTemplateList_DocSrvDocumentationGroupList]
GO
ALTER TABLE [dbo].[DocSrvDocTemplateList]  WITH CHECK ADD  CONSTRAINT [FK_DocSrvDocTemplateList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[DocSrvDocTemplateList] CHECK CONSTRAINT [FK_DocSrvDocTemplateList_UserList]
GO
ALTER TABLE [dbo].[DocSrvDocumentationCodeLibraryList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentationCodeLibraryList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[DocSrvDocumentationCodeLibraryList] CHECK CONSTRAINT [FK_DocumentationCodeLibraryList_UserList]
GO
ALTER TABLE [dbo].[DocSrvDocumentationGroupList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentationGroupList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[DocSrvDocumentationGroupList] CHECK CONSTRAINT [FK_DocumentationGroupList_UserList]
GO
ALTER TABLE [dbo].[DocSrvDocumentationList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentationList_DocumentationGroupList] FOREIGN KEY([DocumentationGroupId])
REFERENCES [dbo].[DocSrvDocumentationGroupList] ([Id])
GO
ALTER TABLE [dbo].[DocSrvDocumentationList] CHECK CONSTRAINT [FK_DocumentationList_DocumentationGroupList]
GO
ALTER TABLE [dbo].[DocSrvDocumentationList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentationList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[DocSrvDocumentationList] CHECK CONSTRAINT [FK_DocumentationList_UserList]
GO
ALTER TABLE [dbo].[GlobalEmailTemplateList]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplateList_SystemLanguageList] FOREIGN KEY([SystemLanguageId])
REFERENCES [dbo].[GlobalLanguageList] ([Id])
GO
ALTER TABLE [dbo].[GlobalEmailTemplateList] CHECK CONSTRAINT [FK_EmailTemplateList_SystemLanguageList]
GO
ALTER TABLE [dbo].[GlobalEmailTemplateList]  WITH CHECK ADD  CONSTRAINT [FK_EmailTemplateList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[GlobalEmailTemplateList] CHECK CONSTRAINT [FK_EmailTemplateList_UserList]
GO
ALTER TABLE [dbo].[GlobalLanguageList]  WITH CHECK ADD  CONSTRAINT [FK_ServerLanguageList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[GlobalLanguageList] CHECK CONSTRAINT [FK_ServerLanguageList_UserList]
GO
ALTER TABLE [dbo].[GlobalMottoList]  WITH CHECK ADD  CONSTRAINT [FK_MottoList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[GlobalMottoList] CHECK CONSTRAINT [FK_MottoList_UserList]
GO
ALTER TABLE [dbo].[GlobalUserList]  WITH CHECK ADD  CONSTRAINT [FK_UserList_UserList] FOREIGN KEY([Id])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[GlobalUserList] CHECK CONSTRAINT [FK_UserList_UserList]
GO
ALTER TABLE [dbo].[GlobalUserList]  WITH CHECK ADD  CONSTRAINT [FK_UserList_UserRoleList] FOREIGN KEY([RoleId])
REFERENCES [dbo].[GlobalUserRoleList] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GlobalUserList] CHECK CONSTRAINT [FK_UserList_UserRoleList]
GO
ALTER TABLE [dbo].[LicSrvLicenseAlgorithmList]  WITH CHECK ADD  CONSTRAINT [FK_LicenseAlgorithmList_AddressList] FOREIGN KEY([AddressId])
REFERENCES [dbo].[BusinessAddressList] ([Id])
GO
ALTER TABLE [dbo].[LicSrvLicenseAlgorithmList] CHECK CONSTRAINT [FK_LicenseAlgorithmList_AddressList]
GO
ALTER TABLE [dbo].[LicSrvLicenseAlgorithmList]  WITH CHECK ADD  CONSTRAINT [FK_LicenseAlgorithmList_ItemList] FOREIGN KEY([ItemId])
REFERENCES [dbo].[BasicItemList] ([Id])
GO
ALTER TABLE [dbo].[LicSrvLicenseAlgorithmList] CHECK CONSTRAINT [FK_LicenseAlgorithmList_ItemList]
GO
ALTER TABLE [dbo].[LicSrvLicenseAlgorithmList]  WITH CHECK ADD  CONSTRAINT [FK_LicenseAlgorithmList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[LicSrvLicenseAlgorithmList] CHECK CONSTRAINT [FK_LicenseAlgorithmList_UserList]
GO
ALTER TABLE [dbo].[LicSrvUsedLicenseList]  WITH CHECK ADD  CONSTRAINT [FK_UsedLicenseList_AddressList] FOREIGN KEY([AddressId])
REFERENCES [dbo].[BusinessAddressList] ([Id])
GO
ALTER TABLE [dbo].[LicSrvUsedLicenseList] CHECK CONSTRAINT [FK_UsedLicenseList_AddressList]
GO
ALTER TABLE [dbo].[LicSrvUsedLicenseList]  WITH CHECK ADD  CONSTRAINT [FK_UsedLicenseList_ItemList] FOREIGN KEY([ItemId])
REFERENCES [dbo].[BasicItemList] ([Id])
GO
ALTER TABLE [dbo].[LicSrvUsedLicenseList] CHECK CONSTRAINT [FK_UsedLicenseList_ItemList]
GO
ALTER TABLE [dbo].[PortalMessagesList]  WITH CHECK ADD  CONSTRAINT [FK_WebMessagesList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[PortalMessagesList] CHECK CONSTRAINT [FK_WebMessagesList_UserList]
GO
ALTER TABLE [dbo].[ProdGuidGroupList]  WITH CHECK ADD  CONSTRAINT [FK_ProdGuidGroupList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ProdGuidGroupList] CHECK CONSTRAINT [FK_ProdGuidGroupList_UserList]
GO
ALTER TABLE [dbo].[ProdGuidOperationList]  WITH CHECK ADD  CONSTRAINT [FK_ProdGuidOperationList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ProdGuidOperationList] CHECK CONSTRAINT [FK_ProdGuidOperationList_UserList]
GO
ALTER TABLE [dbo].[ProdGuidPartList]  WITH CHECK ADD  CONSTRAINT [FK_ProdGuidPartList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ProdGuidPartList] CHECK CONSTRAINT [FK_ProdGuidPartList_UserList]
GO
ALTER TABLE [dbo].[ProdGuidPersonList]  WITH CHECK ADD  CONSTRAINT [FK_ProdGuidPersonList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ProdGuidPersonList] CHECK CONSTRAINT [FK_ProdGuidPersonList_UserList]
GO
ALTER TABLE [dbo].[ProdGuidWorkList]  WITH CHECK ADD  CONSTRAINT [FK_ProdGuidWorkList_ProdGuidPersonList] FOREIGN KEY([PersonalNumber])
REFERENCES [dbo].[ProdGuidPersonList] ([PersonalNumber])
GO
ALTER TABLE [dbo].[ProdGuidWorkList] CHECK CONSTRAINT [FK_ProdGuidWorkList_ProdGuidPersonList]
GO
ALTER TABLE [dbo].[ProdGuidWorkList]  WITH CHECK ADD  CONSTRAINT [FK_ProdGuidWorkList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ProdGuidWorkList] CHECK CONSTRAINT [FK_ProdGuidWorkList_UserList]
GO
ALTER TABLE [dbo].[ProviderAutoGenServerReqList]  WITH CHECK ADD  CONSTRAINT [FK_ProviderAutoGenServerCreateRequest_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ProviderAutoGenServerReqList] CHECK CONSTRAINT [FK_ProviderAutoGenServerCreateRequest_UserList]
GO
ALTER TABLE [dbo].[ProviderGeneratedLicenseList]  WITH CHECK ADD  CONSTRAINT [FK_ProviderGeneratedLicenseList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ProviderGeneratedLicenseList] CHECK CONSTRAINT [FK_ProviderGeneratedLicenseList_UserList]
GO
ALTER TABLE [dbo].[ProviderGeneratedToolList]  WITH CHECK ADD  CONSTRAINT [FK_GeneratedToolList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ProviderGeneratedToolList] CHECK CONSTRAINT [FK_GeneratedToolList_UserList]
GO
ALTER TABLE [dbo].[ServerBrowsablePathList]  WITH CHECK ADD  CONSTRAINT [FK_ServerBrowsablePathList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ServerBrowsablePathList] CHECK CONSTRAINT [FK_ServerBrowsablePathList_UserList]
GO
ALTER TABLE [dbo].[ServerHealthCheckTaskList]  WITH CHECK ADD  CONSTRAINT [FK_HealthCheckTaskList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ServerHealthCheckTaskList] CHECK CONSTRAINT [FK_HealthCheckTaskList_UserList]
GO
ALTER TABLE [dbo].[ServerLiveDataMonitorList]  WITH CHECK ADD  CONSTRAINT [FK_ServerLiveDataMonitorList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ServerLiveDataMonitorList] CHECK CONSTRAINT [FK_ServerLiveDataMonitorList_UserList]
GO
ALTER TABLE [dbo].[ServerToolPanelDefinitionList]  WITH CHECK ADD  CONSTRAINT [FK_ServerToolPanelDefinitionList_ToolTypeList] FOREIGN KEY([ToolTypeId])
REFERENCES [dbo].[ServerToolTypeList] ([Id])
GO
ALTER TABLE [dbo].[ServerToolPanelDefinitionList] CHECK CONSTRAINT [FK_ServerToolPanelDefinitionList_ToolTypeList]
GO
ALTER TABLE [dbo].[ServerToolPanelDefinitionList]  WITH CHECK ADD  CONSTRAINT [FK_ServerToolPanelDefinitionList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ServerToolPanelDefinitionList] CHECK CONSTRAINT [FK_ServerToolPanelDefinitionList_UserList]
GO
ALTER TABLE [dbo].[ServerToolTypeList]  WITH CHECK ADD  CONSTRAINT [FK_ServerToolTypeList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[ServerToolTypeList] CHECK CONSTRAINT [FK_ServerToolTypeList_UserList]
GO
ALTER TABLE [dbo].[SystemCustomPageList]  WITH CHECK ADD  CONSTRAINT [FK_SystemCustomPageList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemCustomPageList] CHECK CONSTRAINT [FK_SystemCustomPageList_UserList]
GO
ALTER TABLE [dbo].[SystemDocumentAdviceList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentAdvice_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemDocumentAdviceList] CHECK CONSTRAINT [FK_DocumentAdvice_UserList]
GO
ALTER TABLE [dbo].[SystemDocumentAdviceList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentAdviceList_BranchList] FOREIGN KEY([BranchId])
REFERENCES [dbo].[BusinessBranchList] ([Id])
GO
ALTER TABLE [dbo].[SystemDocumentAdviceList] CHECK CONSTRAINT [FK_DocumentAdviceList_BranchList]
GO
ALTER TABLE [dbo].[SystemDocumentAdviceList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentAdviceList_DocumentTypeList] FOREIGN KEY([DocumentType])
REFERENCES [dbo].[SystemDocumentTypeList] ([SystemName])
GO
ALTER TABLE [dbo].[SystemDocumentAdviceList] CHECK CONSTRAINT [FK_DocumentAdviceList_DocumentTypeList]
GO
ALTER TABLE [dbo].[SystemDocumentTypeList]  WITH CHECK ADD  CONSTRAINT [FK_DocumentTypeList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemDocumentTypeList] CHECK CONSTRAINT [FK_DocumentTypeList_UserList]
GO
ALTER TABLE [dbo].[SystemFailList]  WITH CHECK ADD  CONSTRAINT [FK_SystemFailList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemFailList] CHECK CONSTRAINT [FK_SystemFailList_UserList]
GO
ALTER TABLE [dbo].[SystemGroupMenuList]  WITH CHECK ADD  CONSTRAINT [FK_SystemGroupMenuList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemGroupMenuList] CHECK CONSTRAINT [FK_SystemGroupMenuList_UserList]
GO
ALTER TABLE [dbo].[SystemIgnoredExceptionList]  WITH CHECK ADD  CONSTRAINT [FK_IgnoredExceptionList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemIgnoredExceptionList] CHECK CONSTRAINT [FK_IgnoredExceptionList_UserList]
GO
ALTER TABLE [dbo].[SystemMenuList]  WITH CHECK ADD  CONSTRAINT [FK_SystemMenuList_SystemGroupMenuList] FOREIGN KEY([GroupId])
REFERENCES [dbo].[SystemGroupMenuList] ([Id])
GO
ALTER TABLE [dbo].[SystemMenuList] CHECK CONSTRAINT [FK_SystemMenuList_SystemGroupMenuList]
GO
ALTER TABLE [dbo].[SystemMenuList]  WITH CHECK ADD  CONSTRAINT [FK_SystemMenuList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemMenuList] CHECK CONSTRAINT [FK_SystemMenuList_UserList]
GO
ALTER TABLE [dbo].[SystemParameterList]  WITH CHECK ADD  CONSTRAINT [FK_ParameterList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SystemParameterList] CHECK CONSTRAINT [FK_ParameterList_UserList]
GO
ALTER TABLE [dbo].[SystemReportList]  WITH CHECK ADD  CONSTRAINT [FK_ReportList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemReportList] CHECK CONSTRAINT [FK_ReportList_UserList]
GO
ALTER TABLE [dbo].[SystemSvgIconList]  WITH CHECK ADD  CONSTRAINT [FK_SvgIconList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemSvgIconList] CHECK CONSTRAINT [FK_SvgIconList_UserList]
GO
ALTER TABLE [dbo].[SystemTranslationList]  WITH CHECK ADD  CONSTRAINT [FK_SystemTranslationList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[SystemTranslationList] CHECK CONSTRAINT [FK_SystemTranslationList_UserList]
GO
ALTER TABLE [dbo].[TemplateList]  WITH CHECK ADD  CONSTRAINT [FK_TemplateList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[TemplateList] CHECK CONSTRAINT [FK_TemplateList_UserList]
GO
ALTER TABLE [dbo].[TemplateList]  WITH CHECK ADD  CONSTRAINT [FK_TemplateList_UserRoleList] FOREIGN KEY([GroupId])
REFERENCES [dbo].[GlobalUserRoleList] ([Id])
GO
ALTER TABLE [dbo].[TemplateList] CHECK CONSTRAINT [FK_TemplateList_UserRoleList]
GO
ALTER TABLE [dbo].[WebBannedIpAddressList]  WITH CHECK ADD  CONSTRAINT [FK_WebBannedUserList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[WebBannedIpAddressList] CHECK CONSTRAINT [FK_WebBannedUserList_UserList]
GO
ALTER TABLE [dbo].[WebCodeLibraryList]  WITH CHECK ADD  CONSTRAINT [FK_WebCodeLibraryList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[WebCodeLibraryList] CHECK CONSTRAINT [FK_WebCodeLibraryList_UserList]
GO
ALTER TABLE [dbo].[WebCoreFileList]  WITH CHECK ADD  CONSTRAINT [FK_WebCoreFileList_GlobalUserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[WebCoreFileList] CHECK CONSTRAINT [FK_WebCoreFileList_GlobalUserList]
GO
ALTER TABLE [dbo].[WebDeveloperNewsList]  WITH CHECK ADD  CONSTRAINT [FK_WebDeveloperNewsList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[WebDeveloperNewsList] CHECK CONSTRAINT [FK_WebDeveloperNewsList_UserList]
GO
ALTER TABLE [dbo].[WebGroupMenuList]  WITH CHECK ADD  CONSTRAINT [FK_WebGroupMenuList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[WebGroupMenuList] CHECK CONSTRAINT [FK_WebGroupMenuList_UserList]
GO
ALTER TABLE [dbo].[WebMenuList]  WITH CHECK ADD  CONSTRAINT [FK_WebMenuList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
GO
ALTER TABLE [dbo].[WebMenuList] CHECK CONSTRAINT [FK_WebMenuList_UserList]
GO
ALTER TABLE [dbo].[WebMenuList]  WITH CHECK ADD  CONSTRAINT [FK_WebMenuList_WebGroupMenuList] FOREIGN KEY([GroupId])
REFERENCES [dbo].[WebGroupMenuList] ([Id])
GO
ALTER TABLE [dbo].[WebMenuList] CHECK CONSTRAINT [FK_WebMenuList_WebGroupMenuList]
GO
ALTER TABLE [dbo].[WebSettingList]  WITH CHECK ADD  CONSTRAINT [FK_WebSettingList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebSettingList] CHECK CONSTRAINT [FK_WebSettingList_UserList]
GO
ALTER TABLE [dbo].[WebUserSettingList]  WITH CHECK ADD  CONSTRAINT [FK_WebUserSettingList_UserList] FOREIGN KEY([UserId])
REFERENCES [dbo].[GlobalUserList] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebUserSettingList] CHECK CONSTRAINT [FK_WebUserSettingList_UserList]
GO
/****** Object:  Trigger [dbo].[TR_CurrencyList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_CurrencyList] ON [dbo].[BasicCurrencyList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BasicCurrencyList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BasicCurrencyList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BasicCurrencyList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BasicCurrencyList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BasicCurrencyList] ENABLE TRIGGER [TR_CurrencyList]
GO
/****** Object:  Trigger [dbo].[TR_ImageGalleryList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE   TRIGGER [dbo].[TR_ImageGalleryList] ON [dbo].[BasicImageGalleryList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @isPrimary bit;DECLARE @RecId int;DECLARE @HotelId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @isPrimary = ins.[IsPrimary] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@isPrimary = 1) BEGIN
			UPDATE [dbo].BasicImageGalleryList SET [IsPrimary] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @isPrimary = ins.[IsPrimary] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@isPrimary = 1) BEGIN
				UPDATE [dbo].BasicImageGalleryList SET [IsPrimary] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @isPrimary = ins.[IsPrimary] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@isPrimary = 1) BEGIN
		UPDATE [dbo].BasicImageGalleryList SET [IsPrimary] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BasicImageGalleryList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BasicImageGalleryList] ENABLE TRIGGER [TR_ImageGalleryList]
GO
/****** Object:  Trigger [dbo].[TR_UnitList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   TRIGGER [dbo].[TR_UnitList] ON [dbo].[BasicUnitList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BasicUnitList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BasicUnitList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BasicUnitList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BasicUnitList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BasicUnitList] ENABLE TRIGGER [TR_UnitList]
GO
/****** Object:  Trigger [dbo].[TR_VatList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_VatList] ON [dbo].[BasicVatList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BasicVatList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BasicVatList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BasicVatList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BasicVatList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BasicVatList] ENABLE TRIGGER [TR_VatList]
GO
/****** Object:  Trigger [dbo].[TR_BranchList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   TRIGGER [dbo].[TR_BranchList] ON [dbo].[BusinessBranchList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setActive bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setActive = ins.[Active] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setActive = 1) BEGIN
			UPDATE [dbo].BusinessBranchList SET [Active] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setActive = ins.[Active] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setActive = 1) BEGIN
				UPDATE [dbo].BusinessBranchList SET [Active] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setActive = ins.[Active] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setActive = 1) BEGIN
		UPDATE [dbo].BusinessBranchList SET [Active] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessBranchList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BusinessBranchList] ENABLE TRIGGER [TR_BranchList]
GO
/****** Object:  Trigger [dbo].[TR_MaturityList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   TRIGGER [dbo].[TR_MaturityList] ON [dbo].[BusinessMaturityList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BusinessMaturityList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BusinessMaturityList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BusinessMaturityList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessMaturityList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BusinessMaturityList] ENABLE TRIGGER [TR_MaturityList]
GO
/****** Object:  Trigger [dbo].[TR_PaymentMethodList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_PaymentMethodList] ON [dbo].[BusinessPaymentMethodList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BusinessPaymentMethodList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BusinessPaymentMethodList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BusinessPaymentMethodList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessPaymentMethodList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BusinessPaymentMethodList] ENABLE TRIGGER [TR_PaymentMethodList]
GO
/****** Object:  Trigger [dbo].[TR_PaymentStatusList]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_PaymentStatusList] ON [dbo].[BusinessPaymentStatusList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BusinessPaymentStatusList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BusinessPaymentStatusList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BusinessPaymentStatusList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessPaymentStatusList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BusinessPaymentStatusList] ENABLE TRIGGER [TR_PaymentStatusList]
GO
/****** Object:  Trigger [dbo].[TR_PaymentStatusListCreditNote]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_PaymentStatusListCreditNote] ON [dbo].[BusinessPaymentStatusList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setCreditNote bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setCreditNote = ins.[CreditNote] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setCreditNote = 1) BEGIN
			UPDATE [dbo].BusinessPaymentStatusList SET [CreditNote] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setCreditNote = ins.[CreditNote] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setCreditNote = 1) BEGIN
				UPDATE [dbo].BusinessPaymentStatusList SET [CreditNote] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setCreditNote = ins.[CreditNote] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setCreditNote = 1) BEGIN
		UPDATE [dbo].BusinessPaymentStatusList SET [CreditNote] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessPaymentStatusList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BusinessPaymentStatusList] ENABLE TRIGGER [TR_PaymentStatusListCreditNote]
GO
/****** Object:  Trigger [dbo].[TR_PaymentStatusListReceipt]    Script Date: 07.12.2023 13:45:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   TRIGGER [dbo].[TR_PaymentStatusListReceipt] ON [dbo].[BusinessPaymentStatusList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setReceipt bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setReceipt = ins.[Receipt] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setReceipt = 1) BEGIN
			UPDATE [dbo].BusinessPaymentStatusList SET [Receipt] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setReceipt = ins.[Receipt] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setReceipt = 1) BEGIN
				UPDATE [dbo].BusinessPaymentStatusList SET [Receipt] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setReceipt = ins.[Receipt] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setReceipt = 1) BEGIN
		UPDATE [dbo].BusinessPaymentStatusList SET [Receipt] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessPaymentStatusList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BusinessPaymentStatusList] ENABLE TRIGGER [TR_PaymentStatusListReceipt]
GO
/****** Object:  Trigger [dbo].[TR_WarehouseList]    Script Date: 07.12.2023 13:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_WarehouseList] ON [dbo].[BusinessWarehouseList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].BusinessWarehouseList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].BusinessWarehouseList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].BusinessWarehouseList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].BusinessWarehouseList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[BusinessWarehouseList] ENABLE TRIGGER [TR_WarehouseList]
GO
/****** Object:  Trigger [dbo].[TR_DocumentationList]    Script Date: 07.12.2023 13:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_DocumentationList] ON [dbo].[DocSrvDocumentationList]
FOR INSERT, UPDATE--, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setActive bit;DECLARE @autoVersion int;DECLARE @RecId int;DECLARE @GroupId int;DECLARE @RecName varchar(150);
	DECLARE @autoRemoveOld bit; DECLARE @UserId int;
	

	SET @autoVersion = 0;SET @setActive = 1;SET @autoRemoveOld = 0;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setActive = ins.[Active] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;
		SELECT @UserId = ins.UserId from inserted ins;
		SELECT @GroupId = ins.DocumentationGroupId from inserted ins;
		SELECT @RecName = ins.[Name] from inserted ins;

		--GET AutoRemoveSetting
		SELECT @autoRemoveOld = CAST(CAST(SUBSTRING(p.[Value],1,10) as varchar(10)) as bit) FROM [dbo].[SystemParameterList] p WHERE p.[UserId] = @UserId AND p.[SystemName] = 'ServerDocsOldAutoRemoveEnabled';

		IF(@setActive = 1) BEGIN
			UPDATE [dbo].DocSrvDocumentationList SET [Active] = 0 WHERE Id <> @RecId AND [Name] = @RecName AND [DocumentationGroupId] = @GroupId; 		
		END

		--AutoRemove Older versions
		IF(@autoRemoveOld = 1) BEGIN
			DELETE FROM  [dbo].DocSrvDocumentationList WHERE Id <> @RecId AND [Name] = @RecName AND [DocumentationGroupId] = @GroupId; 		
		END

	END ELSE
		BEGIN -- INSERT
			SELECT @setActive = ins.[Active] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;
			SELECT @UserId = ins.UserId from inserted ins;
			SELECT @GroupId = ins.DocumentationGroupId from inserted ins;
			SELECT @RecName = ins.[Name] from inserted ins;

			--GET AutoRemoveSetting
			SELECT @autoRemoveOld = CAST(CAST(SUBSTRING(p.[Value],1,10) as varchar(10)) as bit) FROM [dbo].[SystemParameterList] p WHERE p.[UserId] = @UserId AND p.[SystemName] = 'ServerDocsOldAutoRemoveEnabled';

			--AutoVersioning
			SELECT @autoVersion = MAX(d.[AutoVersion]) + 1 FROM [dbo].DocSrvDocumentationList d WHERE d.[Name] = @RecName AND [DocumentationGroupId] = @GroupId;
			IF (@autoVersion = 0 ) BEGIN SET @autoVersion = 1; END
			UPDATE [dbo].DocSrvDocumentationList SET [AutoVersion] = @autoVersion WHERE Id = @RecId AND [DocumentationGroupId] = @GroupId;

			IF(@setActive = 1) BEGIN
				UPDATE [dbo].DocSrvDocumentationList SET [Active] = 0 WHERE Id <> @RecId AND [Name] = @RecName AND [DocumentationGroupId] = @GroupId; 		
			END
			
			--AutoRemove Older versions
			IF(@autoRemoveOld = 1) BEGIN
				DELETE FROM  [dbo].DocSrvDocumentationList WHERE Id <> @RecId AND [Name] = @RecName AND [DocumentationGroupId] = @GroupId; 		
			END
		END
END /* ELSE 
BEGIN --DELETE
	SELECT @setActive = ins.[Active] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;
	SELECT @RecName = ins.[Name] from deleted ins;

	IF(@setActive = 1) BEGIN
		UPDATE [dbo].DocSrvDocumentationList SET [Active] = 1 
		WHERE Id IN(SELECT TOP (1) MAX(d.Id) FROM [dbo].DocSrvDocumentationList d WHERE d.Id <> @RecId AND d.[Name] = @RecName)
		;
	END
END
*/
GO
ALTER TABLE [dbo].[DocSrvDocumentationList] ENABLE TRIGGER [TR_DocumentationList]
GO
/****** Object:  Trigger [dbo].[TR_UserList]    Script Date: 07.12.2023 13:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   TRIGGER [dbo].[TR_UserList] ON [dbo].[GlobalUserList]
FOR INSERT
AS
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @UserId int;

	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE

		SELECT @UserId = ins.Id from inserted ins;

	END ELSE
		BEGIN -- INSERT

			SELECT @UserId = ins.Id from inserted ins;
			
			INSERT INTO [dbo].[SystemParameterList]([UserId],[SystemName],[Value],[Type],[Description])
			SELECT @UserId, pa.[SystemName],pa.[Value],pa.[Type],pa.[Description]
			FROM [dbo].[ParameterList] pa
			WHERE pa.UserId IS NULL;

		END
END /* ELSE 
BEGIN --DELETE
	SELECT @setActive = ins.[Active] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;
	SELECT @RecName = ins.[Name] from deleted ins;

	IF(@setActive = 1) BEGIN
		UPDATE [dbo].DocumentationList SET [Active] = 1 
		WHERE Id IN(SELECT TOP (1) MAX(d.Id) FROM [dbo].DocumentationList d WHERE d.Id <> @RecId AND d.[Name] = @RecName)
		;
	END
END
*/
GO
ALTER TABLE [dbo].[GlobalUserList] ENABLE TRIGGER [TR_UserList]
GO
/****** Object:  Trigger [dbo].[TR_ParameterList]    Script Date: 07.12.2023 13:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   TRIGGER [dbo].[TR_ParameterList] ON [dbo].[SystemParameterList]
FOR INSERT
AS
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @UserId int;DECLARE @RecId int;DECLARE @SystemName varchar(50);	DECLARE @Value varchar(max);	
	DECLARE @Type varchar(20); DECLARE @Description varchar(MAX);

	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		
		SELECT @UserId = ins.UserId from inserted ins;
		
	END ELSE
		BEGIN -- INSERT

			SELECT @RecId = ins.[Id] from inserted ins;
			SELECT @UserId = ins.[UserId] from inserted ins;
			SELECT @SystemName = ins.[SystemName] from inserted ins;
			SELECT @Type  = ins.[Type] from inserted ins;

			SELECT @Value = CONVERT(varchar(MAX),p.[Value]), @Description = CONVERT(varchar(MAX),p.[Description]) from [dbo].[SystemParameterList] p WHERE p.Id =  @RecId ;
			
			IF (@UserId IS NULL) BEGIN
			
				INSERT INTO [dbo].[SystemParameterList]([UserId],[SystemName],[Value],[Type],[Description])
				SELECT DISTINCT pa.UserId, @SystemName, @Value, @Type, @Description
				FROM [dbo].[SystemParameterList] pa
				WHERE pa.UserId IS NOT NULL;
				
			END
		END
END /* ELSE 
BEGIN --DELETE
	SELECT @UserId = ins.[UserId] from inserted ins;
	SELECT @SystemName = ins.[SystemName] from inserted ins;
	
	IF (@UserId IS NULL) BEGIN
		DELETE FROM [dbo].[SystemParameterList] WHERE [SystemName] = @SystemName;
	END
END*/

GO
ALTER TABLE [dbo].[SystemParameterList] ENABLE TRIGGER [TR_ParameterList]
GO
/****** Object:  Trigger [dbo].[TR_ReportList]    Script Date: 07.12.2023 13:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   TRIGGER [dbo].[TR_ReportList] ON [dbo].[SystemReportList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].SystemReportList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].SystemReportList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].SystemReportList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].SystemReportList WHERE Id <> @RecId)
		;
	END
END
GO
ALTER TABLE [dbo].[SystemReportList] ENABLE TRIGGER [TR_ReportList]
GO
/****** Object:  Trigger [dbo].[TR_SystemTranslationList]    Script Date: 07.12.2023 13:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   TRIGGER [dbo].[TR_SystemTranslationList] ON [dbo].[SystemTranslationList]
FOR INSERT
AS
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @UserId int;DECLARE @RecId int;
	DECLARE @AutoFillDictionaries bit;
	DECLARE @SystemName varchar(50);DECLARE @DescriptionCz varchar(500);DECLARE @DescriptionEn varchar(500);
	SET @AutoFillDictionaries = 1;

	SET NOCOUNT ON;
    IF NOT EXISTS (SELECT 0 FROM deleted)
		BEGIN -- INSERT
			
			SELECT @RecId = ins.Id from inserted ins;
			SELECT @UserId = ins.UserId from inserted ins;
			SELECT @SystemName = ins.SystemName from inserted ins;
			SELECT @DescriptionCz = ins.DescriptionCz from inserted ins;
			SELECT @DescriptionEn = ins.DescriptionEn from inserted ins;
			
			--GET AutoFilling Configuration
			SELECT @AutoFillDictionaries = CAST(CAST(SUBSTRING(p.[Value],1,10) as varchar(10)) as bit) FROM [dbo].[SystemParameterList] p WHERE p.[UserId] IS NULL AND p.[SystemName] = 'ServerTranslationAutoFillEnabled';

			IF (@AutoFillDictionaries = 1) BEGIN
				IF(@DescriptionCz IS NULL OR LEN(@DescriptionCz) = 0) BEGIN SET @DescriptionCz = @SystemName; END
				IF(@DescriptionEn IS NULL OR LEN(@DescriptionEn) = 0) BEGIN SET @DescriptionEn = @SystemName; END
				UPDATE dbo.SystemTranslationList SET [DescriptionCz] = @DescriptionCz, [DescriptionEn] = @DescriptionEn WHERE Id = @RecId;
			END
		END
END
GO
ALTER TABLE [dbo].[SystemTranslationList] ENABLE TRIGGER [TR_SystemTranslationList]
GO
/****** Object:  Trigger [dbo].[TR_WebMenuList]    Script Date: 07.12.2023 13:45:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   TRIGGER [dbo].[TR_WebMenuList] ON [dbo].[WebMenuList]
FOR INSERT, UPDATE, DELETE
AS
DECLARE @Operation VARCHAR(15)
 
IF EXISTS (SELECT 0 FROM inserted)
BEGIN
	DECLARE @setDefault bit;DECLARE @RecId int;
	SET NOCOUNT ON;

    IF EXISTS (SELECT 0 FROM deleted)
    BEGIN --UPDADE
		SELECT @setDefault = ins.[Default] from inserted ins;
		SELECT @RecId = ins.Id from inserted ins;

		IF(@setDefault = 1) BEGIN
			UPDATE [dbo].WebMenuList SET [Default] = 0 WHERE Id <> @RecId; 		
		END
	END ELSE
		BEGIN -- INSERT
			SELECT @setDefault = ins.[Default] from inserted ins;
			SELECT @RecId = ins.Id from inserted ins;

			IF(@setDefault = 1) BEGIN
				UPDATE [dbo].WebMenuList SET [Default] = 0 WHERE Id <> @RecId; 		
			END
		
		END
END/* ELSE 
BEGIN --DELETE
	SELECT @setDefault = ins.[Default] from deleted ins;
	SELECT @RecId = ins.Id from deleted ins;

	IF(@setDefault = 1) BEGIN
		UPDATE [dbo].WebMenuList SET [Default] = 1  
		WHERE Id IN(SELECT TOP (1) Id FROM [dbo].WebMenuList WHERE Id <> @RecId)
		;
	END
END*/
GO
ALTER TABLE [dbo].[WebMenuList] ENABLE TRIGGER [TR_WebMenuList]
GO
