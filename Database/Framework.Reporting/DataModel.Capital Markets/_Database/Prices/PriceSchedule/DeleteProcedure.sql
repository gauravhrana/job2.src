IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceScheduleDelete') 
	BEGIN
	DROP Procedure PriceScheduleDelete
END
GO

CREATE Procedure dbo.PriceScheduleDelete
(
		@PriceScheduleId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PriceSchedule'
)
AS
BEGIN
		DELETE dbo.PriceSchedule
		WHERE	PriceScheduleId = @PriceScheduleId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PriceScheduleId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
