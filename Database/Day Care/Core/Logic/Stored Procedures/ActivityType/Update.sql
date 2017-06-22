IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure ActivityTypeUpdate'
	DROP  Procedure  ActivityTypeUpdate
END
GO

PRINT 'Creating Procedure ActivityTypeUpdate'

GO

/******************************************************************************
**		File: 
**		Name: ActivityTypeUpdate
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

CREATE Procedure dbo.ActivityTypeUpdate
(           
		@ActivityTypeId  		INT		
	,	@Name				    VARCHAR(50)
	,	@Description		    VARCHAR(500)	= NULL
	,	@SortOrder			    INT				= 1
	,   @AuditId				INT				
	,   @AuditDate				DATETIME		= NULL 
	,	@SystemEntityType		VARCHAR(50)		= 'ActivityType'
)
AS
BEGIN
	UPDATE	dbo.ActivityType
	SET		Name					 = @Name	
		,	Description              = @Description         
		,	SortOrder				 = @SortOrder
	WHERE	ActivityTypeId		     = @ActivityTypeId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @ActivityTypeId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

