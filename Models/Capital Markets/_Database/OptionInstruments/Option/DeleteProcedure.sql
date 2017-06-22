IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='OptionDelete') 
	BEGIN
	DROP Procedure OptionDelete
END
GO

CREATE Procedure dbo.OptionDelete
(
		@OptionId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Option'
)
AS
BEGIN
		DELETE dbo.Option
		WHERE	OptionId = @OptionId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @OptionId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
