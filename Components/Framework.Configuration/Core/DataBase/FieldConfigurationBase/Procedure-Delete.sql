IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FieldConfigurationBaseDelete') 
BEGIN
	DROP Procedure FieldConfigurationBaseDelete
END
GO

CREATE Procedure dbo.FieldConfigurationBaseDelete
(
		@FieldConfigurationBaseId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'FieldConfigurationBase'
)
AS
BEGIN

	DELETE dbo.FieldConfigurationBase
	WHERE	FieldConfigurationBaseId = @FieldConfigurationBaseId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @FieldConfigurationBaseId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
