IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SubIndustryDelete') 
BEGIN
	DROP Procedure SubIndustryDelete
END
GO

CREATE Procedure dbo.SubIndustryDelete
(
		@SubIndustryId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'SubIndustry'
)
AS
BEGIN

	DELETE dbo.SubIndustry

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SubIndustryId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
