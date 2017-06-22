IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityDelete')
BEGIN
	PRINT 'Dropping Procedure ActivityDelete'
	DROP  Procedure  ActivityDelete
END
GO

PRINT 'Creating Procedure ActivityDelete'
GO

/******************************************************************************
**		File: 
**		Name: ActivityDelete
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

CREATE Procedure dbo.ActivityDelete
(
		@ActivityId			INT
	,	@AuditId			INT			
    ,	@AuditDate			DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50) = 'Activity'	
)
AS
BEGIN

	DELETE	dbo.Activity
	WHERE	ActivityId = @ActivityId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivityId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

