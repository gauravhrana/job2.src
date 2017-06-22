IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionTypeInsert') 
BEGIN
	DROP Procedure CommissionTypeInsert
END
GO

CREATE Procedure dbo.CommissionTypeInsert
(
		@CommissionTypeId				INT		= NULL 	OUTPUT 
	,	@CommissionTypeDescription				VARCHAR(500)
	,	@LastModifiedBy				VARCHAR(500)
	,	@LastModifiedOn				DATETIME
	,	@ShowInFilter				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CommissionType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CommissionTypeId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.CommissionType
	(
			CommissionTypeId
		,	CommissionTypeDescription
		,	LastModifiedBy
		,	LastModifiedOn
		,	ShowInFilter
		,	ApplicationId
	)
	VALUES
	(
			@CommissionTypeId
		,	@CommissionTypeDescription
		,	@LastModifiedBy
		,	@LastModifiedOn
		,	@ShowInFilter
		,	@ApplicationId
	)

	SELECT @CommissionTypeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CommissionTypeId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
