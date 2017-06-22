IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TrainStationDelete') 
	BEGIN
	DROP Procedure TrainStationDelete
END
GO

CREATE Procedure dbo.TrainStationDelete
(
		@TrainStationId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='TrainStation'
)
AS
BEGIN
		DELETE dbo.TrainStation
		WHERE	TrainStationId = @TrainStationId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TrainStationId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
