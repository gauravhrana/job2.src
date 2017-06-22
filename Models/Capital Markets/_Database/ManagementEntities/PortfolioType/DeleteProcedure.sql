IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PortfolioTypeDelete') 
BEGIN
	DROP Procedure PortfolioTypeDelete
END
GO

CREATE Procedure dbo.PortfolioTypeDelete
(
		@PortfolioTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'PortfolioType'
)
AS
BEGIN

	DELETE dbo.PortfolioType
	WHERE	PortfolioTypeId = @PortfolioTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @PortfolioTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
