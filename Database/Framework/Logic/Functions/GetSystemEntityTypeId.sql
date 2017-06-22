IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'GetSystemEntityTypeId')
BEGIN
	PRINT 'Dropping Function GetSystemEntityTypeId'
	DROP FUNCTION  dbo.GetSystemEntityTypeId
END
GO

PRINT 'Creating Function GetSystemEntityTypeId'
GO

CREATE FUNCTION dbo.GetSystemEntityTypeId
(
	@SystemEntityType	VARCHAR(50)		
)	
RETURNS INT
AS
BEGIN

	DECLARE @SystemEntityTypeId AS INT
	
	SELECT	@SystemEntityTypeId	= a.SystemEntityTypeId
	FROM	dbo.SystemEntityType a
	WHERE	a.EntityName		= @SystemEntityType 
	
	RETURN @SystemEntityTypeId

END