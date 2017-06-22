IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageContextList')
BEGIN
	PRINT 'Dropping Procedure HelpPageContextList'
	DROP  Procedure  dbo.HelpPageContextList
END
GO

PRINT 'Creating Procedure HelpPageContextList'
GO

/******************************************************************************
**		File: 
**		Name: HelpPageContextList
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.HelpPageContextList
(
		@ApplicationId			INT
	,	@AuditId				INT	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'HelpPageContext'
)
AS
BEGIN

	SELECT	a.HelpPageContextId
		,	a.ApplicationId	
		,	a.Name			
		,	a.Description		
		,	a.SortOrder
	FROM	dbo.HelpPageContext		a
	WHERE	a.ApplicationId = @ApplicationId		
	ORDER BY a.SortOrder			ASC
		,	 a.HelpPageContextId	ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
