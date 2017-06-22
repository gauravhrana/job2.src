IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='STIFDelete') 
	BEGIN
	DROP Procedure STIFDelete
END
GO

CREATE Procedure dbo.STIFDelete
(
		@STIFId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='STIF'
)
AS
BEGIN
		DELETE dbo.STIF
		WHERE	STIFId = @STIFId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @STIFId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
