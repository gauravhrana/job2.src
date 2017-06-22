IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportCategoryDelete')
BEGIN
	PRINT 'Dropping Procedure ReportCategoryDelete'
	DROP  Procedure ReportCategoryDelete
END
GO

PRINT 'Creating Procedure ReportCategoryDelete'
GO
/******************************************************************************
**		File: 
**		Name: ReportCategoryDelete
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
CREATE Procedure dbo.ReportCategoryDelete
(
			@ReportCategoryId 		INT						
		,	@AuditId				INT						
		,	@AuditDate				DATETIME	= NULL		
		,	@SystemEntityType		VARCHAR(50)	= 'ReportCategory'
)
AS
BEGIN
		DELETE	 dbo.ReportCategory
		WHERE	 ReportCategoryId = @ReportCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReportCategory'
		,	@EntityKey				= @ReportCategoryId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
