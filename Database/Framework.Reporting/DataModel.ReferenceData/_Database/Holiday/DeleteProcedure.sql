IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='HolidayDelete') 
	BEGIN
	DROP Procedure HolidayDelete
END
GO

CREATE Procedure dbo.HolidayDelete
(
		@HolidayId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Holiday'
)
AS
BEGIN
		DELETE dbo.Holiday
		WHERE	HolidayId = @HolidayId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @HolidayId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
