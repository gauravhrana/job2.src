IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ReligionInsert') 
BEGIN
	DROP Procedure ReligionInsert
END
GO

CREATE Procedure dbo.ReligionInsert
(
		@ReligionId				INT		= NULL 	OUTPUT 
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Religion'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReligionId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.Religion
	(
			ReligionId
		,	Name
		,	Description
		,	SortOrder
		,	ApplicationId
	)
	VALUES
	(
			@ReligionId
		,	@Name
		,	@Description
		,	@SortOrder
		,	@ApplicationId
	)

	SELECT @ReligionId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ReligionId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
