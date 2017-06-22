IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='EquityDelete') 
	BEGIN
	DROP Procedure EquityDelete
END
GO

CREATE Procedure dbo.EquityDelete
(
		@EquityId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Equity'
)
AS
BEGIN
		DELETE dbo.Equity
		WHERE	EquityId = @EquityId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @EquityId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
