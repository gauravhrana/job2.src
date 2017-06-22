IF EXISTS (SELECT * FROM sysobjects WHERE type = 'FN' AND name = 'GetMAXAuditHistoryId')
BEGIN
	PRINT 'Dropping Function GetMAXAuditHistoryId'
	DROP FUNCTION  dbo.GetMAXAuditHistoryId
END
GO

PRINT 'Creating Function GetMAXAuditHistoryId'
GO

CREATE FUNCTION dbo.GetMAXAuditHistoryId
(
	@EntityKey				INT		= NULL	, 
	@SystemEntityTypeId		INT		= NULL
)
RETURNS INT
AS
BEGIN

	DECLARE @AuditHistoryId AS INT

	SELECT	
	@AuditHistoryId = MAX(c.AuditHistoryId)
	FROM 		dbo.AuditHistory c		
	WHERE		c.EntityKey			= @EntityKey
	AND			c.SystemEntityId	= @SystemEntityTypeId
	AND			c.AuditActionId		IN (1,2)
	

	RETURN @AuditHistoryId

END
