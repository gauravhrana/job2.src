IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SeriesDelete') 
	BEGIN
	DROP Procedure SeriesDelete
END
GO

CREATE Procedure dbo.SeriesDelete
(
		@SeriesId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Series'
)
AS
BEGIN
		DELETE dbo.Series
		WHERE	SeriesId = @SeriesId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SeriesId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
