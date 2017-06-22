IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceCategoryDeleteHard'
	DROP  Procedure UserPreferenceCategoryDeleteHard
END
GO

PRINT 'Creating Procedure UserPreferenceCategoryDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: UserPreferenceCategoryDelete
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
CREATE Procedure dbo.UserPreferenceCategoryDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		 = NULL			
	,	@SystemEntityType		VARCHAR(50)		 = 'UserPreferenceCategory'
)
AS
BEGIN

	IF @KeyType = 'UserPreferenceCategoryId'
	BEGIN

		EXEC	dbo.UserPreferenceDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'UserPreferenceCategoryId',
				@AuditId	=	@AuditId

		DELETE	 dbo.UserPreferenceCategory
		WHERE	 UserPreferenceCategoryId = @KeyId

	END

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			
			@SystemEntityType		=	'UserPreferenceCategory'
		,	@EntityKey				=	@KeyId
		,	@AuditAction			=	'DeleteHard'
		,	@CreatedDate			=	@AuditDate
		,	@CreatedByPersonId		=	@AuditId
END
GO
