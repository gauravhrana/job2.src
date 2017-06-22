IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SubBusinessUnitDelete') 
BEGIN
	DROP Procedure SubBusinessUnitDelete
END
GO

CREATE Procedure dbo.SubBusinessUnitDelete
(
		@SubBusinessUnitId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SubBusinessUnit'
)
AS
BEGIN

	DELETE dbo.SubBusinessUnit
	WHERE	SubBusinessUnitId = @SubBusinessUnitId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SubBusinessUnitId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
