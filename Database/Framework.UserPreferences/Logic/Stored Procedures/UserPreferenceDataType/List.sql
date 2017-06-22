IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'UserPreferenceDataTypeList')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceDataTypeList'
	DROP  Procedure  dbo.UserPreferenceDataTypeList
END
GO

PRINT 'Creating Procedure UserPreferenceDataTypeList'
GO

/******************************************************************************
**		File: 
**		Name: UserPreferenceDataTypeList
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

CREATE Procedure dbo.UserPreferenceDataTypeList
(
		@AuditId				INT			
	,	@ApplicationId			INT	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'UserPreferenceDataType'
)
AS
	SELECT	UserPreferenceDataTypeId
		,	ApplicationId
		,	Name						
		,	Description	
		,	SortOrder			
	FROM	dbo.UserPreferenceDataType
	WHERE	ApplicationId  = @ApplicationId 
	ORDER BY UserPreferenceDataTypeId			ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
		
GO
