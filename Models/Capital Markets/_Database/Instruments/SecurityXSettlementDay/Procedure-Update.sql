IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXSettlementDayUpdate') 
BEGIN
	DROP Procedure SecurityXSettlementDayUpdate
END
GO

CREATE Procedure dbo.SecurityXSettlementDayUpdate
(
		@SecurityXSettlementDayId				INT
	,	@SettlementDay				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXSettlementDay'
)
AS
BEGIN
			SettlementDay				=	@SettlementDay
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	SecurityXSettlementDayId			=   @SecurityXSettlementDayId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SecurityXSettlementDayId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
