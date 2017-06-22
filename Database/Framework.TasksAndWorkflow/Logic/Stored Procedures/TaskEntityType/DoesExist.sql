IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TaskEntityTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeDoesExist'
	DROP  Procedure  TaskEntityTypeDoesExist
END
GO

PRINT 'Creating Procedure TaskEntityTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TaskEntityTypeDoesExist
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

Create procedure dbo.TaskEntityTypeDoesExist
(
		@Name					VARCHAR(50)		= NULL	
	,	@TaskEntityTypeId	    INT				= NULL						
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'TaskEntityType'
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TaskEntityType a
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

