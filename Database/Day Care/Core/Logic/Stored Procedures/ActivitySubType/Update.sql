IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivitySubTypeUpdate')
BEGIN
	PRINT 'Dropping Procedure ActivitySubTypeUpdate'
	DROP  Procedure  ActivitySubTypeUpdate
END
GO

PRINT 'Creating Procedure ActivitySubTypeUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ActivitySubTypeUpdate
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

CREATE Procedure dbo.ActivitySubTypeUpdate
(          
		 @ActivitySubTypeId 	INT		 	
    ,    @ActivityTypeId        INT
	,	 @Name				    VARCHAR(50)
	,	 @Description		    VARCHAR(500)	= NULL
	,	 @SortOrder			    INT				= 1
	,    @AuditId			    INT			
    ,    @AuditDate		        DATETIME		= NULL 
	,	 @SystemEntityType		VARCHAR(50)		= 'ActivitySubType'
)
AS
 BEGIN
	UPDATE	dbo.ActivitySubType
	SET		ActivityTypeId          = @ActivityTypeId   						
		,	Name					= @Name			       
		,	Description             = @Description         
		,	SortOrder				= @SortOrder
	WHERE	ActivitySubTypeId		= @ActivitySubTypeId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @ActivitySubTypeId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

