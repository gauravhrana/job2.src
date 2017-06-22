IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PerformanceKeyDelete') 
	BEGIN
	DROP Procedure PerformanceKeyDelete
END
GO

CREATE Procedure dbo.PerformanceKeyDelete
(
		@PerformanceKeyId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PerformanceKey'
)
AS
BEGIN
		DELETE dbo.PerformanceKey
		WHERE	PerformanceKeyId = @PerformanceKeyId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PerformanceKeyId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
