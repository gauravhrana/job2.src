IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ReportingRequirementDelete') 
BEGIN
	DROP Procedure ReportingRequirementDelete
END
GO

CREATE Procedure dbo.ReportingRequirementDelete
(
		@ReportingRequirementId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'ReportingRequirement'
)
AS
BEGIN

	DELETE dbo.ReportingRequirement
	WHERE	ReportingRequirementId = @ReportingRequirementId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @ReportingRequirementId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
