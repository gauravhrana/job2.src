IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionTypeDelete') 
BEGIN
	DROP Procedure CommissionTypeDelete
END
GO

CREATE Procedure dbo.CommissionTypeDelete
(
		@CommissionTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CommissionType'
)
AS
BEGIN

	DELETE dbo.CommissionType
	WHERE	CommissionTypeId = @CommissionTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CommissionTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
