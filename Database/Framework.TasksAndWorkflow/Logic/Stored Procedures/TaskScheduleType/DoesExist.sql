IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='TaskScheduleTypeDoesExist')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeDoesExist'
	DROP  Procedure  TaskScheduleTypeDoesExist
END
GO

PRINT 'Creating Procedure TaskScheduleTypeDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: TaskScheduleTypeDoesExist
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

Create procedure dbo.TaskScheduleTypeDoesExist
(
		@Name					VARCHAR(50)		= NULL
	,	@TaskScheduleTypeId		INT				= NULL		
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'TaskScheduleType'		
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.TaskScheduleType a
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

