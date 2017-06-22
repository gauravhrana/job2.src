IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CustodianDelete') 
	BEGIN
	DROP Procedure CustodianDelete
END
GO

CREATE Procedure dbo.CustodianDelete
(
		@CustodianId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Custodian'
)
AS
BEGIN
		DELETE dbo.Custodian
		WHERE	CustodianId = @CustodianId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CustodianId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
