IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StateInsert') 
BEGIN
	DROP Procedure StateInsert
END
GO

CREATE Procedure dbo.StateInsert
(
		@StateId				INT		= NULL 	OUTPUT 
	,	@CountryId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'State'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @StateId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.State
	(
			StateId
		,	CountryId
		,	Name
		,	Description
		,	SortOrder
		,	ApplicationId
	)
	VALUES
	(
			@StateId
		,	@CountryId
		,	@Name
		,	@Description
		,	@SortOrder
		,	@ApplicationId
	)

	SELECT @StateId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @StateId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
