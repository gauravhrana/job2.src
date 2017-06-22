IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SickReportList')
BEGIN
	PRINT 'Dropping Procedure SickReportList'
	DROP PROCEDURE SickReportList
END
GO

PRINT 'Creating Procedure SickReportList'
GO

/******************************************************************************
**		File: 
**		Name: SickReportList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SickReportList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SickReport'
)
AS
BEGIN
		SELECT	a.*
		FROM	dbo.SickReport a
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY SickReportId	ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
