IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='GenderDelete') 
	BEGIN
	DROP Procedure GenderDelete
END
GO

CREATE Procedure dbo.GenderDelete
(
		@GenderId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Gender'
)
AS
BEGIN
		DELETE dbo.Gender
		WHERE	GenderId = @GenderId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @GenderId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
