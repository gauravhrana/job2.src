IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityTypeList')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeList'
	DROP  Procedure  dbo.TaskEntityTypeList
END
GO

PRINT 'Creating Procedure TaskEntityTypeList'
GO

/******************************************************************************
**		File: 
**		Name: TaskEntityTypeList
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

CREATE Procedure dbo.TaskEntityTypeList
(
		@AuditId				INT			
	,	@ApplicationId			INT		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TaskEntityType'
)
AS
BEGIN
	SELECT	TaskEntityTypeId	
		,   ApplicationId
		,	Name				
		,	Description			
		,	Active				
		,	SortOrder			
	FROM	dbo.TaskEntityType 
	WHERE	ApplicationId	=	@ApplicationId
	ORDER BY	SortOrder			ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO