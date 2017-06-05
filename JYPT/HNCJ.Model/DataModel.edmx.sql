
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/21/2017 15:57:10
-- Generated from EDMX file: G:\Chenbin\修改\HNCJ.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NEW];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionInfo] DROP CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoOrderInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderInfo] DROP CONSTRAINT [FK_UserInfoOrderInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoGoodsInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GoodsInfo] DROP CONSTRAINT [FK_UserInfoGoodsInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_GoodsTypeGoodsInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GoodsInfo] DROP CONSTRAINT [FK_GoodsTypeGoodsInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_WFInstanceFileInfo_WFInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WFInstanceFileInfo] DROP CONSTRAINT [FK_WFInstanceFileInfo_WFInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_WFInstanceFileInfo_FileInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WFInstanceFileInfo] DROP CONSTRAINT [FK_WFInstanceFileInfo_FileInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_WFTempWFInstance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WFInstance] DROP CONSTRAINT [FK_WFTempWFInstance];
GO
IF OBJECT_ID(N'[dbo].[FK_WFInstanceWFStep]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WFStep] DROP CONSTRAINT [FK_WFInstanceWFStep];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoAddressInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AddressInfo] DROP CONSTRAINT [FK_UserInfoAddressInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetailInfoOrderInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetailInfo] DROP CONSTRAINT [FK_OrderDetailInfoOrderInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderDetailInfoGoodsInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderDetailInfo] DROP CONSTRAINT [FK_OrderDetailInfoGoodsInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_CartInfoUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartInfo] DROP CONSTRAINT [FK_CartInfoUserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_CartInfoGoodsInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CartInfo] DROP CONSTRAINT [FK_CartInfoGoodsInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_PaymentDeals]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Deals] DROP CONSTRAINT [FK_PaymentDeals];
GO
IF OBJECT_ID(N'[dbo].[FK_DeptUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_DeptUserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoActionInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoActionInfo] DROP CONSTRAINT [FK_UserInfoActionInfo_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoActionInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoActionInfo] DROP CONSTRAINT [FK_UserInfoActionInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoActionItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionItem] DROP CONSTRAINT [FK_ActionInfoActionItem];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoActionItem_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoActionItem] DROP CONSTRAINT [FK_UserInfoActionItem_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoActionItem_ActionItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoActionItem] DROP CONSTRAINT [FK_UserInfoActionItem_ActionItem];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionItem_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionItem] DROP CONSTRAINT [FK_RoleInfoActionItem_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoActionItem_ActionItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleInfoActionItem] DROP CONSTRAINT [FK_RoleInfoActionItem_ActionItem];
GO
IF OBJECT_ID(N'[dbo].[FK_GoodsInfoComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_GoodsInfoComment];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[OrderInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderInfo];
GO
IF OBJECT_ID(N'[dbo].[GoodsInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GoodsInfo];
GO
IF OBJECT_ID(N'[dbo].[GoodsType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GoodsType];
GO
IF OBJECT_ID(N'[dbo].[LinksInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LinksInfo];
GO
IF OBJECT_ID(N'[dbo].[FocusPicture]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FocusPicture];
GO
IF OBJECT_ID(N'[dbo].[Freight]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Freight];
GO
IF OBJECT_ID(N'[dbo].[Payment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payment];
GO
IF OBJECT_ID(N'[dbo].[WFTemp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WFTemp];
GO
IF OBJECT_ID(N'[dbo].[WFInstance]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WFInstance];
GO
IF OBJECT_ID(N'[dbo].[FileInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileInfo];
GO
IF OBJECT_ID(N'[dbo].[WFStep]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WFStep];
GO
IF OBJECT_ID(N'[dbo].[AddressInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AddressInfo];
GO
IF OBJECT_ID(N'[dbo].[LoginLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginLog];
GO
IF OBJECT_ID(N'[dbo].[CartInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CartInfo];
GO
IF OBJECT_ID(N'[dbo].[OrderDetailInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderDetailInfo];
GO
IF OBJECT_ID(N'[dbo].[Deals]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Deals];
GO
IF OBJECT_ID(N'[dbo].[Dept]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dept];
GO
IF OBJECT_ID(N'[dbo].[SysLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SysLog];
GO
IF OBJECT_ID(N'[dbo].[ActionItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionItem];
GO
IF OBJECT_ID(N'[dbo].[Comment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comment];
GO
IF OBJECT_ID(N'[dbo].[UserInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoRoleInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfoActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfoActionInfo];
GO
IF OBJECT_ID(N'[dbo].[WFInstanceFileInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WFInstanceFileInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoActionInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoActionItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoActionItem];
GO
IF OBJECT_ID(N'[dbo].[RoleInfoActionItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfoActionItem];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [salt] nvarchar(10)  NULL,
    [UserPwd] nvarchar(64)  NULL,
    [Avatar] nvarchar(256)  NULL,
    [NickName] nvarchar(100)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [IsSys] bit  NULL,
    [DeptID] int  NOT NULL,
    [State] bit  NULL,
    [Moblie] nvarchar(11)  NULL,
    [Email] nvarchar(50)  NULL,
    [Sex] nvarchar(2)  NULL,
    [Birthday] datetime  NULL,
    [Telphone] nvarchar(11)  NULL,
    [area] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [qq] nvarchar(20)  NULL,
    [Amount] decimal(10,2)  NULL,
    [Point] int  NULL,
    [Description] nvarchar(256)  NULL,
    [DeptName] nvarchar(64)  NULL
);
GO

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ActionName] nvarchar(50)  NULL,
    [ParentName] nvarchar(50)  NULL,
    [DelFlag] bit  NOT NULL,
    [RegTime] datetime  NOT NULL,
    [ModfiedOn] datetime  NOT NULL,
    [Url] nvarchar(max)  NULL,
    [HttpMethd] nvarchar(max)  NULL,
    [IsMenu] bit  NOT NULL,
    [MenuIcon] nvarchar(max)  NULL,
    [Sort] nvarchar(max)  NULL,
    [Description] nvarchar(256)  NULL,
    [ParentId] int  NULL
);
GO

-- Creating table 'RoleInfo'
CREATE TABLE [dbo].[RoleInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(50)  NULL,
    [RoleNo] nvarchar(50)  NULL,
    [State] bit  NULL,
    [Description] nvarchar(256)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL
);
GO

-- Creating table 'OrderInfo'
CREATE TABLE [dbo].[OrderInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [OrderName] nvarchar(64)  NULL,
    [Content] nvarchar(256)  NULL,
    [Address] nvarchar(128)  NULL,
    [Status] int  NULL,
    [Money] decimal(10,2)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [DeliverTime] datetime  NULL,
    [SuccessTime] datetime  NULL,
    [UserInfoID] int  NOT NULL
);
GO

-- Creating table 'GoodsInfo'
CREATE TABLE [dbo].[GoodsInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GoodsName] nvarchar(64)  NULL,
    [GoodsPrice] decimal(10,2)  NULL,
    [GoodsRemark] nvarchar(max)  NULL,
    [GoodsNum] int  NULL,
    [ImageMenu] nvarchar(256)  NULL,
    [GoodsAddress] nvarchar(max)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [UserInfoID] int  NOT NULL,
    [GoodsTypeID] int  NOT NULL,
    [Status] int  NULL,
    [Point] int  NOT NULL
);
GO

-- Creating table 'GoodsType'
CREATE TABLE [dbo].[GoodsType] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TypeName] nvarchar(32)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [English] nvarchar(64)  NULL,
    [Url] nvarchar(64)  NULL,
    [NodeID] int  NOT NULL
);
GO

-- Creating table 'LinksInfo'
CREATE TABLE [dbo].[LinksInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(32)  NULL,
    [Url] nvarchar(64)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL
);
GO

-- Creating table 'FocusPicture'
CREATE TABLE [dbo].[FocusPicture] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(64)  NULL,
    [ImagePath] nvarchar(256)  NULL,
    [Url] nvarchar(64)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL
);
GO

-- Creating table 'Freight'
CREATE TABLE [dbo].[Freight] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FreightName] nvarchar(64)  NULL,
    [FreightPrice] decimal(18,0)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL
);
GO

-- Creating table 'Payment'
CREATE TABLE [dbo].[Payment] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PaymentName] nvarchar(64)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [APIURL] nvarchar(64)  NULL
);
GO

-- Creating table 'WFTemp'
CREATE TABLE [dbo].[WFTemp] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TempName] nvarchar(32)  NULL,
    [Description] nvarchar(max)  NULL,
    [TempForm] nvarchar(max)  NULL,
    [Remark] nvarchar(256)  NULL,
    [DelFlag] bit  NULL,
    [SubTime] datetime  NULL,
    [ActivityType] nvarchar(32)  NULL
);
GO

-- Creating table 'WFInstance'
CREATE TABLE [dbo].[WFInstance] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [InstanceName] nvarchar(128)  NOT NULL,
    [StartBy] int  NOT NULL,
    [StartTime] datetime  NOT NULL,
    [Level] smallint  NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(256)  NULL,
    [DelFlag] bit  NULL,
    [WFTempID] int  NOT NULL,
    [WFInstanceId] uniqueidentifier  NOT NULL,
    [Status] smallint  NULL
);
GO

-- Creating table 'FileInfo'
CREATE TABLE [dbo].[FileInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(128)  NULL,
    [FileType] nvarchar(64)  NULL,
    [FilePath] nvarchar(256)  NULL,
    [FileSize] nvarchar(32)  NULL,
    [DelFlag] bit  NULL,
    [Remark] nvarchar(256)  NULL,
    [SubTime] datetime  NULL
);
GO

-- Creating table 'WFStep'
CREATE TABLE [dbo].[WFStep] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [StepName] nvarchar(64)  NOT NULL,
    [ProcessBy] int  NOT NULL,
    [SubTime] datetime  NULL,
    [ProcessTime] datetime  NULL,
    [ProcessResult] nvarchar(128)  NULL,
    [ProcessComment] nvarchar(256)  NULL,
    [StepStatus] smallint  NULL,
    [IsStartStep] bit  NULL,
    [IsEndStep] bit  NULL,
    [ParentStepId] int  NULL,
    [WFInstanceID] int  NOT NULL
);
GO

-- Creating table 'AddressInfo'
CREATE TABLE [dbo].[AddressInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Consignee] nvarchar(64)  NOT NULL,
    [Telphone] nvarchar(11)  NOT NULL,
    [DetailedAddress] nvarchar(128)  NULL,
    [IsDefault] bit  NOT NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [UserInfoID] int  NOT NULL,
    [PostCode] nvarchar(6)  NULL,
    [Province] nvarchar(64)  NULL,
    [City] nvarchar(64)  NULL,
    [Area] nvarchar(64)  NULL
);
GO

-- Creating table 'LoginLog'
CREATE TABLE [dbo].[LoginLog] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [LoginTime] datetime  NULL,
    [IP] nvarchar(20)  NULL,
    [UserInfoID] int  NOT NULL,
    [DelFlag] bit  NULL
);
GO

-- Creating table 'CartInfo'
CREATE TABLE [dbo].[CartInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GoodsNum] int  NULL,
    [GoodsPrice] decimal(10,2)  NULL,
    [Status] int  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [UserInfoID] int  NOT NULL,
    [GoodsInfoID] int  NOT NULL
);
GO

-- Creating table 'OrderDetailInfo'
CREATE TABLE [dbo].[OrderDetailInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GoodsNum] int  NULL,
    [GoodsPrice] decimal(10,2)  NULL,
    [RegTime] datetime  NULL,
    [DelFlag] bit  NULL,
    [OrderInfoID] int  NOT NULL,
    [GoodsInfoID] int  NOT NULL
);
GO

-- Creating table 'Deals'
CREATE TABLE [dbo].[Deals] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [pay_order_no] nvarchar(32)  NULL,
    [pay_order_amount] decimal(10,2)  NULL,
    [pay_user_name] nvarchar(64)  NULL,
    [PaymentID] int  NOT NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL
);
GO

-- Creating table 'Dept'
CREATE TABLE [dbo].[Dept] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [deptno] nvarchar(32)  NULL,
    [deptname] nvarchar(64)  NULL,
    [ParentName] nvarchar(64)  NULL,
    [deptmanager] nvarchar(64)  NULL,
    [deptmanager_id] int  NULL,
    [telphone] nvarchar(11)  NULL,
    [remark] nvarchar(256)  NULL,
    [DelFlag] bit  NULL,
    [RegTime] datetime  NULL,
    [ModfiedOn] datetime  NULL,
    [ParentId] int  NULL
);
GO

-- Creating table 'SysLog'
CREATE TABLE [dbo].[SysLog] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Operation] nvarchar(50)  NULL,
    [OperationTime] datetime  NULL,
    [DelFlag] bit  NULL
);
GO

-- Creating table 'ActionItem'
CREATE TABLE [dbo].[ActionItem] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ActionItemName] nvarchar(64)  NULL,
    [ActionItemNo] nvarchar(50)  NULL,
    [Sort] nvarchar(10)  NULL,
    [DelFlag] bit  NOT NULL,
    [RegTime] datetime  NOT NULL,
    [ModfiedOn] datetime  NOT NULL,
    [ActionInfoID] int  NOT NULL
);
GO

