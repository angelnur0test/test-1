USE [master]
GO
/****** Object:  Database [rvca]    Script Date: 4/14/2020 5:30:56 PM ******/
CREATE DATABASE [rvca]
GO

ALTER DATABASE [rvca] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [rvca] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [rvca] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [rvca] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [rvca] SET ARITHABORT OFF 
GO
ALTER DATABASE [rvca] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [rvca] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [rvca] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [rvca] SET AUTO_UPDATE_STATISTICS ON 
GO
USE [rvca]
GO
/****** Object:  Table [dbo].[analytics]    Script Date: 4/14/2020 5:30:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[analytics](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[publicationDate] [datetime] NULL,
	[title] [nvarchar](500) NULL,
	[tags] [nvarchar](100) NULL,
	[fullContent] [ntext] NULL,
 CONSTRAINT [PK_analytics] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[news]    Script Date: 4/14/2020 5:30:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[news](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[publicationDate] [datetime] NULL,
	[title] [nvarchar](500) NULL,
	[previewPicture] [nvarchar](255) NULL,
	[bigPicture] [nvarchar](255) NULL,
	[newsText] [ntext] NULL,
	[newsTag] [nvarchar](100) NULL,
 CONSTRAINT [PK_news] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[analytics] ON 

GO
INSERT [dbo].[analytics] ([id], [publicationDate], [title], [tags], [fullContent]) VALUES (1, CAST(N'2019-08-30 00:00:00.000' AS DateTime), N'Бумаги Prudential Financial больше не интересны для покупки', NULL, N'Prudential Financial Inc. – американская компания финансового сектора, предоставляющая через свои филиалы и подразделения различные финансовые услуги, в число которых входят: страхование жизни, ренты и пенсии, услуги в сфере паевых инвестиционных фондов и инвестиционного менеджмента и др.<br/>В конце июля компания представила разочаровывающие финансовые результаты за второй квартал 2019 г. Кроме того, за последние шесть месяцев её бумаги принесли инвесторам доходность в размере 9,0% и приблизились к установленной нами целевой цене.<br/>Таким образом, с учётом слабой отчётности (не дотянувшая до прогнозов прибыль и отрицательный финансовый результат подразделения индивидуального страхования жизни), а также ввиду переоценённости акций Prudential по ключевому мультипликатору P/E мы рекомендуем зафиксировать прибыль и закрываем рекомендацию по данному инструменту.')
GO
INSERT [dbo].[analytics] ([id], [publicationDate], [title], [tags], [fullContent]) VALUES (2, CAST(N'2019-01-01 00:00:00.000' AS DateTime), N'Акции Valero Energy ждут отмашки для возобновления роста', NULL, N'Valero Energy является одним из крупнейших независимых нефтеперерабатывающих предприятий в Северной Америке, обладающим большими производственными мощностями, которые позволяют перекрыть все потребности различных типов клиентов.<br/>Valero Energy ведет деятельность в трех сегментах: переработка нефти, производство и реализация этанола. В первом квартале вместо сегмента VLP, появилось направление Renewable Diesel, которое занимается производством топлива из возобновляемых источников.<br/>Ключевым преимуществом Valero Energy является ее менеджмент, который проводит дружественную по отношению к инвесторам политику. Компания ежегодно увеличивает дивидендные выплаты и проводит программы обратного выкупа акций.<br/>Valero Energy опубликовала нейтральные финансовые результаты за второй квартал. Отметим, что компания зафиксировала снижение выручки и прибыли за второй квартал, однако оба показателя оказались лучше ожиданий аналитиков.<br/>Во втором квартале компания вернула акционерам $588 млн в виде выплат дивидендов ($376 млн) и программы обратного выкупа акций ($212 млн). Также стоит отметить, что менеджмент компании в 2019 году планирует направить 40-50% денежных средств от операционной деятельности на выплату дивидендов и программу обратного выкупа акций.<br/>Пропускная способность нефтеперерабатывающих заводов в первом квартале составила 94%, что составляет 3 млн баррелей в день.<br/>Valero Energy все также недооценена по ряду мультипликаторов по отношению к конкурентам.')
GO
INSERT [dbo].[analytics] ([id], [publicationDate], [title], [tags], [fullContent]) VALUES (1002, CAST(N'2019-12-04 00:00:00.000' AS DateTime), N'Аналитические сборники РАВИ 2004 – 2019', NULL, N'Аналитический сборник является в настоящий момент наиболее полным аналитическим документом, описывающим состояние венчурной индустрии в России. Для подготовки данной книги экспертами Российской Ассоциации Венчурного Инвестирования было проведено широкомасштабное исследование рынка прямых и венчурных инвестиций, включавшее в себя анкетирование, в котором приняло участие большое количество представителей ведущих инвестиционных структур, работающих в данной сфере.
<br/><br/>
<a href="http://www.rvca.ru/rus/resource/library/rvca-yearbook/">http://www.rvca.ru/rus/resource/library/rvca-yearbook/</a>')
GO
INSERT [dbo].[analytics] ([id], [publicationDate], [title], [tags], [fullContent]) VALUES (1003, CAST(N'2019-12-03 00:00:00.000' AS DateTime), N'Аналитика и исследования РВК и партнеров', NULL, N'В разделе «Аналитика» представлены актуальные исследования и обзоры, подготовленные РВК совместно с российскими и зарубежными партнёрами. Аналитические материалы оценивают текущее состояние венчурного рынка и российской инновационной экосистемы, определяют тенденции развития отдельных технологических отраслей, изучают зарубежный опыт развития инновационных систем, анализируют лучшие мировые практики.
<br/><br/>
<a href="https://www.rvc.ru/analytics/">https://www.rvc.ru/analytics/</a>')
GO
SET IDENTITY_INSERT [dbo].[analytics] OFF
GO
SET IDENTITY_INSERT [dbo].[news] ON 

GO
INSERT [dbo].[news] ([id], [publicationDate], [title], [previewPicture], [bigPicture], [newsText], [newsTag]) VALUES (1, CAST(N'2019-01-08 00:00:00.000' AS DateTime), N'В «ТехноСпарке» изготовили первую партию оборудования для предотвращения турбинного вращения нефтяных насосов', N'75221d126bc6abb87734856736851a92.jpg', NULL, N'Контрактный производитель мехатроники, медицинской техники и робототехники TEN fab, входящий в Группу компаний «ТехноСпарк», изготовил первую серию инновационных устройств для предотвращения турбинного вращения вала погружных электроцентробежных насосов в нефтяных скважинах. Работа выполнена по заказу разработчика оборудования — компании Oklas Technologies.', N'Производство')
GO
INSERT [dbo].[news] ([id], [publicationDate], [title], [previewPicture], [bigPicture], [newsText], [newsTag]) VALUES (2, CAST(N'2019-07-09 00:00:00.000' AS DateTime), N'Компания «ТестГен» начала разработку тест-системы для диагностики предракового состояния', N'testgen.jpg', NULL, N'Компания «ТестГен» из Ульяновского наноцентра ULNANOTECH приступила к разработке тест-системы для диагностики микросателлитной нестабильности в практической онкологии. Предполагается, что набор для анализа будет доступен для стандартных ПЦР-лабораторий (использующих полимеразную цепную реакцию для диагностики наследственных и инфекционных заболеваний). На регистрацию тест-системы планируется выйти к концу 2019 года', N'Медицина')
GO
INSERT [dbo].[news] ([id], [publicationDate], [title], [previewPicture], [bigPicture], [newsText], [newsTag]) VALUES (3, CAST(N'2019-07-09 00:00:00.000' AS DateTime), N'Подготовлена образовательная программа по химическим технологиям в наноэлектронике', N'obraz_pr.jpg', NULL, N'При поддержке Фонда инфраструктурных и образовательных программ Группы РОСНАНО Российским технологическим университетом МИРЭА подготовлена дополнительная образовательная программа профессиональной переподготовки «Химические технологии в наноэлектронике».', N'Образование')
GO
INSERT [dbo].[news] ([id], [publicationDate], [title], [previewPicture], [bigPicture], [newsText], [newsTag]) VALUES (4, CAST(N'2019-07-04 00:00:00.000' AS DateTime), N'Локализация орфанного препарата на заводе НАНОЛЕК позволит сэкономить бюджетные расходы', N'nanolek.jpg', NULL, N'24 июля 2019 в Кирове состоялся специальный пресс-тур, организаторами которого выступили УК «РОСНАНО» и российская биофармацевтическая компания НАНОЛЕК при участии Оргкомитета Национальной премии в области импортозамещения и трансфера технологий «Приоритет-2019».', N'Медицина')
GO
SET IDENTITY_INSERT [dbo].[news] OFF
GO
USE [master]
GO
ALTER DATABASE [rvca] SET  READ_WRITE 
GO
