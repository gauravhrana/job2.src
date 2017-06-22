IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceKeyDeleteHard')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyDeleteHard'
	DROP  Procedure UserPreferenceKeyDeleteHard
END

GO

PRINT 'Creating Procedure UserPreferenceKeyDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: UserPreferenceKeyDeleteHard
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
CREATE Procedure dbo.UserPreferenceKeyDeleteHard
(
		@KeyId 					INT					
	,	@KeyType				VARCHAR(50)							
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'UserPreferenceKey'
)
AS
BEGIN
	
	SET NOCOUNT ON

	IF @KeyType = 'UserPreferenceKeyId'
		BEGIN

			EXEC	dbo.UserPreferenceDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'UserPreferenceKeyId',
					@AuditId	=	@AuditId

			DELETE	dbo.UserPreferenceKey 
			WHERE	UserPreferenceKeyId = @KeyId

		END
	ELSE IF @KeyType = 'UserPreferenceDataTypeId'
		BEGIN

			DELETE	dbo.UserPreferenceKey 
			WHERE	DataTypeId = @KeyId

		END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			
			@SystemEntityType		= 'UserPreferenceKey' 
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
