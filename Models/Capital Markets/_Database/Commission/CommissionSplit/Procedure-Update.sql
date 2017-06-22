IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionSplitUpdate') 
BEGIN
	DROP Procedure CommissionSplitUpdate
END
GO

CREATE Procedure dbo.CommissionSplitUpdate
(
		@CommissionSplitId				INT
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
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CommissionSplit'
)
AS
BEGIN

	UPDATE	dbo.CommissionSplit
	SET
			CommissionSplitCode				=	@CommissionSplitCode
		,	CommissionSplitDescription				=	@CommissionSplitDescription
		,	FullRate				=	@FullRate
		,	NoneCCA				=	@NoneCCA
		,	CCA				=	@CCA
		,	StartDate				=	@StartDate
		,	EndDate				=	@EndDate
		,	LastModifiedBy				=	@LastModifiedBy
		,	LastModifiedOn				=	@LastModifiedOn
		,	CommissionCodeId				=	@CommissionCodeId
	WHERE	CommissionSplitId			=   @CommissionSplitId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CommissionSplitId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
