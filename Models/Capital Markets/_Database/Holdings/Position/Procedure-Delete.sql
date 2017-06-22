IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PositionDelete') 
BEGIN
	DROP Procedure PositionDelete
END
GO

CREATE Procedure dbo.PositionDelete
(
		@PositionId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Position'
)
AS
BEGIN

	DELETE dbo.Position
	WHERE	PositionId = @PositionId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @PositionId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
