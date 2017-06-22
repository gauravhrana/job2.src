IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SettlementStatusDelete') 
BEGIN
	DROP Procedure SettlementStatusDelete
END
GO

CREATE Procedure dbo.SettlementStatusDelete
(
		@SettlementStatusId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SettlementStatus'
)
AS
BEGIN

	DELETE dbo.SettlementStatus
	WHERE	SettlementStatusId = @SettlementStatusId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SettlementStatusId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
