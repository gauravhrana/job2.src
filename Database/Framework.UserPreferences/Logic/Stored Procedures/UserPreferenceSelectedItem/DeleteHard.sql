IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceSelectedItemDeleteHard')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceSelectedItemDeleteHard'
	DROP  Procedure UserPreferenceSelectedItemDeleteHard
END
GO

PRINT 'Creating Procedure UserPreferenceSelectedItemDeleteHard'
GO


/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceSelectedItemDeleteHard
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

CREATE Procedure dbo.UserPreferenceSelectedItemDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'UserPreferenceSelectedItem'
)
AS
BEGIN

	IF @KeyType = 'UserPreferenceSelectedItemId'
		BEGIN			
			
			DELETE	 dbo.UserPreferenceSelectedItem
			WHERE	 UserPreferenceSelectedItemId = @KeyId

		END
	ELSE IF @KeyType = 'UserPreferenceKeyId'
		BEGIN			
			
			DELETE	 dbo.UserPreferenceSelectedItem
			WHERE	 UserPreferenceKeyId = @KeyId		

		END
		

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
		
			@SystemEntityType		=	'UserPreferenceSelectedItem'
		,	@EntityKey				=	@KeyId
		,	@AuditAction			=	'DeleteHard'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO

