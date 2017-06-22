IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='EthnicityDelete') 
	BEGIN
	DROP Procedure EthnicityDelete
END
GO

CREATE Procedure dbo.EthnicityDelete
(
		@EthnicityId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Ethnicity'
)
AS
BEGIN
		DELETE dbo.Ethnicity
		WHERE	EthnicityId = @EthnicityId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @EthnicityId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
