CREATE DATABASE WorkDB
GO
USE WorkDB
GO
IF OBJECT_ID ('[dbo].[ContractStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ContractStatus];
GO
CREATE TABLE [dbo].[ContractStat]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [StatName] NCHAR(100) NOT NULL
);
INSERT INTO [dbo].[ContractStatus] (StatName) VALUES (N'Ещё не заключен');
INSERT INTO [dbo].[ContractStatus] (StatName) VALUES (N'Заключен');
INSERT INTO [dbo].[ContractStatus] (StatName) VALUES (N'Расторгнут');
GO
IF OBJECT_ID ('[dbo].[Company]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Company];
GO
CREATE TABLE [dbo].[Company]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyName] NCHAR(100) NOT NULL,
	[ContractStatId] int NOT NULL FOREIGN KEY REFERENCES [dbo].[ContractStat](Id)
);
INSERT INTO [dbo].[Company] (CompanyName,ContractStatId) VALUES (N'Меркурий',	1);
INSERT INTO [dbo].[Company] (CompanyName,ContractStatId) VALUES (N'Венера',	2);
INSERT INTO [dbo].[Company] (CompanyName,ContractStatId) VALUES (N'Земля',	3);
INSERT INTO [dbo].[Company] (CompanyName,ContractStatId) VALUES (N'Марс',		1);
INSERT INTO [dbo].[Company] (CompanyName,ContractStatId) VALUES (N'Сатурн',	2);
GO
IF OBJECT_ID ('[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NCHAR(100) NOT NULL,	
	[Login] NCHAR(100) NOT NULL,
	[Password] NCHAR(100) NOT NULL,
	[CompanyId] INT NOT NULL FOREIGN KEY REFERENCES [dbo].[Company](Id)
);
INSERT INTO [dbo].[User] (UserName, CompanyId, Login, Password) VALUES (N'Саша', 1, N'User1',N'Pass1');
INSERT INTO [dbo].[User] (UserName, CompanyId, Login, Password) VALUES (N'Петя', 2, N'User2',N'Pass2');
INSERT INTO [dbo].[User] (UserName, CompanyId, Login, Password) VALUES (N'Вася', 3, N'User3',N'Pass3');
INSERT INTO [dbo].[User] (UserName, CompanyId, Login, Password) VALUES (N'Анна', 4, N'User4',N'Pass4');
INSERT INTO [dbo].[User] (UserName, CompanyId, Login, Password) VALUES (N'Лена', 5, N'User5',N'Pass5');
INSERT INTO [dbo].[User] (UserName, CompanyId, Login, Password) VALUES (N'Маша', 1, N'User6',N'Pass6');
GO