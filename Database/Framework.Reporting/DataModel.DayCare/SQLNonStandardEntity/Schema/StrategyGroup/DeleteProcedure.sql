IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StrategyGroupDelete') 
BEGIN
	DROP Procedure StrategyGroupDelete
END
GO

CREATE Procedure dbo.StrategyGroupDelete
(
		@StrategyGroupId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'StrategyGroup'
)
AS
BEGIN

	DELETE dbo.StrategyGroup
	WHERE	StrategyGroupId = @StrategyGroupId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @StrategyGroupId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
