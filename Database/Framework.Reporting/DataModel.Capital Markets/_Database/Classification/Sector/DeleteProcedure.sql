IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SectorDelete') 
BEGIN
	DROP Procedure SectorDelete
END
GO

CREATE Procedure dbo.SectorDelete
(
		@SectorId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Sector'
)
AS
BEGIN

	DELETE dbo.Sector

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @SectorId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
