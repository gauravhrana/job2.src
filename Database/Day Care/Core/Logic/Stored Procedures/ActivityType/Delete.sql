IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityTypeDelete')
BEGIN
	PRINT 'Dropping Procedure ActivityTypeDelete'
	DROP  Procedure  ActivityTypeDelete
END
GO

PRINT 'Creating Procedure ActivityTypeDelete'
GO

/******************************************************************************
**		File: 
**		Name: ActivityTypeDelete
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
**     ----------							-----------
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

CREATE Procedure dbo.ActivityTypeDelete
(
	    @ActivityTypeId	    INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'ActivityType'
)
AS
 BEGIN
	DELETE	dbo.ActivityType
	WHERE	ActivityTypeId = @ActivityTypeId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @ActivityTypeId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

