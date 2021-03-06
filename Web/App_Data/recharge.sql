USE [recharge]
GO
/****** Object:  Table [dbo].[AllOperators]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllOperators](
	[ID] [nvarchar](36) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[Description] [nvarchar](128) NOT NULL,
	[Country] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_PulsaOperators] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AllTypes]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllTypes](
	[ID] [nvarchar](36) NOT NULL,
	[Types] [nvarchar](16) NOT NULL,
	[Channel] [nvarchar](128) NOT NULL,
	[OperatorsId] [nvarchar](36) NOT NULL,
	[OperatorsName] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_AllTypes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Logs]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logs](
	[ID] [varchar](36) NOT NULL,
	[LogText] [varchar](2000) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [nvarchar](36) NOT NULL,
	[PulsaCode] [nvarchar](128) NOT NULL,
	[CnPulsatype] [nvarchar](8) NOT NULL,
	[Masaaktif] [nvarchar](128) NOT NULL,
	[CnQuatity] [nvarchar](36) NOT NULL,
	[CnOp] [nvarchar](128) NOT NULL,
	[UsePoints] [decimal](18, 2) NOT NULL,
	[CnPrice] [decimal](18, 2) NOT NULL,
	[CnOldprice] [decimal](18, 2) NOT NULL,
	[OperatorId] [nvarchar](36) NOT NULL,
	[WechatpayState] [int] NOT NULL,
	[PulsaState] [int] NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[WechatOpenid] [nvarchar](64) NOT NULL,
	[WechatNickname] [nvarchar](64) NOT NULL,
	[WechatHeadimgurl] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PulsaPrefix]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PulsaPrefix](
	[Code] [nvarchar](4) NOT NULL,
	[OperatorID] [nvarchar](36) NOT NULL,
	[OperatorName] [nvarchar](64) NOT NULL,
	[DataChannel] [nvarchar](64) NOT NULL,
	[PulsaChannel] [nvarchar](64) NOT NULL,
	[LogoUrl] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_PulsaPrefix] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PulsaProduct]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PulsaProduct](
	[pulsa_code] [nvarchar](128) NOT NULL,
	[pulsa_op] [nvarchar](128) NOT NULL,
	[pulsa_nominal] [nvarchar](512) NOT NULL,
	[pulsa_price] [decimal](18, 0) NOT NULL,
	[pulsa_type] [nvarchar](128) NOT NULL,
	[masaaktif] [nvarchar](128) NOT NULL,
	[status] [nvarchar](36) NOT NULL,
	[cn_quatity] [nvarchar](36) NOT NULL,
	[cn_op] [nvarchar](128) NOT NULL,
	[cn_status] [int] NOT NULL,
	[cn_price] [decimal](18, 2) NOT NULL,
	[create_time] [datetime2](7) NOT NULL,
	[update_time] [datetime2](7) NOT NULL,
	[pulsa_channel] [varchar](128) NOT NULL,
	[cn_oldprice] [decimal](18, 2) NOT NULL CONSTRAINT [DF_PulsaProduct_cn_oldprice]  DEFAULT ((0)),
 CONSTRAINT [PK_Pulsa_Product] PRIMARY KEY CLUSTERED 
(
	[pulsa_code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SysUser](
	[Username] [nvarchar](36) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](200) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](200) NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_SYSUSER] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserPoints]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPoints](
	[ID] [nvarchar](36) NOT NULL,
	[OrderId] [nvarchar](36) NOT NULL,
	[Point] [decimal](18, 2) NOT NULL,
	[ToOpenid] [nvarchar](64) NOT NULL,
	[FromWechatOpenid] [nvarchar](64) NOT NULL,
	[FromWechatNickname] [nvarchar](64) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_UserPoints] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WeChatUser]    Script Date: 2019/12/22 11:52:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeChatUser](
	[Openid] [nvarchar](64) NOT NULL,
	[Nickname] [nvarchar](64) NOT NULL,
	[Sex] [int] NOT NULL,
	[Country] [nvarchar](128) NOT NULL,
	[Province] [nvarchar](128) NOT NULL,
	[City] [nvarchar](128) NOT NULL,
	[Headimgurl] [nvarchar](512) NOT NULL,
	[CreateTime] [datetime2](7) NOT NULL,
	[RecommendUserOpenid] [nvarchar](64) NOT NULL,
	[MyRecommendCode] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_WeChatUser] PRIMARY KEY CLUSTERED 
(
	[Openid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_UsePoints]  DEFAULT ((0)) FOR [UsePoints]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1启用0禁用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PulsaProduct', @level2type=N'COLUMN',@level2name=N'cn_status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Username'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Password'
GO
