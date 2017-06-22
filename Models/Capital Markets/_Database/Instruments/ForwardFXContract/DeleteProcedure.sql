IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ForwardFXContractDelete') 
	BEGIN
	DROP Procedure ForwardFXContractDelete
END
GO

CREATE Procedure dbo.ForwardFXContractDelete
(
		@ForwardFXContractId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='ForwardFXContract'
)
AS
BEGIN
		DELETE dbo.ForwardFXContract
		WHERE	ForwardFXContractId = @ForwardFXContractId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ForwardFXContractId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