-- Creating table 'Comment'
CREATE TABLE [dbo].[Comment] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(256)  NOT NULL,
    [UserImg] nvarchar(256)  NOT NULL,
    [UserName] nvarchar(64)  NOT NULL,
    [CreateTime] datetime  NOT NULL,
    [GoodsInfoID] int  NOT NULL
);
GO

-- Creating table 'UserInfoRoleInfo'
CREATE TABLE [dbo].[UserInfoRoleInfo] (
    [UserInfo_ID] int  NOT NULL,
    [RoleInfo_ID] int  NOT NULL
);
GO

-- Creating table 'RoleInfoActionInfo'
CREATE TABLE [dbo].[RoleInfoActionInfo] (
    [RoleInfo_ID] int  NOT NULL,
    [ActionInfo_ID] int  NOT NULL
);
GO

-- Creating table 'WFInstanceFileInfo'
CREATE TABLE [dbo].[WFInstanceFileInfo] (
    [WFInstance_ID] int  NOT NULL,
    [FileInfo_ID] int  NOT NULL
);
GO

-- Creating table 'UserInfoActionInfo'
CREATE TABLE [dbo].[UserInfoActionInfo] (
    [UserInfo_ID] int  NOT NULL,
    [ActionInfo_ID] int  NOT NULL
);
GO

