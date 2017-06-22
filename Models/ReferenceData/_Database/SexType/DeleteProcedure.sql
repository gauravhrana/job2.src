IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SexTypeDelete') 
	BEGIN
	DROP Procedure SexTypeDelete
END
GO

CREATE Procedure dbo.SexTypeDelete
(
		@SexTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='SexType'
)
AS
BEGIN
		DELETE dbo.SexType
		WHERE	SexTypeId = @SexTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SexTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
