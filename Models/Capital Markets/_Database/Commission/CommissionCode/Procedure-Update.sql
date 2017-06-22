IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionCodeUpdate') 
BEGIN
	DROP Procedure CommissionCodeUpdate
END
GO

CREATE Procedure dbo.CommissionCodeUpdate
(
		@CommissionCodeId				INT
	,	@CommissionCodeCode				VARCHAR(500)
	,	@CommissionCodeDescription				VARCHAR(500)
	,	@BrokerId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CommissionCode'
)
AS
BEGIN

	UPDATE	dbo.CommissionCode
	SET
			CommissionCodeCode				=	@CommissionCodeCode
		,	CommissionCodeDescription				=	@CommissionCodeDescription
		,	BrokerId				=	@BrokerId
	WHERE	CommissionCodeId			=   @CommissionCodeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CommissionCodeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
