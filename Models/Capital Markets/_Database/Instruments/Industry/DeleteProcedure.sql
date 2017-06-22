IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='IndustryDelete') 
BEGIN
	DROP Procedure IndustryDelete
END
GO

CREATE Procedure dbo.IndustryDelete
(
		@IndustryId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Industry'
)
AS
BEGIN

	DELETE dbo.Industry

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @IndustryId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
