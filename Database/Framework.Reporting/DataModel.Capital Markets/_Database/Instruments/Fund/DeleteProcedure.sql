IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundDelete') 
	BEGIN
	DROP Procedure FundDelete
END
GO

CREATE Procedure dbo.FundDelete
(
		@FundId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Fund'
)
AS
BEGIN
		DELETE dbo.Fund
		WHERE	FundId = @FundId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FundId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
