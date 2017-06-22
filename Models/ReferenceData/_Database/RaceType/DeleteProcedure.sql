IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RaceTypeDelete') 
	BEGIN
	DROP Procedure RaceTypeDelete
END
GO

CREATE Procedure dbo.RaceTypeDelete
(
		@RaceTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='RaceType'
)
AS
BEGIN
		DELETE dbo.RaceType
		WHERE	RaceTypeId = @RaceTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @RaceTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
