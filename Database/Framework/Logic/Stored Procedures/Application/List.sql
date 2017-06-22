IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationList')
BEGIN
	PRINT 'Dropping Procedure ApplicationList'
	DROP  Procedure  dbo.ApplicationList
END
GO

PRINT 'Creating Procedure ApplicationList'
GO

/******************************************************************************
**		Task: 
**		Name: ApplicationList
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ApplicationList
(
		@AuditId				INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Application'
)
AS
BEGIN

	SELECT	ApplicationId	
		,	Name			
		,	Description		
		,	SortOrder			
	FROM	dbo.Application
	ORDER BY SortOrder			ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO