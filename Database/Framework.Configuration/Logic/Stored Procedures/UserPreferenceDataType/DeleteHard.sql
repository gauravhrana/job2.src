IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDataTypeDeleteHard')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeDeleteHard'
	DROP  Procedure UserPreferenceDataTypeDeleteHard
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeDeleteHard
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

CREATE Procedure dbo.UserPreferenceDataTypeDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'UserPreferenceDataType'
)
AS
BEGIN
	
	SET NOCOUNT ON

	IF @KeyType = 'UserPreferenceDataTypeId'
		BEGIN

			EXEC	dbo.UserPreferenceDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'UserPreferenceDataTypeId',
					@AuditId	=	@AuditId

			EXEC	dbo.UserPreferenceKeyDeleteHard 
					@KeyId		=	@KeyId, 
					@KeyType	=	'UserPreferenceDataTypeId',
					@AuditId	=	@AuditId

			DELETE	 dbo.UserPreferenceDataType
			WHERE	 UserPreferenceDataTypeId = @KeyId

		END
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
	      
			@SystemEntityType		= 'UserPreferenceDataType'
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
