IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FinancialAccountTypeDelete') 
BEGIN
	DROP Procedure FinancialAccountTypeDelete
END
GO

CREATE Procedure dbo.FinancialAccountTypeDelete
(
		@FinancialAccountTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'FinancialAccountType'
)
AS
BEGIN

	DELETE dbo.FinancialAccountType
	WHERE	FinancialAccountTypeId = @FinancialAccountTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @FinancialAccountTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
