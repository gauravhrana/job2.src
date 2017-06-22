IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionRateDelete') 
BEGIN
	DROP Procedure CommissionRateDelete
END
GO

CREATE Procedure dbo.CommissionRateDelete
(
		@CommissionRateId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CommissionRate'
)
AS
BEGIN

	DELETE dbo.CommissionRate
	WHERE	CommissionRateId = @CommissionRateId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CommissionRateId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
