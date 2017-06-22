IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AnnotationsDelete') 
	BEGIN
	DROP Procedure AnnotationsDelete
END
GO

CREATE Procedure dbo.AnnotationsDelete
(
		@AnnotationsId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Annotations'
)
AS
BEGIN
		DELETE dbo.Annotations
		WHERE	AnnotationsId = @AnnotationsId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AnnotationsId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
