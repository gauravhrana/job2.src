IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BathRoomDelete') 
	BEGIN
	DROP Procedure BathRoomDelete
END
GO

CREATE Procedure dbo.BathRoomDelete
(
		@BathRoomId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='BathRoom'
)
AS
BEGIN
		DELETE dbo.BathRoom
		WHERE	BathRoomId = @BathRoomId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @BathRoomId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
