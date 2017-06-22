IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccountExecutiveDelete') 
	BEGIN
	DROP Procedure AccountExecutiveDelete
END
GO

CREATE Procedure dbo.AccountExecutiveDelete
(
		@AccountExecutiveId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccountExecutive'
)
AS
BEGIN
		DELETE dbo.AccountExecutive
		WHERE	AccountExecutiveId = @AccountExecutiveId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccountExecutiveId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
