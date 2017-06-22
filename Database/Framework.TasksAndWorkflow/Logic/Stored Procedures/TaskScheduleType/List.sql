IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleTypeList')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeList'
	DROP  Procedure  dbo.TaskScheduleTypeList
END
GO

PRINT 'Creating Procedure TaskScheduleTypeList'
GO

/******************************************************************************
**		File: 
**		Name: TaskScheduleTypeList
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

CREATE Procedure dbo.TaskScheduleTypeList
(
		@AuditId				INT				
	,	@ApplicationId			INT		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskScheduleType'
)
AS
BEGIN

	SELECT	TaskScheduleTypeId	
		,	ApplicationId
		,	Name				
		,	Description			
		,	Active				
			SortOrder			
	FROM	dbo.TaskScheduleType 
	WHERE	ApplicationId	=	@ApplicationId
	ORDER BY SortOrder				ASC
		,	 TaskScheduleTypeId		ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO