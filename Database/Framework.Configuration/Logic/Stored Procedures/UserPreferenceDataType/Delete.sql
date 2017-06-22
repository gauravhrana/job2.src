IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceDataTypeDelete')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeDelete'
	DROP  Procedure UserPreferenceDataTypeDelete
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeDelete'
GO


/******************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeDelete
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

CREATE Procedure dbo.UserPreferenceDataTypeDelete
(
		@UserPreferenceDataTypeId 				INT					
	,	@AuditId								INT					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50) = 'UserPreferenceDataType'
)
AS
BEGIN

	DELETE	 FROM dbo.UserPreferenceDataType
	WHERE	 UserPreferenceDataTypeId = @UserPreferenceDataTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
	    	@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserPreferenceDataTypeId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
