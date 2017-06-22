IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FieldConfigurationBaseInsert') 
BEGIN
	DROP Procedure FieldConfigurationBaseInsert
END
GO

CREATE Procedure dbo.FieldConfigurationBaseInsert
(
		@FieldConfigurationBaseId				INT		= NULL 	OUTPUT 
	,	@Name				VARCHAR(500)
	,	@Value				VARCHAR(500)
	,	@ControlType				VARCHAR(500)
	,	@Formatting				VARCHAR(500)
	,	@Version				VARCHAR(500)
	,	@Width				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FieldConfigurationBase'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @FieldConfigurationBaseId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.FieldConfigurationBase
	(
			FieldConfigurationBaseId
		,	Name
		,	Value
		,	ControlType
		,	Formatting
		,	Version
		,	Width
		,	ApplicationId
	)
	VALUES
	(
			@FieldConfigurationBaseId
		,	@Name
		,	@Value
		,	@ControlType
		,	@Formatting
		,	@Version
		,	@Width
		,	@ApplicationId
	)

	SELECT @FieldConfigurationBaseId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FieldConfigurationBaseId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
