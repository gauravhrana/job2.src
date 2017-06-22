IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDeleteHard')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDeleteHard'
	DROP  Procedure UserPreferenceDeleteHard
END
GO

PRINT 'Creating Procedure UserPreferenceDeleteHard'
GO


/******************************************************************************
**		File: 
**		ApplicationUserId: UserPreferenceDeleteHard
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

CREATE Procedure dbo.UserPreferenceDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'UserPreference'
)
AS
BEGIN

	IF @KeyType = 'UserPreferenceId'
		BEGIN			
			
			DELETE	 dbo.UserPreference
			WHERE	 UserPreferenceId = @KeyId

		END
	ELSE IF @KeyType = 'UserPreferenceCategoryId'
		BEGIN			
			
			DELETE	 dbo.UserPreference
			WHERE	 UserPreferenceCategoryId = @KeyId		

		END
	ELSE IF @KeyType = 'UserPreferenceKeyId'
		BEGIN			
			
			DELETE	 dbo.UserPreference
			WHERE	 UserPreferenceKeyId = @KeyId		

		END
	ELSE IF @KeyType = 'UserPreferenceDataTypeId'
		BEGIN			
			
			DELETE	 dbo.UserPreference
			WHERE	 DataTypeId = @KeyId		

		END
	ELSE IF @KeyType = 'ApplicationId'
		BEGIN			
			
			DELETE	 dbo.UserPreference
			WHERE	 ApplicationId = @KeyId		

		END
		

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
		
			@SystemEntityType		=	'UserPreference'
		,	@EntityKey				=	@KeyId
		,	@AuditAction			=	'DeleteHard'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO

