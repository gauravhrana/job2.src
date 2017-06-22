IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AnalystDelete') 
	BEGIN
	DROP Procedure AnalystDelete
END
GO

CREATE Procedure dbo.AnalystDelete
(
		@AnalystId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Analyst'
)
AS
BEGIN
		DELETE dbo.Analyst
		WHERE	AnalystId = @AnalystId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AnalystId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
