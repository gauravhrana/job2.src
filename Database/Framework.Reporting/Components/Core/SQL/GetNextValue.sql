	
DECLARE @DBName			AS	VARCHAR(50)
DECLARE @NextValue		AS	INT	
DECLARE @sql			AS  NVARCHAR(512)	
	
SELECT	@NextValue				= a.NextValue
FROM	Configuration.dbo.SystemEntityType a
WHERE	a.EntityName		= '@EntityName@'

SELECT @NextValue




