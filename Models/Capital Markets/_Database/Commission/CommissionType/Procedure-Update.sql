IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionTypeUpdate') 
BEGIN
	DROP Procedure CommissionTypeUpdate
END
GO

CREATE Procedure dbo.CommissionTypeUpdate
(
		@CommissionTypeId				INT
	,	@CommissionTypeDescription				VARCHAR(500)
	,	@LastModifiedBy				VARCHAR(500)
	,	@LastModifiedOn				DATETIME
	,	@ShowInFilter				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CommissionType'
)
AS
BEGIN

	UPDATE	dbo.CommissionType
	SET
			CommissionTypeDescription				=	@CommissionTypeDescription
		,	LastModifiedBy				=	@LastModifiedBy
		,	LastModifiedOn				=	@LastModifiedOn
		,	ShowInFilter				=	@ShowInFilter
	WHERE	CommissionTypeId			=   @CommissionTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CommissionTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
