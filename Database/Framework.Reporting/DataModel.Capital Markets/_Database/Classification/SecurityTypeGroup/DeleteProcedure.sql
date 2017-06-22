IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityTypeGroupDelete') 
BEGIN
	DROP Procedure SecurityTypeGroupDelete
END
GO

CREATE Procedure dbo.SecurityTypeGroupDelete
(
		@SecurityTypeGroupId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SecurityTypeGroup'
)
AS
BEGIN

	DELETE dbo.SecurityTypeGroup

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SecurityTypeGroupId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
