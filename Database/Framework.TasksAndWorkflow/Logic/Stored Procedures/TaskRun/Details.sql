IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskRunDetails')
BEGIN
	PRINT 'Dropping Procedure TaskRunDetails'
	DROP  Procedure TaskRunDetails
END
GO

PRINT 'Creating Procedure TaskRunDetails'
GO


/******************************************************************************
**		File: 
**		Name: TaskRunDetails
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

CREATE Procedure dbo.TaskRunDetails
(
		@TaskRunId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'TaskRun'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TaskRunId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		
	
	SELECT	a.TaskRunId		
		,	a.ApplicationId
		,	a.TaskScheduleId	
		,	a.TaskEntityId	
		,	a.BusinessDate	
		,	a.StartTime		
		,	a.EndTime		
		,	a.RunBy		
		,	b.Name		AS	'TaskEntity'		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'			
	FROM		dbo.TaskRun a
	INNER JOIN	dbo.TaskEntity b ON a.TaskEntityId		= b.TaskEntityId
	WHERE	TaskRunId = @TaskRunId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskRunId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
   