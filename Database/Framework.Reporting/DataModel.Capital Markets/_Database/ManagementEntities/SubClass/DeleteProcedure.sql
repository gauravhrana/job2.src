IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SubClassDelete') 
	BEGIN
	DROP Procedure SubClassDelete
END
GO

CREATE Procedure dbo.SubClassDelete
(
		@SubClassId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='SubClass'
)
AS
BEGIN
		DELETE dbo.SubClass
		WHERE	SubClassId = @SubClassId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SubClassId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
