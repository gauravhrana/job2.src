IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityList')
BEGIN
	PRINT 'Dropping Procedure TaskEntityList'
	DROP  Procedure  dbo.TaskEntityList
END
GO

PRINT 'Creating Procedure TaskEntityList'
GO

/******************************************************************************
**		File: 
**		Name: TaskEntityList
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

CREATE Procedure dbo.TaskEntityList
(
		@AuditId				INT			
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskEntity'
)
AS
BEGIN

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
	WHERE		a.ApplicationId = @ApplicationId
	ORDER BY	a.SortOrder	            ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO