USE [TEMPUS]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_UpdateUser]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[sp_UpdateUser]
GO

CREATE PROCEDURE [dbo].[sp_UpdateUser]
	@id uniqueidentifier,
	@firstName nvarchar(50),
	@lastName nvarchar(50)
AS
BEGIN
	UPDATE [dbo].[User] 
	SET [FirstName] = @firstName, 
		[LastName] = @lastName
	WHERE [Id] = @id 
END

GO