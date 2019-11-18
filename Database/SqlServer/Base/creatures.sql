/*
 Navicat Premium Data Transfer

 Source Server         : SQL Server Locale
 Source Server Type    : SQL Server
 Source Server Version : 14001000
 Source Host           : localhost\SQLEXPRESS:1433
 Source Catalog        : ellerealtime
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 14001000
 File Encoding         : 65001

 Date: 18/11/2019 19:08:38
*/


-- ----------------------------
-- Table structure for creatures
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[creatures]') AND type IN ('U'))
	DROP TABLE [dbo].[creatures]
GO

CREATE TABLE [dbo].[creatures] (
  [PrefabName] varchar(255) COLLATE Latin1_General_CI_AS  NOT NULL,
  [PosX] float(53)  NOT NULL,
  [PosY] float(53)  NOT NULL,
  [PosZ] float(53)  NOT NULL,
  [RotX] float(53)  NOT NULL,
  [RotY] float(53)  NOT NULL,
  [RotZ] float(53)  NOT NULL,
  [Guid] int  NOT NULL
)
GO

ALTER TABLE [dbo].[creatures] SET (LOCK_ESCALATION = TABLE)
GO

