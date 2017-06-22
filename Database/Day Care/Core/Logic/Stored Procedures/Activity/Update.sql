IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityUpdate')
BEGIN
	PRINT 'Dropping Procedure ActivityUpdate'
	DROP  Procedure  ActivityUpdate
END
GO

PRINT 'Creating Procedure ActivityUpdate'

GO

/******************************************************************************
**		File: 
**		Name: ActivityUpdate
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
**		----------						-----------
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

CREATE Procedure dbo.ActivityUpdate
(       
		@ActivityId		    INT 		
	,	@StudentId			INT
	,	@ActivityTypeId		INT
	,	@ActivitySubTypeId  INT	
	,   @AuditId			INT			
    ,   @AuditDate			DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'Activity'
)
AS
BEGIN
	UPDATE	dbo.Activity
	SET		StudentId				    = @StudentId		   
		,	ActivityTypeId              = @ActivityTypeId      
		,	ActivitySubTypeId			= @ActivitySubTypeId
	WHERE	ActivityId		     		= @ActivityId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivityId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END

GO

