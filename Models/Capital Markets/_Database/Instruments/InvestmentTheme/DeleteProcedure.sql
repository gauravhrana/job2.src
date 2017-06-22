IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='InvestmentThemeDelete') 
	BEGIN
	DROP Procedure InvestmentThemeDelete
END
GO

CREATE Procedure dbo.InvestmentThemeDelete
(
		@InvestmentThemeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='InvestmentTheme'
)
AS
BEGIN
		DELETE dbo.InvestmentTheme
		WHERE	InvestmentThemeId = @InvestmentThemeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @InvestmentThemeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
