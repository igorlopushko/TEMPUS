USE [TEMPUS]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_CreateUser]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_CreateUser]
GO

CREATE PROCEDURE [dbo].[sp_Createuser]
	@id uniqueidentifier,
	@firstName nvarchar(50),
	@lastName nvarchar(50)
AS
BEGIN
	INSERT INTO [dbo].[User] ([Id], [FirstName], [LastName])
	VALUES(@id, @firstName, @lastName)
END

GO