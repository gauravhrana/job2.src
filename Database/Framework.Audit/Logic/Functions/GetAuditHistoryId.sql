--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'GetAuditHistoryId')
--BEGIN
--	PRINT 'Dropping Function GetAuditHistoryId'
--	DROP FUNCTION  dbo.GetAuditHistoryId
--END
--GO

--PRINT 'Creating Function GetAuditHistoryId'
--GO

CREATE FUNCTION dbo.GetAuditHistoryId
(
	@EntityKey				INT		= NULL	, 
	@SystemEntityTypeId		INT		= NULL
)
RETURNS INT
AS
BEGIN

	DECLARE @AuditHistoryId AS INT

	SELECT	
	TOP 1		@AuditHistoryId		= c.AuditHistoryId	
	FROM 	  	CommonServices.dbo.AuditHistory c		
	WHERE		c.EntityKey			= @EntityKey
	AND			c.SystemEntityId	= @SystemEntityTypeId
	AND			c.AuditActionId		IN (1,2)
	ORDER BY	c.CreatedDate DESC

	RETURN @AuditHistoryId

END