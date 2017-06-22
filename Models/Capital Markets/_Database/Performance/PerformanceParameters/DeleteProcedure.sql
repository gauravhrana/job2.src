IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PerformanceParametersDelete') 
	BEGIN
	DROP Procedure PerformanceParametersDelete
END
GO

CREATE Procedure dbo.PerformanceParametersDelete
(
		@PerformanceParametersId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PerformanceParameters'
)
AS
BEGIN
		DELETE dbo.PerformanceParameters
		WHERE	PerformanceParametersId = @PerformanceParametersId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PerformanceParametersId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
