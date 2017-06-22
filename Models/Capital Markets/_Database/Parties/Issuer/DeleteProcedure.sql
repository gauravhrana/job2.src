IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='IssuerDelete') 
	BEGIN
	DROP Procedure IssuerDelete
END
GO

CREATE Procedure dbo.IssuerDelete
(
		@IssuerId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Issuer'
)
AS
BEGIN
		DELETE dbo.Issuer
		WHERE	IssuerId = @IssuerId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @IssuerId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
