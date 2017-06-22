IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommissionCodeInsert') 
BEGIN
	DROP Procedure CommissionCodeInsert
END
GO

CREATE Procedure dbo.CommissionCodeInsert
(
		@CommissionCodeId				INT		= NULL 	OUTPUT 
	,	@CommissionCodeCode				VARCHAR(500)
	,	@CommissionCodeDescription				VARCHAR(500)
	,	@BrokerId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CommissionCode'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CommissionCodeId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.CommissionCode
	(
			CommissionCodeId
		,	CommissionCodeCode
		,	CommissionCodeDescription
		,	BrokerId
		,	ApplicationId
	)
	VALUES
	(
			@CommissionCodeId
		,	@CommissionCodeCode
		,	@CommissionCodeDescription
		,	@BrokerId
		,	@ApplicationId
	)

	SELECT @CommissionCodeId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CommissionCodeId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
