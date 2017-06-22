IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXSettlementDayDelete') 
BEGIN
	DROP Procedure SecurityXSettlementDayDelete
END
GO

CREATE Procedure dbo.SecurityXSettlementDayDelete
(
		@SecurityXSettlementDayId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SecurityXSettlementDay'
)
AS
BEGIN

	DELETE dbo.SecurityXSettlementDay
	WHERE	SecurityXSettlementDayId = @SecurityXSettlementDayId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SecurityXSettlementDayId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
