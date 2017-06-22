IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StudentUpdate') 
	BEGIN
	DROP Procedure StudentUpdate
END
GO

CREATE Procedure dbo.StudentUpdate
(
		@StudentId			INT
	,	@Description				VARCHAR(500)	= NULL
	,	@Name						VARCHAR(50)		= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Student'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.Student SET
		Description				=	@Description
	,	Name				=	@Name
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		StudentId			=   @StudentId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @StudentId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
