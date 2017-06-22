IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CustodianAccountDelete') 
BEGIN
	DROP Procedure CustodianAccountDelete
END
GO

CREATE Procedure dbo.CustodianAccountDelete
(
		@CustodianAccountId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CustodianAccount'
)
AS
BEGIN

	DELETE dbo.CustodianAccount
	WHERE	CustodianAccountId = @CustodianAccountId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CustodianAccountId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
