IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ManagementFirmDelete') 
	BEGIN
	DROP Procedure ManagementFirmDelete
END
GO

CREATE Procedure dbo.ManagementFirmDelete
(
		@ManagementFirmId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='ManagementFirm'
)
AS
BEGIN
		DELETE dbo.ManagementFirm
		WHERE	ManagementFirmId = @ManagementFirmId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ManagementFirmId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
