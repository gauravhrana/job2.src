IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccountExecutiveUpdate') 
	BEGIN
	DROP Procedure AccountExecutiveUpdate
END
GO

CREATE Procedure dbo.AccountExecutiveUpdate
(
		@AccountExcecutiveId					INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'AccountExecutive'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.AccountExecutive SET
		AccountExcecutiveId				=	@AccountExcecutiveId
	,	Name				=	@Name
	,	Description				=	@Description
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		AccountExecutiveId			=   @AccountExecutiveId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @AccountExecutiveId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
