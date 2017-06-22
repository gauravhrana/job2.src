IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='GeographicRegionDelete') 
	BEGIN
	DROP Procedure GeographicRegionDelete
END
GO

CREATE Procedure dbo.GeographicRegionDelete
(
		@GeographicRegionId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='GeographicRegion'
)
AS
BEGIN
		DELETE dbo.GeographicRegion
		WHERE	GeographicRegionId = @GeographicRegionId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @GeographicRegionId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
