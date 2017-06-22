IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ProvinceTypeDelete') 
	BEGIN
	DROP Procedure ProvinceTypeDelete
END
GO

CREATE Procedure dbo.ProvinceTypeDelete
(
		@ProvinceTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='ProvinceType'
)
AS
BEGIN
		DELETE dbo.ProvinceType
		WHERE	ProvinceTypeId = @ProvinceTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ProvinceTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
