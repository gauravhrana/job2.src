IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AirportDelete') 
BEGIN
	DROP Procedure AirportDelete
END
GO

CREATE Procedure dbo.AirportDelete
(
		@AirportId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Airport'
)
AS
BEGIN

	DELETE dbo.Airport
	WHERE	AirportId = @AirportId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @AirportId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
