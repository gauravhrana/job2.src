IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AllocationGroupDelete') 
BEGIN
	DROP Procedure AllocationGroupDelete
END
GO

CREATE Procedure dbo.AllocationGroupDelete
(
		@AllocationGroupId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'AllocationGroup'
)
AS
BEGIN

	DELETE dbo.AllocationGroup
	WHERE	AllocationGroupId = @AllocationGroupId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @AllocationGroupId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
