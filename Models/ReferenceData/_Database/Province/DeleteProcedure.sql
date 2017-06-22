IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ProvinceDelete') 
BEGIN
	DROP Procedure ProvinceDelete
END
GO

CREATE Procedure dbo.ProvinceDelete
(
		@ProvinceId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Province'
)
AS
BEGIN

	DELETE dbo.Province
	WHERE	ProvinceId = @ProvinceId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @ProvinceId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
