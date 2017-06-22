IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='JurisdictionsDelete') 
BEGIN
	DROP Procedure JurisdictionsDelete
END
GO

CREATE Procedure dbo.JurisdictionsDelete
(
		@JurisdictionsId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Jurisdictions'
)
AS
BEGIN

	DELETE dbo.Jurisdictions
	WHERE	JurisdictionsId = @JurisdictionsId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @JurisdictionsId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
