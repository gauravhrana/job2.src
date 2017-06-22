IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionCodeDelete') 
BEGIN
	DROP Procedure CommissionCodeDelete
END
GO

CREATE Procedure dbo.CommissionCodeDelete
(
		@CommissionCodeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CommissionCode'
)
AS
BEGIN

	DELETE dbo.CommissionCode
	WHERE	CommissionCodeId = @CommissionCodeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CommissionCodeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
