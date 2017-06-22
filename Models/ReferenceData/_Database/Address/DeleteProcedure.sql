IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AddressDelete') 
BEGIN
	DROP Procedure AddressDelete
END
GO

CREATE Procedure dbo.AddressDelete
(
		@AddressId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Address'
)
AS
BEGIN

	DELETE dbo.Address
	WHERE	AddressId = @AddressId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @AddressId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
