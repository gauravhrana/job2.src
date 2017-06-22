IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityClassDelete') 
	BEGIN
	DROP Procedure SecurityClassDelete
END
GO

CREATE Procedure dbo.SecurityClassDelete
(
		@SecurityClassId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='SecurityClass'
)
AS
BEGIN
		DELETE dbo.SecurityClass
		WHERE	SecurityClassId = @SecurityClassId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SecurityClassId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
