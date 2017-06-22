IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CountryDelete') 
	BEGIN
	DROP Procedure CountryDelete
END
GO

CREATE Procedure dbo.CountryDelete
(
		@CountryId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Country'
)
AS
BEGIN
		DELETE dbo.Country
		WHERE	CountryId = @CountryId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CountryId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
