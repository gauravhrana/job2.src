IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SleepDeleteHard')
BEGIN
	PRINT 'Dropping Procedure SleepDeleteHard'
	DROP  Procedure SleepDeleteHard
END
GO

PRINT 'Creating Procedure SleepDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: SleepDelete
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

CREATE Procedure dbo.SleepDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Sleep'
)
AS
BEGIN

	IF @KeyType = 'SleepId'
		BEGIN

			DELETE	 dbo.Sleep
			WHERE	 SleepId = @KeyId	

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.Sleep
			WHERE	 StudentId = @KeyId

		END	


	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= 'Sleep'
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
