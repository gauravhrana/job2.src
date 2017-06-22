IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityTypeChildrenGet')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeChildrenGet'
	DROP  Procedure TaskEntityTypeChildrenGet
END
GO

PRINT 'Creating Procedure TaskEntityTypeChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: TaskEntityTypeChildrenGet
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
**     ----------						-----------
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

CREATE Procedure dbo.TaskEntityTypeChildrenGet
(
		@TaskEntityTypeId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'TaskEntityType'
)
AS
BEGIN

	-- GET TaskEntity Records
	SELECT	a.TaskEntityId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.TaskEntityTypeId	 
		,	a.Description		 
		,	a.Active			 
		,	a.SortOrder
		,	b.Name		AS	'TaskEntityType'
	FROM		dbo.TaskEntity a
	INNER JOIN	dbo.TaskEntityType b ON a.TaskEntityTypeId = b.TaskEntityTypeId
	WHERE	a.TaskEntityTypeId = @TaskEntityTypeId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityTypeId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   