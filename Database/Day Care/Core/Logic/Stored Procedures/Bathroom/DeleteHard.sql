IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BathroomDeleteHard')
BEGIN
	PRINT 'Dropping Procedure BathroomDeleteHard'
	DROP  Procedure BathroomDeleteHard
END
GO

PRINT 'Creating Procedure BathroomDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: BathroomDelete
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

CREATE Procedure dbo.BathroomDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'Bathroom'	
)
AS
BEGIN

	IF @KeyType = 'BathroomId'
		BEGIN

			DELETE	 dbo.Bathroom
			WHERE	 BathroomId = @KeyId	

		END
	ELSE IF @KeyType = 'StudentId'
		BEGIN

			DELETE	 dbo.Bathroom
			WHERE	 StudentId = @KeyId

		END	
	ELSE IF @KeyType = 'TeacherId'
		BEGIN

			DELETE	 dbo.Bathroom
			WHERE	 TeacherId = @KeyId

		END	
	ELSE IF @KeyType = 'DiaperStatusId'
		BEGIN

			DELETE	 dbo.Bathroom
			WHERE	 DiaperStatusId = @KeyId

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
