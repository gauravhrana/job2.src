IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PersonSuffixDelete') 
	BEGIN
	DROP Procedure PersonSuffixDelete
END
GO

CREATE Procedure dbo.PersonSuffixDelete
(
		@PersonSuffixId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PersonSuffix'
)
AS
BEGIN
		DELETE dbo.PersonSuffix
		WHERE	PersonSuffixId = @PersonSuffixId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PersonSuffixId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
