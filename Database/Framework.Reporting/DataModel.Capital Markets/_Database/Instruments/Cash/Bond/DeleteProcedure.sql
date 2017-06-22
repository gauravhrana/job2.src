IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BondDelete') 
	BEGIN
	DROP Procedure BondDelete
END
GO

CREATE Procedure dbo.BondDelete
(
		@BondId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Bond'
)
AS
BEGIN
		DELETE dbo.Bond
		WHERE	BondId = @BondId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @BondId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
