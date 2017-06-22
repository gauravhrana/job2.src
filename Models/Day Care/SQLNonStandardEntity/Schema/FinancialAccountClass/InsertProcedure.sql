IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FinancialAccountClassInsert') 
BEGIN
	DROP Procedure FinancialAccountClassInsert
END
GO

CREATE Procedure dbo.FinancialAccountClassInsert
(
		@FinancialAccountClassId				INT		= NULL 	OUTPUT 
	,	@Assets				VARCHAR(500)
	,	@Liabilities				VARCHAR(500)
	,	@Equity				VARCHAR(500)
	,	@Income				INT
	,	@Expense				DECIMAL(18, 5)
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FinancialAccountClass'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@FinancialAccountClassId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.FinancialAccountClass
	(
			FinancialAccountClassId
		,	Assets
		,	Liabilities
		,	Equity
		,	Income
		,	Expense
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@FinancialAccountClassId
		,	@Assets
		,	@Liabilities
		,	@Equity
		,	@Income
		,	@Expense
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @FinancialAccountClassId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FinancialAccountClassId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
