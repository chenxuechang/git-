USE [master]
GO
/****** Object:  Database [TalentBigData]    Script Date: 2021/7/27 9:33:57 ******/
CREATE DATABASE [TalentBigData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TalentBigData', FILENAME = N'D:\数据库\MSSQL\Data\TalentBigData.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TalentBigData_log', FILENAME = N'D:\数据库\MSSQL\Data\TalentBigData_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TalentBigData] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TalentBigData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TalentBigData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TalentBigData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TalentBigData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TalentBigData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TalentBigData] SET ARITHABORT OFF 
GO
ALTER DATABASE [TalentBigData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TalentBigData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TalentBigData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TalentBigData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TalentBigData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TalentBigData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TalentBigData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TalentBigData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TalentBigData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TalentBigData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TalentBigData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TalentBigData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TalentBigData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TalentBigData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TalentBigData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TalentBigData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TalentBigData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TalentBigData] SET RECOVERY FULL 
GO
ALTER DATABASE [TalentBigData] SET  MULTI_USER 
GO
ALTER DATABASE [TalentBigData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TalentBigData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TalentBigData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TalentBigData] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TalentBigData] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TalentBigData', N'ON'
GO
ALTER DATABASE [TalentBigData] SET QUERY_STORE = OFF
GO
USE [TalentBigData]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [TalentBigData]
GO
/****** Object:  UserDefinedFunction [dbo].[F_UserRoleInfo]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
--管理员用户角色信息
-- =============================================
CREATE FUNCTION [dbo].[F_UserRoleInfo]
(
@userId uniqueidentifier
)
RETURNS nvarchar(500)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @result nvarchar(500);
	set @result='';
	DECLARE userrole_cursor cursor for
	select v.RoleName from V_UserRoleInfo v where v.IsDel='0' and UserId=@userId ;

	open userrole_cursor
	declare @rolename nvarchar(50)
	fetch next from userrole_cursor into  @rolename
	while @@FETCH_STATUS=0
	begin
	set @result=@result+','+@rolename
	fetch next from userrole_cursor into  @rolename
	end
	close userrole_cursor
	deallocate userrole_cursor
	if len(@result)>0
	begin
	set @result=SUBSTRING(@result,2,LEN(@result)-1);
	end
	else
	begin
	set @result='';
	end
	-- Return the result of the function
	RETURN @result;

END
GO
/****** Object:  Table [dbo].[T_UserRole]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_UserRole](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_T_USERROLE] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_RoleManage]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_RoleManage](
	[RoleId] [uniqueidentifier] NOT NULL,
	[RoleName] [nvarchar](50) NULL,
	[RoleCode] [varchar](50) NULL,
	[Sort] [decimal](18, 2) NULL,
	[LastUpdateTime] [datetime] NULL,
	[IsDel] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_UserRoleInfo]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_UserRoleInfo]
AS
SELECT  ur.UserId, trm.RoleId, trm.RoleName, trm.RoleCode, trm.Sort, trm.LastUpdateTime, trm.IsDel
FROM      dbo.T_UserRole AS ur INNER JOIN
                   dbo.T_RoleManage AS trm ON ur.RoleId = trm.RoleId
GO
/****** Object:  Table [dbo].[T_Menu]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Menu](
	[MenuId] [uniqueidentifier] NOT NULL,
	[MenuName] [nvarchar](50) NULL,
	[MenuCode] [varchar](50) NULL,
	[MenuUrl] [varchar](200) NULL,
	[MenuTarget] [varchar](50) NULL,
	[MenuSort] [decimal](18, 0) NULL,
	[MenuPid] [uniqueidentifier] NULL,
	[MenuType] [char](1) NULL,
	[IconClass] [varchar](100) NULL,
	[LastUpdateTime] [datetime] NULL,
	[IsDel] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_RoleMenu]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_RoleMenu](
	[RoleId] [uniqueidentifier] NOT NULL,
	[MenuId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_T_ROLEMENU] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_UserRoleMenu]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_UserRoleMenu]
AS
SELECT  ur.UserId, m.MenuId, m.MenuName, m.MenuCode, m.MenuUrl, m.MenuTarget, m.MenuSort, m.MenuPid, m.MenuType, 
                   m.IconClass, m.LastUpdateTime, m.IsDel
FROM      dbo.T_UserRole AS ur INNER JOIN
                   dbo.T_RoleMenu AS rm ON ur.RoleId = rm.RoleId INNER JOIN
                   dbo.T_Menu AS m ON rm.MenuId = m.MenuId
WHERE   (m.IsDel = '0')
GO
/****** Object:  Table [dbo].[T_User]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_User](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserAccount] [varchar](50) NULL,
	[UserPassword] [varchar](50) NULL,
	[UserName] [nvarchar](20) NULL,
	[UserSex] [char](1) NULL,
	[UserTitles] [nvarchar](100) NULL,
	[UserDept] [uniqueidentifier] NULL,
	[UserTelPhone] [varchar](50) NULL,
	[UserMobilePhone] [varchar](50) NULL,
	[UserEmail] [varchar](200) NULL,
	[CreateTime] [datetime] NULL,
	[LastUpdateTime] [datetime] NULL,
	[IsDel] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[V_User]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[V_User]
AS
SELECT  UserId, UserAccount, UserPassword, UserName, UserSex, UserTitles, UserDept, UserTelPhone, UserMobilePhone, 
                   UserEmail, LastUpdateTime, IsDel, CASE u.UserSex WHEN 1 THEN '男' WHEN 2 THEN '女' END AS SexName, 
                   dbo.F_UserRoleInfo(UserId) AS RoleName, CreateTime
FROM      dbo.T_User AS u
GO
/****** Object:  View [dbo].[V_RoleMenu]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_RoleMenu]
AS
SELECT  trm.RoleId, trm.MenuId, tm.MenuPid
FROM      dbo.T_RoleMenu AS trm INNER JOIN
                   dbo.T_Menu AS tm ON trm.MenuId = tm.MenuId
WHERE   (tm.IsDel = '0')
GO
/****** Object:  Table [dbo].[T_DataDictionary]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_DataDictionary](
	[id] [uniqueidentifier] NOT NULL,
	[dicName] [nvarchar](50) NULL,
	[dicCode] [varchar](50) NULL,
	[dicType] [char](1) NULL,
	[dicSort] [int] NULL,
	[isDel] [char](1) NULL,
 CONSTRAINT [PK_T_DATADICTIONARY] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_DataDictionaryChild]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_DataDictionaryChild](
	[Id] [uniqueidentifier] NOT NULL,
	[dicPid] [uniqueidentifier] NULL,
	[dicName] [nvarchar](50) NULL,
	[dicCode] [varchar](50) NULL,
	[minValue] [numeric](13, 3) NULL,
	[maxValue] [numeric](13, 3) NULL,
	[dicSort] [int] NULL,
	[isDel] [char](1) NULL,
	[createTime] [datetime] NULL,
	[lastUpdateTime] [datetime] NULL,
 CONSTRAINT [PK_T_DATADICTIONARYCHILD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_Dept]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Dept](
	[DeptId] [uniqueidentifier] NOT NULL,
	[DeptName] [nvarchar](100) NULL,
	[DeptCode] [varchar](50) NULL,
	[DeptSort] [decimal](18, 2) NULL,
	[DeptPid] [uniqueidentifier] NULL,
	[LastUpdateTime] [datetime] NULL,
	[IsDel] [char](1) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[T_VerificationCode]    Script Date: 2021/7/27 9:33:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_VerificationCode](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[number] [int] NULL,
	[time] [datetime] NULL,
 CONSTRAINT [PK_T_VerificationCode] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[T_DataDictionary] ([id], [dicName], [dicCode], [dicType], [dicSort], [isDel]) VALUES (N'45fccc15-81cb-46c9-8aa6-29e5c33203ad', N'单位类型', N'CompType', N'1', 1, N'0')
INSERT [dbo].[T_DataDictionary] ([id], [dicName], [dicCode], [dicType], [dicSort], [isDel]) VALUES (N'13463397-ab95-42c3-926f-dc71d261a70b', N'GDP规模', N'GdpScope', N'2', 2, N'0')
GO
INSERT [dbo].[T_Dept] ([DeptId], [DeptName], [DeptCode], [DeptSort], [DeptPid], [LastUpdateTime], [IsDel]) VALUES (N'358bfbcb-71fb-4040-b53c-46dd6ff15b3c', N'政工部', N'BM_ZHGB', NULL, N'e5ae8c48-6f7b-4fe2-ac6e-544c72cd1b01', CAST(N'2021-07-13T00:00:00.000' AS DateTime), N'0')
INSERT [dbo].[T_Dept] ([DeptId], [DeptName], [DeptCode], [DeptSort], [DeptPid], [LastUpdateTime], [IsDel]) VALUES (N'7107848e-131c-42e6-8fed-d9f22a827ecd', N'宣传部', N'BM_XCHB', NULL, N'e5ae8c48-6f7b-4fe2-ac6e-544c72cd1b01', CAST(N'2021-07-13T00:00:00.000' AS DateTime), N'0')
INSERT [dbo].[T_Dept] ([DeptId], [DeptName], [DeptCode], [DeptSort], [DeptPid], [LastUpdateTime], [IsDel]) VALUES (N'e5ae8c48-6f7b-4fe2-ac6e-544c72cd1b01', N'部门管理', N'BMGL', NULL, N'00000000-0000-0000-0000-000000000000', CAST(N'2021-07-13T00:00:00.000' AS DateTime), N'0')
GO
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'729e5048-6b26-40b5-adb4-68fa51259db3', N'系统管理', N'XTGL', N'', N'', CAST(1 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', N'glyphicon glyphicon-asterisk', CAST(N'2021-07-20T14:09:05.193' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'8d648f04-579e-4d63-b5f9-9efa451f6f85', N'1111', N'1111', NULL, NULL, CAST(3 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-26T16:40:19.070' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'85e680e6-3d0c-4f78-a1e6-1c991c99a058', N'4', N'4', NULL, NULL, CAST(4 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:51:42.117' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'967370ee-36f2-463d-bc33-37dda3df5cac', N'5', N'5', NULL, NULL, CAST(5 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:51:48.197' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'4759961e-8072-4f92-b5c6-9144b90c6187', N'6', N'6', NULL, NULL, CAST(6 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:51:55.030' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'da06022a-a29c-4f78-af2d-3451d91e61a8', N'7', N'7', NULL, NULL, CAST(7 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:52:00.780' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'e1a4b77f-034a-47ba-8fd7-de838e17fb9b', N'8', N'8', NULL, NULL, CAST(8 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:52:06.840' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'fe1ad881-b608-4e5b-abf4-6203ee3f73d5', N'9', N'9', NULL, NULL, CAST(9 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:52:11.760' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'1887df9d-39a4-4a6d-a988-fff46b52e914', N'10', N'10', NULL, NULL, CAST(10 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:52:20.090' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'15e8465c-3290-4996-a60d-5c68d7dea973', N'11', N'11', NULL, NULL, CAST(11 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:52:26.390' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'2eadf141-836a-40ee-9d1a-9b53427d43ee', N'12', N'12', NULL, NULL, CAST(12 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:52:33.063' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'd9c30607-5d04-41b0-a465-7daa95c98d01', N'13', N'13', NULL, NULL, CAST(13 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:52:40.360' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'a5028600-115d-4b09-a055-47392c683ec3', N'14', N'14', NULL, NULL, CAST(14 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-20T17:53:05.847' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'6443930f-ce89-4b83-bc31-2fb38308c0e8', N'15', N'15', NULL, NULL, CAST(15 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-26T16:40:33.863' AS DateTime), N'1')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'5ba87b3f-ea43-46f9-a36a-c95b2e0c331d', N'16', N'16', NULL, NULL, CAST(16 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-26T16:40:43.480' AS DateTime), N'1')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'38974073-7921-4eca-bf82-ee8dd8f177ae', N'角色管理', N'XTGL-JSGL', N'/SystemManage/Role', N'ifrMain', CAST(3 AS Decimal(18, 0)), N'729e5048-6b26-40b5-adb4-68fa51259db3', N'2', NULL, CAST(N'2021-07-20T21:41:01.497' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'2eab6ea2-53b9-4265-856f-2370f085a34f', N'数据字典', N'XTGL-SJZD', N'/SystemManage/DataDictionary', N'ifrMain', CAST(4 AS Decimal(18, 0)), N'729e5048-6b26-40b5-adb4-68fa51259db3', N'2', NULL, CAST(N'2021-07-22T15:02:56.390' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'5bed646a-5bfb-4e46-a39f-58e42da18695', N'流程管理', N'LCGL', NULL, NULL, CAST(2 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', N'glyphicon glyphicon-road', CAST(N'2021-07-20T14:08:45.937' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'ce581a73-681f-4d03-8e08-bb59ea20aaef', N'管理员管理', N'XTGL-GLYGL', N'/SystemManage/Manager', N'ifrMain', CAST(1 AS Decimal(18, 0)), N'729e5048-6b26-40b5-adb4-68fa51259db3', N'2', N'11', CAST(N'2021-07-20T14:44:39.687' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'6866d380-c4e8-4aff-bfab-2848415bd4c2', N'流程管理1', N'LCGL1', NULL, NULL, CAST(1 AS Decimal(18, 0)), N'5bed646a-5bfb-4e46-a39f-58e42da18695', N'2', NULL, CAST(N'2021-07-20T20:25:45.757' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'6957bd94-90d3-4de3-8b30-7c66944f60d1', N'功能管理', N'XTGL-GNGL', N'/SystemManage/FuncManage', N'ifrMain', CAST(2 AS Decimal(18, 0)), N'729e5048-6b26-40b5-adb4-68fa51259db3', N'2', N'', CAST(N'2021-07-20T14:48:20.593' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'1c972adf-989e-4d20-baa9-1f2e9478c042', N'123213', N'213123', N'123213', N'123123', CAST(2 AS Decimal(18, 0)), N'5bed646a-5bfb-4e46-a39f-58e42da18695', N'2', N'213213', CAST(N'2021-07-20T20:26:02.460' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'713733f9-f491-4f99-ab70-f2c238216b83', N'1234-神鼎飞丹砂', N'code-001', N'http://www.baidu.com', N'iframain', CAST(1 AS Decimal(18, 0)), N'8d648f04-579e-4d63-b5f9-9efa451f6f85', N'2', NULL, CAST(N'2021-07-26T16:39:36.653' AS DateTime), N'0')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'7faa25f3-eb60-47ec-8822-61e26b7fd498', N'3234', N'34234', NULL, N'glyphicon glyphicon-road', CAST(1 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-26T16:40:08.830' AS DateTime), N'1')
INSERT [dbo].[T_Menu] ([MenuId], [MenuName], [MenuCode], [MenuUrl], [MenuTarget], [MenuSort], [MenuPid], [MenuType], [IconClass], [LastUpdateTime], [IsDel]) VALUES (N'd65a81bf-1384-4e07-93b6-b36d91d5234b', N'666', N'666', NULL, NULL, CAST(6 AS Decimal(18, 0)), N'00000000-0000-0000-0000-000000000000', N'1', NULL, CAST(N'2021-07-26T16:41:22.203' AS DateTime), N'0')
GO
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'1cbe49a7-a1c5-40fa-b56a-d04b20f3b362', N'管理员', N'GLY', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-13T00:00:00.000' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'1cbe49a8-a1c5-40fa-b56a-d04b20f3b362', N'管理员1', N'GLY1', CAST(2.00 AS Decimal(18, 2)), CAST(N'2021-07-13T00:00:00.000' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'1cbe59a7-a1c5-40fa-b56a-d04b20f3b362', N'管理员2', N'GLY2', CAST(3.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:48:36.117' AS DateTime), N'1')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'1d5393b2-099c-4e21-8bc9-5c6d62adad28', N'管理员3', N'GLY3', CAST(3.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:48:38.247' AS DateTime), N'1')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'fadbfcda-70d6-41d1-86cb-84a0ece40527', N'管理员13', N'CODE14', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:01:41.717' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'a699a083-d1c5-402f-89c3-e5e020c8ec62', N'管理员003', N'003', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:40:03.690' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'678', N'555', CAST(6.00 AS Decimal(18, 2)), CAST(N'2021-07-26T16:41:51.427' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'e8e0d663-e55b-452e-af08-fca16f1b1d6c', N'管理员4', N'123', CAST(2.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:48:21.590' AS DateTime), N'1')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'59717369-7224-4396-afdc-a2100b423502', N'管理员5', N'CODE1', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:01:23.953' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'e6d230d2-4b54-434d-97ee-cfb52fedeb5d', N'管理员131', N'CODE141', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:04:25.257' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'e6ac8f9c-4627-4dad-b7bf-067a75052e2a', N'1323', N'1233', CAST(2.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:07:23.160' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'e5695878-f978-4ba4-a4ef-4a0ffe5fd291', N'管理员001', N'gly001', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:09:28.827' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'de884d72-dc9d-4fd7-b293-d7a3bd8b4534', N'管理员004', N'1234', CAST(234234.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:48:33.893' AS DateTime), N'1')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'da3a64ac-01d5-4c13-a2ee-f7bfd0db0c66', N'管理员005', N'12344', CAST(2342344.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:48:31.447' AS DateTime), N'1')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'6fabc154-0579-4e11-af6f-ed3555651603', N'管理员0051', N'123441', CAST(23423441.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:48:29.027' AS DateTime), N'1')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'85e28c7b-ac48-45e1-a62f-e90c6d150c2a', N'管理员00511', N'1234411', CAST(234234411.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:48:26.503' AS DateTime), N'1')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'77dce2c0-7a03-4455-a9b2-0494ee526c61', N'管理员006', N'1', CAST(2.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:41:28.427' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'44fe0fdd-e712-46a8-aa1e-cfc193bd38e7', N'管理员005', N'006', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:49:03.677' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'236ba028-0ac5-4d8d-919c-5523ce38ace6', N'管理员全', N'001', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-20T21:41:34.977' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'd115d771-6bbb-4586-8c6d-359c172ac89b', N'测试001', N'0011', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-26T19:51:07.620' AS DateTime), N'0')
INSERT [dbo].[T_RoleManage] ([RoleId], [RoleName], [RoleCode], [Sort], [LastUpdateTime], [IsDel]) VALUES (N'8511de78-3761-44cd-a5a0-b4972b1f09d5', N'管理员002', N'2', CAST(1.00 AS Decimal(18, 2)), CAST(N'2021-07-20T20:24:35.807' AS DateTime), N'0')
GO
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'd115d771-6bbb-4586-8c6d-359c172ac89b', N'1c972adf-989e-4d20-baa9-1f2e9478c042')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'd115d771-6bbb-4586-8c6d-359c172ac89b', N'2eab6ea2-53b9-4265-856f-2370f085a34f')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'd115d771-6bbb-4586-8c6d-359c172ac89b', N'5bed646a-5bfb-4e46-a39f-58e42da18695')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'd115d771-6bbb-4586-8c6d-359c172ac89b', N'729e5048-6b26-40b5-adb4-68fa51259db3')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'd115d771-6bbb-4586-8c6d-359c172ac89b', N'6957bd94-90d3-4de3-8b30-7c66944f60d1')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'd115d771-6bbb-4586-8c6d-359c172ac89b', N'ce581a73-681f-4d03-8e08-bb59ea20aaef')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'd115d771-6bbb-4586-8c6d-359c172ac89b', N'38974073-7921-4eca-bf82-ee8dd8f177ae')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'236ba028-0ac5-4d8d-919c-5523ce38ace6', N'6866d380-c4e8-4aff-bfab-2848415bd4c2')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'236ba028-0ac5-4d8d-919c-5523ce38ace6', N'5bed646a-5bfb-4e46-a39f-58e42da18695')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'236ba028-0ac5-4d8d-919c-5523ce38ace6', N'729e5048-6b26-40b5-adb4-68fa51259db3')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'236ba028-0ac5-4d8d-919c-5523ce38ace6', N'6957bd94-90d3-4de3-8b30-7c66944f60d1')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'236ba028-0ac5-4d8d-919c-5523ce38ace6', N'ce581a73-681f-4d03-8e08-bb59ea20aaef')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'236ba028-0ac5-4d8d-919c-5523ce38ace6', N'38974073-7921-4eca-bf82-ee8dd8f177ae')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'00000000-0000-0000-0000-000000000000')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'85e680e6-3d0c-4f78-a1e6-1c991c99a058')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'1c972adf-989e-4d20-baa9-1f2e9478c042')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'2eab6ea2-53b9-4265-856f-2370f085a34f')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'6866d380-c4e8-4aff-bfab-2848415bd4c2')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'da06022a-a29c-4f78-af2d-3451d91e61a8')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'967370ee-36f2-463d-bc33-37dda3df5cac')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'a5028600-115d-4b09-a055-47392c683ec3')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'5bed646a-5bfb-4e46-a39f-58e42da18695')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'15e8465c-3290-4996-a60d-5c68d7dea973')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'fe1ad881-b608-4e5b-abf4-6203ee3f73d5')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'729e5048-6b26-40b5-adb4-68fa51259db3')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'6957bd94-90d3-4de3-8b30-7c66944f60d1')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'd9c30607-5d04-41b0-a465-7daa95c98d01')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'4759961e-8072-4f92-b5c6-9144b90c6187')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'2eadf141-836a-40ee-9d1a-9b53427d43ee')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'8d648f04-579e-4d63-b5f9-9efa451f6f85')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'd65a81bf-1384-4e07-93b6-b36d91d5234b')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'ce581a73-681f-4d03-8e08-bb59ea20aaef')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'e1a4b77f-034a-47ba-8fd7-de838e17fb9b')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'38974073-7921-4eca-bf82-ee8dd8f177ae')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'713733f9-f491-4f99-ab70-f2c238216b83')
INSERT [dbo].[T_RoleMenu] ([RoleId], [MenuId]) VALUES (N'8bd9cd23-0024-4b11-bd1a-941ffefb7713', N'1887df9d-39a4-4a6d-a988-fff46b52e914')
GO
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'61cf9ea3-72b2-4ddc-b2f1-017e1e2a7c86', N'admin0001', N'123456', N'何倩', N'2', N'研发', N'358bfbcb-71fb-4040-b53c-46dd6ff15b3c', N'0310-5485335', N'15175041649', N'284635130@qq.com', CAST(N'2021-07-18T16:05:09.223' AS DateTime), CAST(N'2021-07-20T21:40:00.170' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'61cf9ea2-72b2-4ddc-b2f1-016e1e2a7c86', N'admin001', N'123456', N'何倩1', N'1', N'研发', N'358bfbcb-71fb-4040-b53c-46dd6ff15b3c', N'0310-5485335', N'15175041649', N'284635130@qq.com', CAST(N'2021-07-18T16:06:40.230' AS DateTime), CAST(N'2021-07-26T16:32:11.273' AS DateTime), N'1')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'770bdefe-e756-4b64-8bdb-fcf1d38a2c87', N'admin00002', N'123456', N'管理员1', N'2', N'职称00012', N'00000000-0000-0000-0000-000000000000', N'023-23432432', N'15076455764', N'284635130@qq.com', CAST(N'2021-07-26T16:31:51.433' AS DateTime), CAST(N'2021-07-26T16:32:02.870' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'00010000-0000-0000-0000-000000000000', N'admin123', N'1234567', N'何倩', N'2', N'职称 ', N'61cf9ea1-72b2-4ddc-b2f1-016e1e2a7c86', N'0310-5485335', N'15076455764', N'284635130@qq.com', CAST(N'2021-07-18T13:32:01.670' AS DateTime), CAST(N'2021-07-18T19:44:10.577' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'3688d73c-9425-4bcb-96f3-b270a77476cc', N'admin1234', N'123456', N'何倩', N'2', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'15076455764', NULL, CAST(N'2021-07-18T14:04:59.090' AS DateTime), CAST(N'2021-07-18T19:44:16.417' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'69820ac4-953b-46d9-bb68-03b204b8d16e', N'admin123451', N'1234567', N'张三1', N'2', N'职称1', N'e5ae8c48-6f7b-4fe2-ac6e-544c72cd1b01', N'010-73627341', N'15076455761', N'284635131@qq.com', CAST(N'2021-07-18T14:04:53.243' AS DateTime), CAST(N'2021-07-18T19:44:13.663' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'de95f706-0f83-459d-85c2-52e637ca86e6', N'wangwu0981', N'123456', N'王五', N'1', N'职称', N'e5ae8c48-6f7b-4fe2-ac6e-544c72cd1b01', NULL, N'18763627364', NULL, CAST(N'2021-07-18T11:22:31.010' AS DateTime), CAST(N'2021-07-18T19:43:55.437' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'4e2455c2-1a9e-4111-8021-0b92808ab592', N'admin123456', N'123456', N'李六', N'2', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'18963627283', NULL, CAST(N'2021-07-18T16:06:08.610' AS DateTime), CAST(N'2021-07-18T17:24:07.867' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'ab1dd4bc-eee5-4706-9552-6f057d52670a', N'test002', N'123456', N'测试002', N'2', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'15076455764', NULL, CAST(N'2021-07-18T16:04:56.407' AS DateTime), CAST(N'2021-07-18T16:04:56.407' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'f240aa70-a1c0-4182-b96c-5cf9132907c1', N'again001', N'123456', N'再来一个', N'1', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'15076455764', NULL, CAST(N'2021-07-18T16:15:54.870' AS DateTime), CAST(N'2021-07-18T16:15:54.870' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'0f15f8e1-d6e1-47b3-9cca-c0da4116e1a0', N'test001', N'123456', N'测试0001', N'2', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'15076455764', NULL, CAST(N'2021-07-18T16:04:10.460' AS DateTime), CAST(N'2021-07-18T19:44:20.343' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'33769193-30db-4923-8260-13c4006c4596', N'again004', N'123456', N'12323', N'2', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'15076455764', NULL, CAST(N'2021-07-18T17:15:47.887' AS DateTime), CAST(N'2021-07-18T17:15:47.887' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'08a0e2ac-14d7-4027-a338-9317f181c357', N'test005', N'123456', N'张三005', N'2', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'15076455764', NULL, CAST(N'2021-07-18T17:21:39.567' AS DateTime), CAST(N'2021-07-18T17:22:43.263' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'c25b5b7d-10d0-4eb0-b2ba-289060f762d9', N'test0061', N'123456', N'小小61', N'1', N'111', N'00000000-0000-0000-0000-000000000000', N'0310-5485335', N'15076455762', N'284635130@qq.com', CAST(N'2021-07-18T19:23:22.457' AS DateTime), CAST(N'2021-07-18T19:28:20.023' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'ab7309ca-aca7-4e22-9478-fa14d29dedd6', N'small0071', N'123456', N'小小71', N'2', N'职称', N'00000000-0000-0000-0000-000000000000', N'0310-5485335', N'15076455764', N'284635130@qq.com', CAST(N'2021-07-18T19:37:09.977' AS DateTime), CAST(N'2021-07-18T19:38:13.603' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'11f222a0-f0f3-48e5-a9d5-dd1ff01c0878', N'admin0019', N'123456', N'iiiii', N'2', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'13245678907', NULL, CAST(N'2021-07-18T19:43:20.970' AS DateTime), CAST(N'2021-07-18T19:43:20.970' AS DateTime), N'0')
INSERT [dbo].[T_User] ([UserId], [UserAccount], [UserPassword], [UserName], [UserSex], [UserTitles], [UserDept], [UserTelPhone], [UserMobilePhone], [UserEmail], [CreateTime], [LastUpdateTime], [IsDel]) VALUES (N'bc4de9c5-9be0-4db4-bb8b-b6e48ca1cc9d', N'admin008', N'123456', N'admin', N'2', NULL, N'00000000-0000-0000-0000-000000000000', NULL, N'15076455764', NULL, CAST(N'2021-07-20T20:50:38.380' AS DateTime), CAST(N'2021-07-20T20:50:38.380' AS DateTime), N'0')
GO
INSERT [dbo].[T_UserRole] ([UserId], [RoleId]) VALUES (N'61cf9ea3-72b2-4ddc-b2f1-017e1e2a7c86', N'236ba028-0ac5-4d8d-919c-5523ce38ace6')
INSERT [dbo].[T_UserRole] ([UserId], [RoleId]) VALUES (N'770bdefe-e756-4b64-8bdb-fcf1d38a2c87', N'236ba028-0ac5-4d8d-919c-5523ce38ace6')
GO
SET IDENTITY_INSERT [dbo].[T_VerificationCode] ON 

INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (1, 9053, CAST(N'2021-07-22T10:59:00.317' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (2, 2233, NULL)
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (3, 234234, NULL)
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (4, 2753, CAST(N'2021-07-22T11:02:23.840' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (5, 7541, CAST(N'2021-07-22T11:03:21.097' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (6, 7439, CAST(N'2021-07-22T11:03:41.577' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (7, 2839, CAST(N'2021-07-22T11:03:42.970' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (8, 5216, CAST(N'2021-07-22T13:57:01.333' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (9, 2272, CAST(N'2021-07-22T13:59:40.137' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (10, 8706, CAST(N'2021-07-22T13:59:43.157' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (11, 5170, CAST(N'2021-07-22T13:59:44.717' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (12, 1550, CAST(N'2021-07-22T14:05:35.050' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (13, 7798, CAST(N'2021-07-22T14:06:06.500' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (14, 6647, CAST(N'2021-07-22T14:08:23.657' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (15, 6627, CAST(N'2021-07-22T14:08:38.557' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (16, 6996, CAST(N'2021-07-22T14:09:29.330' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (17, 1932, CAST(N'2021-07-22T14:10:17.670' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (18, 7702, CAST(N'2021-07-22T14:12:59.357' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (19, 3260, CAST(N'2021-07-22T14:13:21.093' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (20, 9310, CAST(N'2021-07-22T14:13:37.527' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (21, 322, CAST(N'2021-07-22T14:13:44.620' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (22, 4543, CAST(N'2021-07-22T14:14:17.380' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (23, 8634, CAST(N'2021-07-22T14:42:51.700' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (24, 505, CAST(N'2021-07-22T14:43:04.443' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (25, 9290, CAST(N'2021-07-22T14:45:12.707' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (26, 6455, CAST(N'2021-07-22T14:46:41.457' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (27, 6318, CAST(N'2021-07-22T14:55:27.627' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (28, 2998, CAST(N'2021-07-22T14:57:43.010' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (29, 4993, CAST(N'2021-07-22T14:55:39.287' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (30, 3356, CAST(N'2021-07-22T14:59:28.057' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (31, 1748, CAST(N'2021-07-22T15:00:47.500' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (32, 4894, CAST(N'2021-07-22T15:04:46.177' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (33, 6707, CAST(N'2021-07-22T15:07:11.973' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (34, 6434, CAST(N'2021-07-22T15:11:00.503' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (35, 7297, CAST(N'2021-07-23T08:09:30.537' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (36, 7175, CAST(N'2021-07-23T08:13:48.760' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (37, 9978, CAST(N'2021-07-23T08:14:17.417' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (38, 5691, CAST(N'2021-07-23T08:16:54.643' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (39, 6094, CAST(N'2021-07-23T08:34:50.513' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (40, 3071, CAST(N'2021-07-23T08:35:00.467' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (41, 5981, CAST(N'2021-07-23T11:42:56.677' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (42, 2042, CAST(N'2021-07-23T11:43:25.710' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (43, 2491, CAST(N'2021-07-23T13:39:59.857' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (44, 4876, CAST(N'2021-07-23T13:39:59.853' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (45, 34, CAST(N'2021-07-23T13:40:41.810' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (46, 8677, CAST(N'2021-07-23T13:46:11.513' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (47, 4850, CAST(N'2021-07-23T13:51:32.177' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (48, 2862, CAST(N'2021-07-23T13:54:08.720' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (49, 3447, CAST(N'2021-07-23T13:55:36.720' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (50, 2431, CAST(N'2021-07-23T14:00:29.247' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (51, 8300, CAST(N'2021-07-23T14:02:01.107' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (52, 2338, CAST(N'2021-07-23T14:02:24.047' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (53, 3601, CAST(N'2021-07-23T14:13:21.783' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (54, 81, CAST(N'2021-07-23T14:22:45.933' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (55, 3894, CAST(N'2021-07-23T14:23:12.747' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (56, 1211, CAST(N'2021-07-23T14:36:45.003' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (57, 6326, CAST(N'2021-07-23T14:37:56.637' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (58, 9843, CAST(N'2021-07-23T14:40:24.650' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (59, 9403, CAST(N'2021-07-23T14:40:57.083' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (60, 9437, CAST(N'2021-07-23T14:41:21.053' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (61, 5337, CAST(N'2021-07-23T14:41:41.140' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (62, 8264, CAST(N'2021-07-23T14:43:58.023' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (63, 7369, CAST(N'2021-07-23T14:44:16.410' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (64, 5591, CAST(N'2021-07-23T14:44:46.290' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (65, 890, CAST(N'2021-07-23T14:45:47.513' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (66, 5774, CAST(N'2021-07-23T14:46:18.253' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (67, 352, CAST(N'2021-07-23T14:47:21.083' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (68, 5504, CAST(N'2021-07-23T14:50:09.007' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (69, 4322, CAST(N'2021-07-23T14:50:18.523' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (70, 1116, CAST(N'2021-07-23T14:54:41.200' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (71, 8243, CAST(N'2021-07-23T14:58:44.107' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (72, 3148, CAST(N'2021-07-23T15:05:37.353' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (73, 3661, CAST(N'2021-07-23T15:06:07.973' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (74, 1034, CAST(N'2021-07-23T15:06:57.683' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (75, 5015, CAST(N'2021-07-23T15:10:20.307' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (76, 4579, CAST(N'2021-07-23T15:10:44.903' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (77, 6868, CAST(N'2021-07-23T15:10:50.447' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (78, 3201, CAST(N'2021-07-23T15:10:54.443' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (79, 4189, CAST(N'2021-07-23T15:11:37.273' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (80, 5012, CAST(N'2021-07-23T15:11:40.883' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (81, 4719, CAST(N'2021-07-23T15:26:45.250' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (82, 311, CAST(N'2021-07-23T15:27:09.023' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (83, 3121, CAST(N'2021-07-23T15:27:40.277' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (84, 4363, CAST(N'2021-07-23T15:27:49.447' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (85, 8738, CAST(N'2021-07-23T15:28:15.087' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (86, 1838, CAST(N'2021-07-23T15:29:39.407' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (87, 2422, CAST(N'2021-07-23T15:31:20.867' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (88, 8140, CAST(N'2021-07-23T15:33:03.377' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (89, 8243, CAST(N'2021-07-23T15:36:52.973' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (90, 9971, CAST(N'2021-07-23T15:40:05.583' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (91, 4914, CAST(N'2021-07-23T15:41:45.287' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (92, 8261, CAST(N'2021-07-23T15:42:56.940' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (93, 7068, CAST(N'2021-07-23T15:45:54.297' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (94, 7258, CAST(N'2021-07-23T15:48:26.780' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (95, 8473, CAST(N'2021-07-23T15:48:41.657' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (96, 9272, CAST(N'2021-07-23T15:48:46.580' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (97, 7273, CAST(N'2021-07-23T15:49:11.780' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (98, 2749, CAST(N'2021-07-23T15:51:20.797' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (99, 3481, CAST(N'2021-07-23T15:51:28.257' AS DateTime))
GO
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (100, 5741, CAST(N'2021-07-23T15:52:01.783' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (101, 6322, CAST(N'2021-07-23T15:52:08.690' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (102, 5541, CAST(N'2021-07-23T15:52:43.460' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (103, 1138, CAST(N'2021-07-23T15:53:01.107' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (104, 4802, CAST(N'2021-07-23T15:53:10.673' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (105, 8408, CAST(N'2021-07-23T15:53:26.163' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (106, 2804, CAST(N'2021-07-23T15:53:34.227' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (107, 8820, CAST(N'2021-07-23T15:53:37.550' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (108, 5379, CAST(N'2021-07-23T15:54:21.367' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (109, 8593, CAST(N'2021-07-23T15:54:45.650' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (110, 9465, CAST(N'2021-07-23T15:55:46.500' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (111, 5949, CAST(N'2021-07-23T15:56:07.623' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (112, 8194, CAST(N'2021-07-23T15:57:18.970' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (113, 4734, CAST(N'2021-07-23T16:05:40.803' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (114, 1807, CAST(N'2021-07-23T16:06:13.323' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (115, 4085, CAST(N'2021-07-23T16:25:16.490' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (116, 3966, CAST(N'2021-07-23T16:28:40.640' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (117, 8306, CAST(N'2021-07-23T16:32:01.037' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (118, 7384, CAST(N'2021-07-23T16:38:11.427' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (119, 6041, CAST(N'2021-07-23T16:38:36.740' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (120, 7035, CAST(N'2021-07-23T16:39:10.437' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (121, 4812, CAST(N'2021-07-23T16:39:21.507' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (122, 4435, CAST(N'2021-07-23T16:40:22.120' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (123, 3149, CAST(N'2021-07-23T16:40:32.663' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (124, 9069, CAST(N'2021-07-23T16:40:49.537' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (125, 1570, CAST(N'2021-07-23T16:41:13.553' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (126, 5632, CAST(N'2021-07-23T17:21:58.100' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (127, 4587, CAST(N'2021-07-23T17:22:26.027' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (128, 3397, CAST(N'2021-07-23T17:22:56.293' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (129, 9595, CAST(N'2021-07-26T08:01:55.797' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (130, 1490, CAST(N'2021-07-26T08:13:21.197' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (131, 1143, CAST(N'2021-07-26T08:14:02.020' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (132, 3259, CAST(N'2021-07-26T08:14:15.567' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (133, 8434, CAST(N'2021-07-26T08:15:09.597' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (134, 4890, CAST(N'2021-07-26T15:02:52.417' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (135, 7080, CAST(N'2021-07-26T17:41:49.547' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (136, 1156, CAST(N'2021-07-26T17:43:07.127' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (137, 1390, CAST(N'2021-07-26T17:43:20.333' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (138, 2409, CAST(N'2021-07-26T17:43:26.527' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (139, 3707, CAST(N'2021-07-26T17:46:29.223' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (140, 9926, CAST(N'2021-07-26T17:46:32.433' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (141, 2883, CAST(N'2021-07-26T17:46:44.920' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (142, 4835, CAST(N'2021-07-26T17:47:10.273' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (143, 4934, CAST(N'2021-07-26T17:47:51.863' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (144, 6185, CAST(N'2021-07-26T17:49:20.137' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (145, 5643, CAST(N'2021-07-26T17:51:29.320' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (146, 2030, CAST(N'2021-07-26T17:53:23.247' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (147, 3340, CAST(N'2021-07-26T17:56:53.023' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (148, 1876, CAST(N'2021-07-26T17:59:28.347' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (149, 7996, CAST(N'2021-07-26T17:59:34.997' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (150, 965, CAST(N'2021-07-26T17:59:47.547' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (151, 3348, CAST(N'2021-07-26T18:00:04.987' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (152, 5210, CAST(N'2021-07-26T18:04:45.517' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (153, 1303, CAST(N'2021-07-26T18:05:09.350' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (154, 2350, CAST(N'2021-07-27T08:02:02.190' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (155, 6487, CAST(N'2021-07-27T08:08:28.080' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (156, 6910, CAST(N'2021-07-27T08:30:45.063' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (157, 4693, CAST(N'2021-07-27T08:41:27.610' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (158, 114, CAST(N'2021-07-27T08:57:11.437' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (159, 9175, CAST(N'2021-07-27T08:58:05.443' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (160, 8608, CAST(N'2021-07-27T09:03:06.787' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (161, 2657, CAST(N'2021-07-27T09:26:07.433' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (162, 5741, CAST(N'2021-07-27T09:26:37.070' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (163, 9874, CAST(N'2021-07-27T09:29:41.743' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (164, 7296, CAST(N'2021-07-27T09:29:45.557' AS DateTime))
INSERT [dbo].[T_VerificationCode] ([id], [number], [time]) VALUES (165, 951, CAST(N'2021-07-27T09:30:09.300' AS DateTime))
SET IDENTITY_INSERT [dbo].[T_VerificationCode] OFF
GO
/****** Object:  Index [PK_T_DEPT]    Script Date: 2021/7/27 9:33:58 ******/
ALTER TABLE [dbo].[T_Dept] ADD  CONSTRAINT [PK_T_DEPT] PRIMARY KEY NONCLUSTERED 
(
	[DeptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_T_MENU]    Script Date: 2021/7/27 9:33:58 ******/
ALTER TABLE [dbo].[T_Menu] ADD  CONSTRAINT [PK_T_MENU] PRIMARY KEY NONCLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_T_ROLEMANAGE]    Script Date: 2021/7/27 9:33:58 ******/
ALTER TABLE [dbo].[T_RoleManage] ADD  CONSTRAINT [PK_T_ROLEMANAGE] PRIMARY KEY NONCLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_T_USER]    Script Date: 2021/7/27 9:33:58 ******/
ALTER TABLE [dbo].[T_User] ADD  CONSTRAINT [PK_T_USER] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionary', @level2type=N'COLUMN',@level2name=N'id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionary', @level2type=N'COLUMN',@level2name=N'dicName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionary', @level2type=N'COLUMN',@level2name=N'dicCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字典类型（1:文字    2:范围）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionary', @level2type=N'COLUMN',@level2name=N'dicType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionary', @level2type=N'COLUMN',@level2name=N'dicSort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已删除（1：未删  0：已删）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionary', @level2type=N'COLUMN',@level2name=N'isDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据字典' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级字典ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'dicPid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子集字典名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'dicName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子集字典编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'dicCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最小值（当字典类型为范围时）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'minValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大值（当字典类型为范围时）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'maxValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'dicSort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已删除（0：未删 1：已删）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'isDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'createTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild', @level2type=N'COLUMN',@level2name=N'lastUpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据字典子集' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_DataDictionaryChild'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dept', @level2type=N'COLUMN',@level2name=N'DeptId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dept', @level2type=N'COLUMN',@level2name=N'DeptName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dept', @level2type=N'COLUMN',@level2name=N'DeptCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dept', @level2type=N'COLUMN',@level2name=N'DeptSort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级部门(一级部门父ID为0)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dept', @level2type=N'COLUMN',@level2name=N'DeptPid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dept', @level2type=N'COLUMN',@level2name=N'LastUpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已删除（0：未删  1：已删）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dept', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Dept'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'MenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'MenuCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'URL' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'MenuUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'MenuTarget'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'MenuSort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'MenuPid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分类（目录1、功能2）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'MenuType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目录图标类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'IconClass'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'LastUpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已删除（0：未删除 1：已删除）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单管理' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_Menu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleManage', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleManage', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleManage', @level2type=N'COLUMN',@level2name=N'RoleCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleManage', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleManage', @level2type=N'COLUMN',@level2name=N'LastUpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否删除（0：未删 1：已删）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleManage', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色管理' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleManage'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleMenu', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleMenu', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_RoleMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'帐号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码(md5加密)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserPassword'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别(1男2女)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserSex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserTitles'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserDept'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'办公电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserTelPhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserMobilePhone'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'E-mail' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'UserEmail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'LastUpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否已删除（0：未删  1：已删）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User', @level2type=N'COLUMN',@level2name=N'IsDel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台_用户表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_User'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_UserRole', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_UserRole', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台_用户角色表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'T_UserRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "trm"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 126
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tm"
            Begin Extent = 
               Top = 7
               Left = 257
               Bottom = 170
               Right = 471
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_RoleMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_RoleMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "u"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_User'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_User'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ur"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 126
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "trm"
            Begin Extent = 
               Top = 7
               Left = 257
               Bottom = 170
               Right = 471
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_UserRoleInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_UserRoleInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ur"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 126
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rm"
            Begin Extent = 
               Top = 7
               Left = 257
               Bottom = 126
               Right = 418
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "m"
            Begin Extent = 
               Top = 7
               Left = 466
               Bottom = 170
               Right = 680
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_UserRoleMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'V_UserRoleMenu'
GO
USE [master]
GO
ALTER DATABASE [TalentBigData] SET  READ_WRITE 
GO
