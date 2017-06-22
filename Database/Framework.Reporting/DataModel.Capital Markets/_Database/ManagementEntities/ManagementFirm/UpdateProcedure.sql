IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ManagementFirmUpdate') 
	BEGIN
	DROP Procedure ManagementFirmUpdate
END
GO

CREATE Procedure dbo.ManagementFirmUpdate
(
		@ManagementFirmId			INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'ManagementFirm'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.ManagementFirm SET
		Name				=	@Name
	,	Description				=	@Description
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		ManagementFirmId			=   @ManagementFirmId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @ManagementFirmId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
