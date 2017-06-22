IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='HelpLineDelete') 
	BEGIN
	DROP Procedure HelpLineDelete
END
GO

CREATE Procedure dbo.HelpLineDelete
(
		@HelpLineId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='HelpLine'
)
AS
BEGIN
		DELETE dbo.HelpLine
		WHERE	HelpLineId = @HelpLineId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @HelpLineId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
