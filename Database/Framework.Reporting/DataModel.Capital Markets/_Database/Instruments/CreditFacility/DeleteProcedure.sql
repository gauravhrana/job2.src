IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CreditFacilityDelete') 
	BEGIN
	DROP Procedure CreditFacilityDelete
END
GO

CREATE Procedure dbo.CreditFacilityDelete
(
		@CreditFacilityId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='CreditFacility'
)
AS
BEGIN
		DELETE dbo.CreditFacility
		WHERE	CreditFacilityId = @CreditFacilityId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CreditFacilityId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
