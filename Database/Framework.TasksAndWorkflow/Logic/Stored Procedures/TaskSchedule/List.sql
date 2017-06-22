IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleList')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleList'
	DROP  Procedure  dbo.TaskScheduleList
END
GO

PRINT 'Creating Procedure TaskScheduleList'
GO

/******************************************************************************
**		File: 
**		Name: TaskScheduleList
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

CREATE Procedure dbo.TaskScheduleList
(
		@AuditId				INT	
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskSchedule'
)
AS
BEGIN

	SELECT	a.TaskScheduleId	
		,	a.ApplicationId	
		,	a.TaskScheduleTypeId		
		,	a.TaskEntityId		
		,	b.Name		AS	'TaskScheduleType'				
		,	c.Name		AS	'TaskEntity'		
	FROM	dbo.TaskSchedule a
	INNER JOIN		dbo.TaskScheduleType b ON a.TaskScheduleTypeId	= b.TaskScheduleTypeId
	INNER JOIN		dbo.TaskEntity		 c ON a.TaskEntityId		= c.TaskEntityId
	WHERE	a.ApplicationId	=	@ApplicationId
	ORDER BY   a.TaskScheduleId			 ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO