IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='ActivityTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure ActivityTypeDoesExist'
	DROP  Procedure  ActivityTypeDoesExist
END
GO

PRINT 'Creating Procedure ActivityTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: ActivityTypeDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.ActivityTypeDoesExist
(
		@Name					VARCHAR(50)		= NULL
	,	@ApplicationId			INT				
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'ActivityType'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.ActivityType a
	WHERE		a.Name = @Name
	AND			a.ApplicationId	= @ApplicationId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

