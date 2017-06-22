IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileUpdate') 
BEGIN
	DROP Procedure MSPAFileUpdate
END
GO

CREATE Procedure dbo.MSPAFileUpdate
(
		@MSPAFileId				INT
	,	@Filename				VARCHAR(500)
	,	@DropDate				DATETIME
	,	@BusinessDate				DATETIME
	,	@MSPAExtractTaskRunId				INT
	,	@MSPAHoldingTaskRunId				INT
	,	@MSPATradeTaskRunId				INT
	,	@MSPASecurityTaskRunId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'MSPAFile'
)
AS
BEGIN

	UPDATE	dbo.MSPAFile
	SET
			Filename				=	@Filename
		,	DropDate				=	@DropDate
		,	BusinessDate				=	@BusinessDate
		,	MSPAExtractTaskRunId				=	@MSPAExtractTaskRunId
		,	MSPAHoldingTaskRunId				=	@MSPAHoldingTaskRunId
		,	MSPATradeTaskRunId				=	@MSPATradeTaskRunId
		,	MSPASecurityTaskRunId				=	@MSPASecurityTaskRunId
	WHERE	MSPAFileId			=   @MSPAFileId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MSPAFileId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
