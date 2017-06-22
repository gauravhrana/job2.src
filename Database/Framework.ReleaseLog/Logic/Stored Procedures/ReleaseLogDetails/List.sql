IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailList')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailList'
	DROP  Procedure  dbo.ReleaseLogDetailList
END
GO

PRINT 'Creating Procedure ReleaseLogDetailList'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogList
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

CREATE Procedure dbo.ReleaseLogDetailList
(
		@AuditId				INT		
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseLogDetails'
)
AS
BEGIN

	SELECT	ReleaseLogDetailId	
		,	ApplicationId		
	    ,   ReleaseLogId				
		,	ItemNo						
		,	Description					
		,	SortOrder					
		,	RequestedBy                 
		,	PrimaryDeveloper            
		,	RequestedDate
	FROM		dbo.ReleaseLogDetail 
	WHERE	ApplicationId = @ApplicationId
	ORDER BY	SortOrder				ASC
		,		ReleaseLogDetailId		ASC
		,		ReleaseLogId			ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO