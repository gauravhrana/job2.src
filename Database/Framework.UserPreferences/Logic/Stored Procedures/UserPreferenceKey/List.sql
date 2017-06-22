IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceKeyList')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceKeyList'
	DROP  Procedure  dbo.UserPreferenceKeyList
END
GO

PRINT 'Creating Procedure UserPreferenceKeyList'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceKeyList
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
**     ----------					   ---------
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
CREATE Procedure dbo.UserPreferenceKeyList
(
		@AuditId				INT				
	,	@ApplicationId			INT		
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'UserPreferenceKey'
)
AS
BEGIN

	SET NOCOUNT ON

	SELECT		UserPreferenceKeyId	
			,	ApplicationId	
			,	Name					
			,	Value			
			,	DataTypeId				
			,	Description				
			,	SortOrder						
	FROM	dbo.UserPreferenceKey 
	WHERE	ApplicationId = @ApplicationId
	ORDER BY	UserPreferenceKeyId				ASC
		,		SortOrder						ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO