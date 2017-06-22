IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BusinessUnitDelete') 
BEGIN
	DROP Procedure BusinessUnitDelete
END
GO

CREATE Procedure dbo.BusinessUnitDelete
(
		@BusinessUnitId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'BusinessUnit'
)
AS
BEGIN

	DELETE dbo.BusinessUnit
	WHERE	BusinessUnitId = @BusinessUnitId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @BusinessUnitId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
