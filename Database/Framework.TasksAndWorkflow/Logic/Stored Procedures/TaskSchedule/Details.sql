IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleDetails')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleDetails'
	DROP  Procedure TaskScheduleDetails
END
GO

PRINT 'Creating Procedure TaskScheduleDetails'
GO


/******************************************************************************
**		File: 
**		Name: TaskScheduleDetails
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

CREATE Procedure dbo.TaskScheduleDetails
(
		@TaskScheduleId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'TaskSchedule'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TaskScheduleId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.TaskScheduleId		
		,	a.ApplicationId
		,	a.TaskScheduleTypeId		
		,	a.TaskEntityId		
		,	b.Name					AS	'TaskScheduleType'				
		,	c.Name					AS	'TaskEntity'		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'
	FROM		dbo.TaskSchedule a
	INNER JOIN	dbo.TaskScheduleType b ON a.TaskScheduleTypeId	= b.TaskScheduleTypeId
	INNER JOIN	dbo.TaskEntity		 c ON a.TaskEntityId		= c.TaskEntityId		
	WHERE	TaskScheduleId = @TaskScheduleId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
   