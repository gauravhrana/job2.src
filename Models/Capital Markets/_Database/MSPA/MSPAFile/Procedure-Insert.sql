IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileInsert') 
BEGIN
	DROP Procedure MSPAFileInsert
END
GO

CREATE Procedure dbo.MSPAFileInsert
(
		@MSPAFileId				INT		= NULL 	OUTPUT 
	,	@Filename				VARCHAR(500)
	,	@DropDate				DATETIME
	,	@BusinessDate				DATETIME
	,	@MSPAExtractTaskRunId				INT
	,	@MSPAHoldingTaskRunId				INT
	,	@MSPATradeTaskRunId				INT
	,	@MSPASecurityTaskRunId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'MSPAFile'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MSPAFileId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.MSPAFile
	(
			MSPAFileId
		,	Filename
		,	DropDate
		,	BusinessDate
		,	MSPAExtractTaskRunId
		,	MSPAHoldingTaskRunId
		,	MSPATradeTaskRunId
		,	MSPASecurityTaskRunId
		,	ApplicationId
	)
	VALUES
	(
			@MSPAFileId
		,	@Filename
		,	@DropDate
		,	@BusinessDate
		,	@MSPAExtractTaskRunId
		,	@MSPAHoldingTaskRunId
		,	@MSPATradeTaskRunId
		,	@MSPASecurityTaskRunId
		,	@ApplicationId
	)

	SELECT @MSPAFileId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MSPAFileId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
