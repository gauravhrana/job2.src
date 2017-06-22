IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ActivityDeleteHard'
	DROP  Procedure ActivityDeleteHard
END
GO

PRINT 'Creating Procedure ActivityDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: ActivityDelete
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

CREATE Procedure dbo.ActivityDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'Activity'
)
AS
BEGIN

	IF @KeyType = 'ActivityId'
		BEGIN

			DELETE	 dbo.Activity
			WHERE	 ActivityId = @KeyId	

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.Activity
			WHERE	 StudentId = @KeyId

		END
	ELSE IF @KeyType = 'ActivityTypeId'
		BEGIN

			DELETE	 dbo.Activity
			WHERE	 ActivityTypeId = @KeyId

		END 
	ELSE IF @KeyType = 'ActivitySubTypeId'
		BEGIN

			DELETE	 dbo.Activity
			WHERE	 ActivitySubTypeId = @KeyId

		END 
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
