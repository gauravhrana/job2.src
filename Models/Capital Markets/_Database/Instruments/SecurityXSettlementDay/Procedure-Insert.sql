IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXSettlementDayInsert') 
BEGIN
	DROP Procedure SecurityXSettlementDayInsert
END
GO

CREATE Procedure dbo.SecurityXSettlementDayInsert
(
		@SecurityXSettlementDayId				INT		= NULL 	OUTPUT 
	,	@SettlementDay				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXSettlementDay'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SecurityXSettlementDayId Output, @AuditId


	INSERT INTO dbo.SecurityXSettlementDay
	(
			SecurityXSettlementDayId
		,	SettlementDay
		,	Name
		,	Description
		,	SortOrder
		,	ApplicationId
	)
	VALUES
	(
			@SecurityXSettlementDayId
		,	@SettlementDay
		,	@Name
		,	@Description
		,	@SortOrder
		,	@ApplicationId
	)

	SELECT @SecurityXSettlementDayId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SecurityXSettlementDayId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
