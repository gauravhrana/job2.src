IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='InventoryStateDelete') 
	BEGIN
	DROP Procedure InventoryStateDelete
END
GO

CREATE Procedure dbo.InventoryStateDelete
(
		@InventoryStateId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='InventoryState'
)
AS
BEGIN
		DELETE dbo.InventoryState
		WHERE	InventoryStateId = @InventoryStateId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @InventoryStateId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
