IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityTypeDetails')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeDetails'
	DROP  Procedure TaskEntityTypeDetails
END
GO

PRINT 'Creating Procedure TaskEntityTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: TaskEntityTypeDetails
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

CREATE Procedure dbo.TaskEntityTypeDetails
(
		@TaskEntityTypeId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'TaskEntityType'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@TaskEntityTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		

	SELECT	TaskEntityTypeId	
		,	ApplicationId
		,	Name						
		,	Description			
		,	Active		
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'			
	FROM	dbo.TaskEntityType 
	WHERE	TaskEntityTypeId = @TaskEntityTypeId	
		
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   