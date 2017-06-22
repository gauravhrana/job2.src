IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RegionDelete') 
BEGIN
	DROP Procedure RegionDelete
END
GO

CREATE Procedure dbo.RegionDelete
(
		@RegionId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Region'
)
AS
BEGIN

	DELETE dbo.Region
	WHERE	RegionId = @RegionId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @RegionId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
