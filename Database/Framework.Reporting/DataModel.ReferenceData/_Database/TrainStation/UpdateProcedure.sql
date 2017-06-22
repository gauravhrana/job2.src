IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TrainStationUpdate') 
	BEGIN
	DROP Procedure TrainStationUpdate
END
GO

CREATE Procedure dbo.TrainStationUpdate
(
		@TrainStationId			INT
	,	@Name						VARCHAR(50)		= NULL
	,	@Description				VARCHAR(500)	= NULL
	,	@SortOrder					INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TrainStation'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT
	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE dbo.TrainStation SET
		Name				=	@Name
	,	Description				=	@Description
	,	SortOrder				=	@SortOrder
	,	UpdatedDate				=	@UpdatedDate
	,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE
		TrainStationId			=   @TrainStationId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType		= @SystemEntityType
	,	@EntityKey				= @TrainStationId
	,	@AuditAction			= 'Update'
	,	@CreatedDate			= @AuditDate
	,	@CreatedByPersonId		= @AuditId

END
GO