-- Creating table 'UserInfoActionItem'
CREATE TABLE [dbo].[UserInfoActionItem] (
    [UserInfo_ID] int  NOT NULL,
    [ActionItem_ID] int  NOT NULL
);
GO

-- Creating table 'RoleInfoActionItem'
CREATE TABLE [dbo].[RoleInfoActionItem] (
    [RoleInfo_ID] int  NOT NULL,
    [ActionItem_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActionInfo'
ALTER TABLE [dbo].[ActionInfo]
ADD CONSTRAINT [PK_ActionInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'RoleInfo'
ALTER TABLE [dbo].[RoleInfo]
ADD CONSTRAINT [PK_RoleInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'OrderInfo'
ALTER TABLE [dbo].[OrderInfo]
ADD CONSTRAINT [PK_OrderInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodsInfo'
ALTER TABLE [dbo].[GoodsInfo]
ADD CONSTRAINT [PK_GoodsInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodsType'
ALTER TABLE [dbo].[GoodsType]
ADD CONSTRAINT [PK_GoodsType]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'LinksInfo'
ALTER TABLE [dbo].[LinksInfo]
ADD CONSTRAINT [PK_LinksInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'FocusPicture'
ALTER TABLE [dbo].[FocusPicture]
ADD CONSTRAINT [PK_FocusPicture]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Freight'
ALTER TABLE [dbo].[Freight]
ADD CONSTRAINT [PK_Freight]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Payment'
ALTER TABLE [dbo].[Payment]
ADD CONSTRAINT [PK_Payment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WFTemp'
ALTER TABLE [dbo].[WFTemp]
ADD CONSTRAINT [PK_WFTemp]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WFInstance'
ALTER TABLE [dbo].[WFInstance]
ADD CONSTRAINT [PK_WFInstance]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'FileInfo'
ALTER TABLE [dbo].[FileInfo]
ADD CONSTRAINT [PK_FileInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WFStep'
ALTER TABLE [dbo].[WFStep]
ADD CONSTRAINT [PK_WFStep]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'AddressInfo'
ALTER TABLE [dbo].[AddressInfo]
ADD CONSTRAINT [PK_AddressInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'LoginLog'
ALTER TABLE [dbo].[LoginLog]
ADD CONSTRAINT [PK_LoginLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CartInfo'
ALTER TABLE [dbo].[CartInfo]
ADD CONSTRAINT [PK_CartInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'OrderDetailInfo'
ALTER TABLE [dbo].[OrderDetailInfo]
ADD CONSTRAINT [PK_OrderDetailInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Deals'
ALTER TABLE [dbo].[Deals]
ADD CONSTRAINT [PK_Deals]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Dept'
ALTER TABLE [dbo].[Dept]
ADD CONSTRAINT [PK_Dept]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'SysLog'
ALTER TABLE [dbo].[SysLog]
ADD CONSTRAINT [PK_SysLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActionItem'
ALTER TABLE [dbo].[ActionItem]
ADD CONSTRAINT [PK_ActionItem]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [PK_Comment]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserInfo_ID], [RoleInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [PK_UserInfoRoleInfo]
    PRIMARY KEY NONCLUSTERED ([UserInfo_ID], [RoleInfo_ID] ASC);
GO

-- Creating primary key on [RoleInfo_ID], [ActionInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [PK_RoleInfoActionInfo]
    PRIMARY KEY NONCLUSTERED ([RoleInfo_ID], [ActionInfo_ID] ASC);
GO

-- Creating primary key on [WFInstance_ID], [FileInfo_ID] in table 'WFInstanceFileInfo'
ALTER TABLE [dbo].[WFInstanceFileInfo]
ADD CONSTRAINT [PK_WFInstanceFileInfo]
    PRIMARY KEY NONCLUSTERED ([WFInstance_ID], [FileInfo_ID] ASC);
GO

-- Creating primary key on [UserInfo_ID], [ActionInfo_ID] in table 'UserInfoActionInfo'
ALTER TABLE [dbo].[UserInfoActionInfo]
ADD CONSTRAINT [PK_UserInfoActionInfo]
    PRIMARY KEY NONCLUSTERED ([UserInfo_ID], [ActionInfo_ID] ASC);
GO

-- Creating primary key on [UserInfo_ID], [ActionItem_ID] in table 'UserInfoActionItem'
ALTER TABLE [dbo].[UserInfoActionItem]
ADD CONSTRAINT [PK_UserInfoActionItem]
    PRIMARY KEY NONCLUSTERED ([UserInfo_ID], [ActionItem_ID] ASC);
GO

-- Creating primary key on [RoleInfo_ID], [ActionItem_ID] in table 'RoleInfoActionItem'
ALTER TABLE [dbo].[RoleInfoActionItem]
ADD CONSTRAINT [PK_RoleInfoActionItem]
    PRIMARY KEY NONCLUSTERED ([RoleInfo_ID], [ActionItem_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_UserInfo]
    FOREIGN KEY ([UserInfo_ID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleInfo_ID] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_ID])
    REFERENCES [dbo].[RoleInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRoleInfo_RoleInfo'
CREATE INDEX [IX_FK_UserInfoRoleInfo_RoleInfo]
ON [dbo].[UserInfoRoleInfo]
    ([RoleInfo_ID]);
GO

-- Creating foreign key on [RoleInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_ID])
    REFERENCES [dbo].[RoleInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionInfo_ID] in table 'RoleInfoActionInfo'
ALTER TABLE [dbo].[RoleInfoActionInfo]
ADD CONSTRAINT [FK_RoleInfoActionInfo_ActionInfo]
    FOREIGN KEY ([ActionInfo_ID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleInfoActionInfo_ActionInfo'
CREATE INDEX [IX_FK_RoleInfoActionInfo_ActionInfo]
ON [dbo].[RoleInfoActionInfo]
    ([ActionInfo_ID]);
GO

-- Creating foreign key on [UserInfoID] in table 'OrderInfo'
ALTER TABLE [dbo].[OrderInfo]
ADD CONSTRAINT [FK_UserInfoOrderInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoOrderInfo'
CREATE INDEX [IX_FK_UserInfoOrderInfo]
ON [dbo].[OrderInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [UserInfoID] in table 'GoodsInfo'
ALTER TABLE [dbo].[GoodsInfo]
ADD CONSTRAINT [FK_UserInfoGoodsInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoGoodsInfo'
CREATE INDEX [IX_FK_UserInfoGoodsInfo]
ON [dbo].[GoodsInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [GoodsTypeID] in table 'GoodsInfo'
ALTER TABLE [dbo].[GoodsInfo]
ADD CONSTRAINT [FK_GoodsTypeGoodsInfo]
    FOREIGN KEY ([GoodsTypeID])
    REFERENCES [dbo].[GoodsType]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodsTypeGoodsInfo'
CREATE INDEX [IX_FK_GoodsTypeGoodsInfo]
ON [dbo].[GoodsInfo]
    ([GoodsTypeID]);
GO

-- Creating foreign key on [WFInstance_ID] in table 'WFInstanceFileInfo'
ALTER TABLE [dbo].[WFInstanceFileInfo]
ADD CONSTRAINT [FK_WFInstanceFileInfo_WFInstance]
    FOREIGN KEY ([WFInstance_ID])
    REFERENCES [dbo].[WFInstance]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FileInfo_ID] in table 'WFInstanceFileInfo'
ALTER TABLE [dbo].[WFInstanceFileInfo]
ADD CONSTRAINT [FK_WFInstanceFileInfo_FileInfo]
    FOREIGN KEY ([FileInfo_ID])
    REFERENCES [dbo].[FileInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WFInstanceFileInfo_FileInfo'
CREATE INDEX [IX_FK_WFInstanceFileInfo_FileInfo]
ON [dbo].[WFInstanceFileInfo]
    ([FileInfo_ID]);
GO

-- Creating foreign key on [WFTempID] in table 'WFInstance'
ALTER TABLE [dbo].[WFInstance]
ADD CONSTRAINT [FK_WFTempWFInstance]
    FOREIGN KEY ([WFTempID])
    REFERENCES [dbo].[WFTemp]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WFTempWFInstance'
CREATE INDEX [IX_FK_WFTempWFInstance]
ON [dbo].[WFInstance]
    ([WFTempID]);
GO

-- Creating foreign key on [WFInstanceID] in table 'WFStep'
ALTER TABLE [dbo].[WFStep]
ADD CONSTRAINT [FK_WFInstanceWFStep]
    FOREIGN KEY ([WFInstanceID])
    REFERENCES [dbo].[WFInstance]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_WFInstanceWFStep'
CREATE INDEX [IX_FK_WFInstanceWFStep]
ON [dbo].[WFStep]
    ([WFInstanceID]);
GO

-- Creating foreign key on [UserInfoID] in table 'AddressInfo'
ALTER TABLE [dbo].[AddressInfo]
ADD CONSTRAINT [FK_UserInfoAddressInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoAddressInfo'
CREATE INDEX [IX_FK_UserInfoAddressInfo]
ON [dbo].[AddressInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [OrderInfoID] in table 'OrderDetailInfo'
ALTER TABLE [dbo].[OrderDetailInfo]
ADD CONSTRAINT [FK_OrderDetailInfoOrderInfo]
    FOREIGN KEY ([OrderInfoID])
    REFERENCES [dbo].[OrderInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetailInfoOrderInfo'
CREATE INDEX [IX_FK_OrderDetailInfoOrderInfo]
ON [dbo].[OrderDetailInfo]
    ([OrderInfoID]);
GO

-- Creating foreign key on [GoodsInfoID] in table 'OrderDetailInfo'
ALTER TABLE [dbo].[OrderDetailInfo]
ADD CONSTRAINT [FK_OrderDetailInfoGoodsInfo]
    FOREIGN KEY ([GoodsInfoID])
    REFERENCES [dbo].[GoodsInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderDetailInfoGoodsInfo'
CREATE INDEX [IX_FK_OrderDetailInfoGoodsInfo]
ON [dbo].[OrderDetailInfo]
    ([GoodsInfoID]);
GO

-- Creating foreign key on [UserInfoID] in table 'CartInfo'
ALTER TABLE [dbo].[CartInfo]
ADD CONSTRAINT [FK_CartInfoUserInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CartInfoUserInfo'
CREATE INDEX [IX_FK_CartInfoUserInfo]
ON [dbo].[CartInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [GoodsInfoID] in table 'CartInfo'
ALTER TABLE [dbo].[CartInfo]
ADD CONSTRAINT [FK_CartInfoGoodsInfo]
    FOREIGN KEY ([GoodsInfoID])
    REFERENCES [dbo].[GoodsInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CartInfoGoodsInfo'
CREATE INDEX [IX_FK_CartInfoGoodsInfo]
ON [dbo].[CartInfo]
    ([GoodsInfoID]);
GO

-- Creating foreign key on [PaymentID] in table 'Deals'
ALTER TABLE [dbo].[Deals]
ADD CONSTRAINT [FK_PaymentDeals]
    FOREIGN KEY ([PaymentID])
    REFERENCES [dbo].[Payment]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PaymentDeals'
CREATE INDEX [IX_FK_PaymentDeals]
ON [dbo].[Deals]
    ([PaymentID]);
GO

-- Creating foreign key on [DeptID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [FK_DeptUserInfo]
    FOREIGN KEY ([DeptID])
    REFERENCES [dbo].[Dept]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DeptUserInfo'
CREATE INDEX [IX_FK_DeptUserInfo]
ON [dbo].[UserInfo]
    ([DeptID]);
GO

-- Creating foreign key on [UserInfo_ID] in table 'UserInfoActionInfo'
ALTER TABLE [dbo].[UserInfoActionInfo]
ADD CONSTRAINT [FK_UserInfoActionInfo_UserInfo]
    FOREIGN KEY ([UserInfo_ID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionInfo_ID] in table 'UserInfoActionInfo'
ALTER TABLE [dbo].[UserInfoActionInfo]
ADD CONSTRAINT [FK_UserInfoActionInfo_ActionInfo]
    FOREIGN KEY ([ActionInfo_ID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoActionInfo_ActionInfo'
CREATE INDEX [IX_FK_UserInfoActionInfo_ActionInfo]
ON [dbo].[UserInfoActionInfo]
    ([ActionInfo_ID]);
GO

-- Creating foreign key on [ActionInfoID] in table 'ActionItem'
ALTER TABLE [dbo].[ActionItem]
ADD CONSTRAINT [FK_ActionInfoActionItem]
    FOREIGN KEY ([ActionInfoID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoActionItem'
CREATE INDEX [IX_FK_ActionInfoActionItem]
ON [dbo].[ActionItem]
    ([ActionInfoID]);
GO

-- Creating foreign key on [UserInfo_ID] in table 'UserInfoActionItem'
ALTER TABLE [dbo].[UserInfoActionItem]
ADD CONSTRAINT [FK_UserInfoActionItem_UserInfo]
    FOREIGN KEY ([UserInfo_ID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionItem_ID] in table 'UserInfoActionItem'
ALTER TABLE [dbo].[UserInfoActionItem]
ADD CONSTRAINT [FK_UserInfoActionItem_ActionItem]
    FOREIGN KEY ([ActionItem_ID])
    REFERENCES [dbo].[ActionItem]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoActionItem_ActionItem'
CREATE INDEX [IX_FK_UserInfoActionItem_ActionItem]
ON [dbo].[UserInfoActionItem]
    ([ActionItem_ID]);
GO

-- Creating foreign key on [RoleInfo_ID] in table 'RoleInfoActionItem'
ALTER TABLE [dbo].[RoleInfoActionItem]
ADD CONSTRAINT [FK_RoleInfoActionItem_RoleInfo]
    FOREIGN KEY ([RoleInfo_ID])
    REFERENCES [dbo].[RoleInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionItem_ID] in table 'RoleInfoActionItem'
ALTER TABLE [dbo].[RoleInfoActionItem]
ADD CONSTRAINT [FK_RoleInfoActionItem_ActionItem]
    FOREIGN KEY ([ActionItem_ID])
    REFERENCES [dbo].[ActionItem]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleInfoActionItem_ActionItem'
CREATE INDEX [IX_FK_RoleInfoActionItem_ActionItem]
ON [dbo].[RoleInfoActionItem]
    ([ActionItem_ID]);
GO

-- Creating foreign key on [GoodsInfoID] in table 'Comment'
ALTER TABLE [dbo].[Comment]
ADD CONSTRAINT [FK_GoodsInfoComment]
    FOREIGN KEY ([GoodsInfoID])
    REFERENCES [dbo].[GoodsInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodsInfoComment'
CREATE INDEX [IX_FK_GoodsInfoComment]
ON [dbo].[Comment]
    ([GoodsInfoID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------