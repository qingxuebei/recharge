USE [Yino]
GO
/****** Object:  Table [dbo].[Market]    Script Date: 2019/6/14 13:07:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Market](
	[ID] [nvarchar](36) NOT NULL,
	[IconUrl] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[SmsUrl] [nvarchar](200) NOT NULL,
	[MaxMoney] [int] NOT NULL,
	[Tenure] [nvarchar](200) NOT NULL,
	[Rate] [nvarchar](200) NOT NULL,
	[ApprovlTime] [nvarchar](200) NOT NULL,
	[Disbursement] [nvarchar](200) NOT NULL,
	[SortId] [int] NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreatePerson] [nvarchar](100) NULL,
	[UpdateTime] [datetime] NULL,
	[UpdatePerson] [nvarchar](100) NULL,
	[State] [int] NOT NULL,
 CONSTRAINT [PK_Market] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SysUser]    Script Date: 2019/6/14 13:07:05 ******/
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
ALTER TABLE [dbo].[Market] ADD  CONSTRAINT [DF_Market_MaxMoney]  DEFAULT ((0)) FOR [MaxMoney]
GO
ALTER TABLE [dbo].[Market] ADD  CONSTRAINT [DF_Market_SortId]  DEFAULT ((0)) FOR [SortId]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Username'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysUser', @level2type=N'COLUMN',@level2name=N'Password'
GO
