IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TaskEntityDoesExist')
BEGIN
	PRINT 'Dropping Procedure TaskEntityDoesExist'
	DROP  Procedure  TaskEntityDoesExist
END
GO

PRINT 'Creating Procedure TaskEntityDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TaskEntityDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.TaskEntityDoesExist
(
		@Name					VARCHAR(50)		= NULL
	,	@TaskEntityId			INT				= NULL								
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'TaskEntity'
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TaskEntity a
	WHERE		a.Name = @Name	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= NULL
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END
GO

