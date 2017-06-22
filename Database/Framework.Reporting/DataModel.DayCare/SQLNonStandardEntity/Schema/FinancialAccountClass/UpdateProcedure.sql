IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FinancialAccountClassUpdate') 
BEGIN
	DROP Procedure FinancialAccountClassUpdate
END
GO

CREATE Procedure dbo.FinancialAccountClassUpdate
(
		@FinancialAccountClassId				INT
	,	@Assets				VARCHAR(500)
	,	@Liabilities				VARCHAR(500)
	,	@Equity				VARCHAR(500)
	,	@Income				INT
	,	@Expense				DECIMAL(18, 5)
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FinancialAccountClass'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.FinancialAccountClass SET
			Assets				=	@Assets
		,	Liabilities				=	@Liabilities
		,	Equity				=	@Equity
		,	Income				=	@Income
		,	Expense				=	@Expense
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	FinancialAccountClassId			=   @FinancialAccountClassId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FinancialAccountClassId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
