IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FieldConfigurationBaseUpdate') 
BEGIN
	DROP Procedure FieldConfigurationBaseUpdate
END
GO

CREATE Procedure dbo.FieldConfigurationBaseUpdate
(
		@FieldConfigurationBaseId				INT
	,	@Name				VARCHAR(500)
	,	@Value				VARCHAR(500)
	,	@ControlType				VARCHAR(500)
	,	@Formatting				VARCHAR(500)
	,	@Version				VARCHAR(500)
	,	@Width				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'FieldConfigurationBase'
)
AS
BEGIN

	UPDATE	dbo.FieldConfigurationBase
	SET
			Name				=	@Name
		,	Value				=	@Value
		,	ControlType				=	@ControlType
		,	Formatting				=	@Formatting
		,	Version				=	@Version
		,	Width				=	@Width
	WHERE	FieldConfigurationBaseId			=   @FieldConfigurationBaseId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FieldConfigurationBaseId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
