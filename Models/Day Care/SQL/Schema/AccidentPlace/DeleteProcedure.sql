IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccidentPlaceDelete') 
	BEGIN
	DROP Procedure AccidentPlaceDelete
END
GO

CREATE Procedure dbo.AccidentPlaceDelete
(
		@AccidentPlaceId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccidentPlace'
)
AS
BEGIN
		DELETE dbo.AccidentPlace
		WHERE	AccidentPlaceId = @AccidentPlaceId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccidentPlaceId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
