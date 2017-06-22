IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionSplitDelete') 
BEGIN
	DROP Procedure CommissionSplitDelete
END
GO

CREATE Procedure dbo.CommissionSplitDelete
(
		@CommissionSplitId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CommissionSplit'
)
AS
BEGIN

	DELETE dbo.CommissionSplit
	WHERE	CommissionSplitId = @CommissionSplitId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CommissionSplitId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
