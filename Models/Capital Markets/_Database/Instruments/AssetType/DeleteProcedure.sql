IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AssetTypeDelete') 
	BEGIN
	DROP Procedure AssetTypeDelete
END
GO

CREATE Procedure dbo.AssetTypeDelete
(
		@AssetTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AssetType'
)
AS
BEGIN
		DELETE dbo.AssetType
		WHERE	AssetTypeId = @AssetTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AssetTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
