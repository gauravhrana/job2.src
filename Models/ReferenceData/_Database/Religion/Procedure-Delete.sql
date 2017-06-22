IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ReligionDelete') 
BEGIN
	DROP Procedure ReligionDelete
END
GO

CREATE Procedure dbo.ReligionDelete
(
		@ReligionId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Religion'
)
AS
BEGIN

	DELETE dbo.Religion
	WHERE	ReligionId = @ReligionId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @ReligionId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
