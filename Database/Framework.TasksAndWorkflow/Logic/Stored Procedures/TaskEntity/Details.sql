IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityDetails')
BEGIN
	PRINT 'Dropping Procedure TaskEntityDetails'
	DROP  Procedure TaskEntityDetails
END
GO

PRINT 'Creating Procedure TaskEntityDetails'
GO


/******************************************************************************
**		File: 
**		Name: TaskEntityDetails
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

CREATE Procedure dbo.TaskEntityDetails
(
		@TaskEntityId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TaskEntity'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TaskEntityId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	
	
	SELECT	a.TaskEntityId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.TaskEntityTypeId	 
		,	a.Description		 
		,	a.Active			 
		,	a.SortOrder
		,	b.Name					AS	'TaskEntityType'
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'
	FROM		dbo.TaskEntity a
	INNER JOIN	dbo.TaskEntityType b ON a.TaskEntityTypeId = b.TaskEntityTypeId
	WHERE	TaskEntityId	= @TaskEntityId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   