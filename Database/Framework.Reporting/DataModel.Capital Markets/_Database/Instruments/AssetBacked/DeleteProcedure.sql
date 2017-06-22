IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AssetBackedDelete') 
	BEGIN
	DROP Procedure AssetBackedDelete
END
GO

CREATE Procedure dbo.AssetBackedDelete
(
		@AssetBackedId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AssetBacked'
)
AS
BEGIN
		DELETE dbo.AssetBacked
		WHERE	AssetBackedId = @AssetBackedId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AssetBackedId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
