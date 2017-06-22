IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportDelete')
BEGIN
	PRINT 'Dropping Procedure ReportDelete'
	DROP  Procedure ReportDelete
END
GO

PRINT 'Creating Procedure ReportDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReportDelete
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ReportDelete
(
			@ReportId 				INT						
		,	@AuditId				INT						
		,	@AuditDate				DATETIME	= NULL		
		,	@SystemEntityType		VARCHAR(50)	= 'Report'
)
AS
BEGIN
		DELETE	 dbo.Report
		WHERE	 ReportId = @ReportId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Report'
		,	@EntityKey				= @ReportId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
