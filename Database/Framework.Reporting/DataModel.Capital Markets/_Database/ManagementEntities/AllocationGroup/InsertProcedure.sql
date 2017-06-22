IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AllocationGroupInsert') 
	BEGIN
	DROP Procedure AllocationGroupInsert
END
GO

CREATE Procedure dbo.AllocationGroupInsert
(
		@AllocationGroupId			INT				= NULL 	OUTPUT 
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		='AllocationGroup'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@AllocationGroupId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.AllocationGroup
	(
			AllocationGroupId
		,	Name
		,	Description
		,	SortOrder
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@AllocationGroupId
		,	@Name
		,	@Description
		,	@SortOrder
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AllocationGroupId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
