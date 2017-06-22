IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSetList')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetList'
	DROP  Procedure  dbo.BatchFileSetList
END
GO

PRINT 'Creating Procedure BatchFileSetList'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileSetList
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

CREATE Procedure dbo.BatchFileSetList
(
		@AuditId				INT			
	,	@ApplicationId			INT		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileSet'
)
AS
BEGIN

	SELECT	a.BatchFileSetId	
		,	a.ApplicationId	
		,	a.Name						
		,	a.Description			
		,	a.CreatedDate			
		,	a.CreatedByPersonId		
		,	b.FirstName + ' ' + b.LastName	AS 'CreatedByPerson'
	FROM	dbo.BatchFileSet a
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	b	
				ON	a.CreatedByPersonId	= b.ApplicationUserId
	WHERE a.ApplicationId = @ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
		
GO