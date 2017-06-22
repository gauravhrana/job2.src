IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CityDelete') 
BEGIN
	DROP Procedure CityDelete
END
GO

CREATE Procedure dbo.CityDelete
(
		@CityId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'City'
)
AS
BEGIN

	DELETE dbo.City
	WHERE	CityId = @CityId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CityId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
