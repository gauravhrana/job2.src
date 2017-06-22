IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TimeZoneInsert') 
BEGIN
	DROP Procedure TimeZoneInsert
END
GO

CREATE Procedure dbo.TimeZoneInsert
(
		@TimeZoneId				INT		= NULL 	OUTPUT 
	,	@TimeDifference				DECIMAL(18, 5)
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TimeZone'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TimeZoneId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.TimeZone
	(
			TimeZoneId
		,	TimeDifference
		,	Name
		,	Description
		,	SortOrder
		,	ApplicationId
	)
	VALUES
	(
			@TimeZoneId
		,	@TimeDifference
		,	@Name
		,	@Description
		,	@SortOrder
		,	@ApplicationId
	)

	SELECT @TimeZoneId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TimeZoneId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
