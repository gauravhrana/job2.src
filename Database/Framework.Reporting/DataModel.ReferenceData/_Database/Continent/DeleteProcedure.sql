IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ContinentDelete') 
	BEGIN
	DROP Procedure ContinentDelete
END
GO

CREATE Procedure dbo.ContinentDelete
(
		@ContinentId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Continent'
)
AS
BEGIN
		DELETE dbo.Continent
		WHERE	ContinentId = @ContinentId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ContinentId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
