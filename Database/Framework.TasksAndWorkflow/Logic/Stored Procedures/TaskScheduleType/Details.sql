IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskScheduleTypeDetails')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeDetails'
	DROP  Procedure TaskScheduleTypeDetails
END
GO

PRINT 'Creating Procedure TaskScheduleTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: TaskScheduleTypeDetails
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

CREATE Procedure dbo.TaskScheduleTypeDetails
(
		@TaskScheduleTypeId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'TaskScheduleType'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TaskScheduleTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		
	
	SELECT	TaskScheduleTypeId	
		,	ApplicationId
		,	Name						
		,	Description			
		,	Active				
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'			
	FROM	dbo.TaskScheduleType 
	WHERE	TaskScheduleTypeId = @TaskScheduleTypeId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   