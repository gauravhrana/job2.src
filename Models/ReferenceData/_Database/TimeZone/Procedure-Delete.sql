IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TimeZoneDelete') 
BEGIN
	DROP Procedure TimeZoneDelete
END
GO

CREATE Procedure dbo.TimeZoneDelete
(
		@TimeZoneId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TimeZone'
)
AS
BEGIN

	DELETE dbo.TimeZone
	WHERE	TimeZoneId = @TimeZoneId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TimeZoneId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
