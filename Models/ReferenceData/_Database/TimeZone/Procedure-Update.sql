IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TimeZoneUpdate') 
BEGIN
	DROP Procedure TimeZoneUpdate
END
GO

CREATE Procedure dbo.TimeZoneUpdate
(
		@TimeZoneId				INT
	,	@TimeDifference				DECIMAL(18, 5)
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TimeZone'
)
AS
BEGIN

	UPDATE	dbo.TimeZone
	SET
			TimeDifference				=	@TimeDifference
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	TimeZoneId			=   @TimeZoneId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TimeZoneId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
