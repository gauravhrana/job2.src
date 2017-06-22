IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionSplitInsert') 
BEGIN
	DROP Procedure CommissionSplitInsert
END
GO

CREATE Procedure dbo.CommissionSplitInsert
(
		@CommissionSplitId				INT		= NULL 	OUTPUT 
	,	@CommissionSplitCode				VARCHAR(500)
	,	@CommissionSplitDescription				VARCHAR(500)
	,	@FullRate				DECIMAL(18, 5)
	,	@NoneCCA				DECIMAL(18, 5)
	,	@CCA				DECIMAL(18, 5)
	,	@StartDate				DATETIME
	,	@EndDate				DATETIME
	,	@LastModifiedBy				VARCHAR(500)
	,	@LastModifiedOn				DATETIME
	,	@CommissionCodeId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CommissionSplit'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CommissionSplitId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.CommissionSplit
	(
			CommissionSplitId
		,	CommissionSplitCode
		,	CommissionSplitDescription
		,	FullRate
		,	NoneCCA
		,	CCA
		,	StartDate
		,	EndDate
		,	LastModifiedBy
		,	LastModifiedOn
		,	CommissionCodeId
		,	ApplicationId
	)
	VALUES
	(
			@CommissionSplitId
		,	@CommissionSplitCode
		,	@CommissionSplitDescription
		,	@FullRate
		,	@NoneCCA
		,	@CCA
		,	@StartDate
		,	@EndDate
		,	@LastModifiedBy
		,	@LastModifiedOn
		,	@CommissionCodeId
		,	@ApplicationId
	)

	SELECT @CommissionSplitId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CommissionSplitId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
